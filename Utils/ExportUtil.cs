using System;
using System.Threading.Tasks;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    public class ExportUtil
    {
        private DateTime LastExport = DateTime.MinValue;
        private Task ThrotleTask = Task.CompletedTask;
        private ExportUtil() { }
        public static ExportUtil Instance = new ExportUtil();
        public void DumpStoreDataToDatabase()
        {
            var timeSinceLastExport = DateTime.Now - this.LastExport;
            // Only allow exports every x configured ammount of minutes
            if (timeSinceLastExport > TimeSpan.FromMinutes(Config.Data.ThrotleDbUpdatesForMinutes))
            {
                ThrotleTask = UpdateStoreData();
            } else if(ThrotleTask.IsCompleted)
            {
                Logger.Debug("Started a thread to export data when throtle period ends");
                // Start a task to dump store data right after the throtle period expired (unless task is already created)
                ThrotleTask = Task.Delay(TimeSpan.FromMinutes(Config.Data.ThrotleDbUpdatesForMinutes) - timeSinceLastExport).ContinueWith((tsk) => UpdateStoreData());
            } else
            {
                Logger.Debug("Data export not queued since there is already an active data export queued up");
            }
        }

        

        private async Task UpdateStoreData()
        {
            LastExport = DateTime.Now;

            // Overrides current store data in file
            Logger.Debug("Exporting store data");
            var storesString = TradeUtil.GetStoresString();
            if (storesString == null)
            {
                return;
            }

            await DataExporter.WriteToFile("stores", "/", storesString);
            Logger.Debug($"Store data exported at {DateTime.Now.ToShortTimeString()}");
            EcoLiveData.Status = $"Store data exported at {DateTime.Now.ToShortTimeString()}";

            if (Config.Data.SaveHistoricalStoreData)
            {
                Logger.Debug("Saving stores data to history file");
                await DataExporter.AddToFile("storesHistoric", "/", storesString);
            }
            Logger.Debug("Finished UpdateStoreData");
        }
    }
}
