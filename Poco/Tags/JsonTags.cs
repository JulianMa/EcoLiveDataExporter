using Eco.Gameplay.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonTags
    {
        public string PluginVersion { get; set; }
        public int Version { get; set; }
        public Dictionary<string, IEnumerable<string>> Tags { get; set; }
        public JsonDateTime ExportedAt { get; set; }
        public JsonTags(IEnumerable<Tag> craftableTags)
        {
            PluginVersion = EcoLiveData.PluginVersion.ToString();
            Version = 2;
            Tags = craftableTags.ToDictionary(t => t?.DisplayName.NotTranslated, t => t?.TaggedItems().Select(t => t.DisplayName.NotTranslated));
            ExportedAt = new JsonDateTime(DateTime.UtcNow);
        }
    }
}
