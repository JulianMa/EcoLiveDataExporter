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

        /**
         * convert and add trade to List
         * 
         * @param trade: GameAction event of every possible trade
        **/
        public static void Process(GameAction trade)
        {
            if (trade == null || !Config.Data.SaveHistoricalTradesData) return;
            if(trade is CurrencyTrade){
                var tradePoco = new JsonTrade((CurrencyTrade) trade);
                TradesToProcess.Add(tradePoco);
                Logger.Debug($"trade added {trade}");
            }
            else if(trade is BarterTrade)
            {
                var tradePoco = new JsonTrade((BarterTrade) trade);
                TradesToProcess.Add(tradePoco);
                Logger.Debug($"trade added {trade}");
            }
            else if(trade is TransferMoney)
            {
                var tradePoco = new JsonTrade((TransferMoney) trade);
                TradesToProcess.Add(tradePoco);
                Logger.Debug($"trade added {trade}");
            }
            else
            {
                Logger.Debug("this event is not a trade");
            }
        }
    }
}
