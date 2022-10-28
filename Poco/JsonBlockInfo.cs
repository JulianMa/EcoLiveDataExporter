using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonBlockInfo
    {
        [JsonProperty(Order=1)]
        public string PluginVersion { get; set; }
        [JsonProperty(Order=2)]
        public int Version { get; set; }
        [JsonProperty("AllBlocks", Order=3)]
        public Dictionary<int,Dictionary<string,int>> BlockInfos = new Dictionary<int,Dictionary<string,int>>();
        [JsonProperty(Order=4)]
        public JsonDateTime ExportedAt { get; set; }


        public JsonBlockInfo()
        {
            PluginVersion = EcoLiveData.PluginVersion.ToString();
            Version = 2;
            ExportedAt = new JsonDateTime(DateTime.UtcNow);
        }

        public void AddBlockInfo(KeyValuePair<string,int[]> blockInfo){
            var values = blockInfo.Value;
            BlockInfos.Add(values[0],new Dictionary<string, int>(){{blockInfo.Key,values[1]}});
        }
    }
}