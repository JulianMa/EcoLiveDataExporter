using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
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

        public static string GetStoresString()
        {
            try
            {
                Logger.Debug("Started collecting store information");
                var storesPocos = Stores.Select(store => new Poco.JsonStore(store)).ToList();
                Logger.Debug("Got store pocos");
                var json = JsonConvert.SerializeObject(storesPocos);
                Logger.Debug("Got stores string");
                return json;
            }
            catch (Exception e)
            {
                Logger.Error($"Got an exception trying to export store data: \n {e}");
                return null;
            }
        }
    }
}
