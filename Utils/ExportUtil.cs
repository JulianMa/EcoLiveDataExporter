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
            await DataExporter.WriteToFile("stores", "/", TradeUtil.GetStoresString());
            EcoLiveData.Status = $"Store data exported at {DateTime.Now.ToShortTimeString()}";
        }
    }
}
