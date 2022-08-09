using Eco.Core.Items;
using Eco.Gameplay.Items;
using Eco.Mods.TechTree;
using Eco.Plugins.EcoLiveDataExporter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonItems
    {
        public string PluginVersion { get; set; }
        public int Version { get; set; }
        public Dictionary<string, JsonItem> AllItems { get; set; }
        public JsonDateTime ExportedAt { get; set; }
        public JsonItems()
        {
            PluginVersion = EcoLiveData.PluginVersion.ToString();
            Version = 1;
            ExportedAt = new JsonDateTime(DateTime.UtcNow);

            Logger.Debug("Exporting all items and their Tags: " + Item.AllItems.Count());
            AllItems = Item.AllItems.GroupBy(t => t.DisplayName.NotTranslated, StringComparer.OrdinalIgnoreCase).ToDictionary(t => t.Key, t => new JsonItem(t.First()));
        }
    }
}
