using System;
using System.Collections.Generic;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonPatchHelper<T>
    {
        public List<T> A { get; set; }
        public JsonPatchHelper (T a)
        {
            A = new List<T> { a };
        }
    }
}
