using Eco.Gameplay.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonHistStores
    {
        public int Version { get; set; }
        public List<JsonStore> Stores { get; set; }
        public int ExportedAtYear { get; set; }
        public int ExportedAtMonth { get; set; }
        public int ExportedAtDay { get; set; }
        public int ExportedAtHour { get; set; }
        public int ExportedAtMin { get; set; }
        public string ExportedAt { get; set; }
        public JsonHistStores(IEnumerable<StoreComponent> storeComponents)
        {
            Version = 1;
            Stores = storeComponents.Select(store => new Poco.JsonStore(store)).ToList();
            var now = DateTime.UtcNow;
            ExportedAtYear = now.Year;
            ExportedAtMonth = now.Month;
            ExportedAtDay = now.Day;
            ExportedAtHour = now.Hour;
            ExportedAtMin = now.Minute;
            ExportedAt = now.ToString("yyyy-MM-dd, H:mm:ss");
        }
    }
}
