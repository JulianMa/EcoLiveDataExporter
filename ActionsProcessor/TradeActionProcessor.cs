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
        public static void Process(TradeAction trade)
        {
            if (trade == null || !Config.Data.SaveHistoricalTradesData) return;

            Logger.Debug($"Trade detected from {trade.Buyer.Name}: {trade.BoughtOrSold} - {trade.NumberOfItems} - {trade.ItemUsed.Name}");
            var tradePoco = new JsonTrade(trade);
            Logger.Debug($"trade obj: {tradePoco}");
        }
    }
}
