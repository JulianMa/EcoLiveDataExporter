using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Plugins.EcoLiveDataExporter.ActionsProcessor;
using Eco.Plugins.EcoLiveDataExporter.Poco;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    public class TradeUtil
    {
        public static IEnumerable<StoreComponent> Stores => WorldObjectUtil.AllObjsWithComponent<StoreComponent>();

        public static string[] GetStoresString()
        {
            try
            {
                Logger.Debug("Started collecting store information");
                var histStores = new JsonHistStores(Stores);
                var storesJson = JsonConvert.SerializeObject(histStores);
                // We serialize an array of history so that when we patch the db it add's it to the existing array
                var histStoresJson = Config.Data.SaveHistoricalStoreData ? JsonConvert.SerializeObject(new JsonPatchHelper<JsonHistStores>(histStores)) : "";
                Logger.Debug("Got stores string");
                return new string[] { storesJson, histStoresJson };
            }
            catch (Exception e)
            {
                Logger.Error($"Got an exception trying to export store data: \n {e}");
                return null;
            }
        }
        public static string GetTradesString()
        {
            if (TradeActionProcessor.TradesToProcess.Count == 0)
            {
                Logger.Debug("There are no trades data to export!");
                return null;
            }
            try
            {
                var tradesJson = JsonConvert.SerializeObject(new JsonPatchHelper<JsonHistTrades>(new JsonHistTrades(TradeActionProcessor.TradesToProcess)));
                Logger.Debug("Exporting json: " + tradesJson);
                TradeActionProcessor.TradesToProcess = new List<JsonTrade>();
                return tradesJson;
            }
            catch (Exception e)
            {
                Logger.Error($"Got an exception trying to export trades data: \n {e}");
                return null;
            }
        }
    }
}
