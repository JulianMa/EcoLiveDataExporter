using System;
using System.Collections.Generic;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonDateTime
    {
        public long Ticks { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Min { get; set; }
        public int Sec { get; set; }
        public string StringRepresentation { get; set; }

        public JsonDateTime(DateTime time)
        {
            Ticks = time.Ticks;
            Year = time.Year;
            Month = time.Month;
            Day = time.Day;
            Hour = time.Hour;
            Min = time.Minute;
            Sec = time.Second;
            StringRepresentation = time.ToString("yyyy-MM-dd, H:mm:ss");
        }
    }
}
