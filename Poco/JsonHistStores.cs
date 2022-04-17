using Eco.Gameplay.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonHistStores
    {
        public string PluginVersion { get; set; }
        public int Version { get; set; }
        public List<JsonStore> Stores { get; set; }
        public JsonDateTime ExportedAt { get; set; }
        public JsonHistStores(IEnumerable<StoreComponent> storeComponents)
        {
            PluginVersion = EcoLiveData.PluginVersion.ToString();
            Version = 2;
            Stores = storeComponents.Select(store => new Poco.JsonStore(store)).ToList();
            ExportedAt = new JsonDateTime(DateTime.UtcNow);
        }
    }
}
