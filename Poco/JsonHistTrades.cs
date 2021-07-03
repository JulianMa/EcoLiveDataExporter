using Eco.Gameplay.Components;
using Eco.Gameplay.GameActions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonHistTrades
    {
        public int Version { get; set; }
        public List<JsonTrade> Trades { get; set; }
        public JsonDateTime ExportedAt { get; set; }
        public JsonHistTrades(List<JsonTrade> trades)
        {
            Version = 2;
            Trades = trades;
            ExportedAt = new JsonDateTime(DateTime.UtcNow);
        }
    }
}
