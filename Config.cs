using Eco.Core.Plugins;

using System.ComponentModel;
namespace Eco.Plugins.EcoLiveDataExporter
{
    public class Config
    {
        private Config() { }
        public static readonly Config Instance = new Config();
        public static ConfigData Data { get { return Instance.PluginConfig.Config; } }

        public PluginConfig<ConfigData> PluginConfig { get; private set; }

        public void Initialize()
        {
            PluginConfig = new PluginConfig<ConfigData>("LiveDataExporter");
        }
    }

    public class ConfigData
    {
        [Description("Url for the rest api that receives file updates and asynchrounosly writes them to disk."), Category("Database output")]
        public string DbOutputApp { get; set; } = "http://localhost:3030";

        [Description("Do not allow updating database more than once on each x minutes (this is considered for user command only)"), Category("Database output")]
        public int ThrotleDbUpdatesForMinutes { get; set; } = 1;

        [Description("Export util runs each x minutes."), Category("Recurrent data export")]
        public int ExportUtilTimer { get; set; } = 15;

        [Description("Enables saving store data - store prices are used for price calculator."), Category("Recurrent data export")]
        public bool SaveStoreData { get; set; } = true;

        [Description("Enables saving store historical data - useful for making a dashboard and getting statistics over time."), Category("Recurrent data export")]
        public bool SaveHistoricalStoreData { get; set; } = true;

        [Description("Enables saving tradeActions data - useful for list past tradings, making dashboards and getting statistics over time."), Category("Recurrent data export")]
        public bool SaveHistoricalTradesData { get; set; } = true;

        [Description("Enables debugging output to the console."), Category("Debugging")]
        public bool Debug { get; set; } = false;
    }
}
