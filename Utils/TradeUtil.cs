using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;

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
            var storesPocos = Stores.Select(store => new Poco.JsonStore(store)).ToList();
            var storesString = JsonConvert.SerializeObject(storesPocos );
            Logger.Debug(storesString);
            return storesString;
        }
    }
}
