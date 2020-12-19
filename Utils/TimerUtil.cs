using Eco.Core.Utils.Threading;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    public class TimerUtil
    {
        private TimerUtil() { }
        public static TimerUtil Instance = new TimerUtil();

        private IntervalActionWorker HistoricalStoreDataWorker { get; set; }

        public void RestartTimers()
        {
            HistoricalStoreDataWorker?.Shutdown();
            HistoricalStoreDataWorker = PeriodicWorkerFactory.CreateWithInterval(TimeSpan.FromMinutes(Config.Data.UpdateHistoricalStoreDataTimer), OnSaveHistoricalStoreDataTimer);
            HistoricalStoreDataWorker.Start();
        }

        public void StopTimers()
        {
            HistoricalStoreDataWorker?.Shutdown();
        }

        private async Task OnSaveHistoricalStoreDataTimer(CancellationToken token)
        {
            if (!token.IsCancellationRequested)
            {
                await ExportUtil.Instance.ExportStoreData();
            }
        }
    }
}
