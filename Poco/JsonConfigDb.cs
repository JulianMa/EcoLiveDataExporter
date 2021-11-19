using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonConfigDb
    {
        public string Name { get; set; }
        public string Bin { get; set; }
        public JsonDateTime ExportedAt { get; set; }

        public JsonConfigDb() { 
        }

        public JsonConfigDb(string name, string bin)
        {
            Name = name;
            Bin = bin;
            ExportedAt = new JsonDateTime(DateTime.UtcNow);
        }

        public JsonConfigDb UpdateFileNow()
        {
            return new JsonConfigDb(Name, Bin);
        }
    }
}
