using Eco.Gameplay.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonHistCraftingTables
    {
        public int Version { get; set; }
        public List<JsonCraftingTable> CraftingTables { get; set; }
        public JsonDateTime ExportedAt { get; set; }
        public JsonHistCraftingTables(IEnumerable<CraftingComponent> craftingComponents)
        {
            Version = 2;
            CraftingTables = craftingComponents?.Select(table => new Poco.JsonCraftingTable(table)).ToList() ?? new List<JsonCraftingTable>();
            ExportedAt = new JsonDateTime(DateTime.UtcNow);
        }
    }
}
