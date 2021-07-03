using Eco.Gameplay.GameActions;
using Eco.Plugins.EcoLiveDataExporter.Poco;
using Eco.Plugins.EcoLiveDataExporter.Utils;

using System;
using System.Collections.Generic;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.ActionsProcessor
{
    public class TradeActionProcessor
    {
        public static List<JsonTrade> TradesToProcess = new List<JsonTrade>();
        public static void Process(CurrencyTrade trade)
        {
            if (trade == null || !Config.Data.SaveHistoricalTradesData) return;

            var tradePoco = new JsonTrade(trade);
            TradesToProcess.Add(tradePoco);
        }
    }
}
