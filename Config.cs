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
        [Description("When true the files will be saved locally (currently it's the default option for sharing the files with other tools)")]
        public bool SaveFilesLocally { get; set; } = true;

        [Description("This identifies your server. Ask Ucat for an id specific to your server. (To be deprecated).")]
        public string SecretKey { get; set; } = "";

        [Description("Do not allow updating database more than once on each x minutes (this is considered for user command only)"), Category("Database output")]
        public int ThrotleDbUpdatesForMinutes { get; set; } = 1;

        [Description("Export util runs each x minutes."), Category("Recurrent data export")]
        public int ExportUtilTimer { get; set; } = 5;

        [Description("Enables saving store data - store prices are used for price calculator."), Category("Recurrent data export")]
        public bool SaveStoreData { get; set; } = true;

        [Description("Enables saving store historical data - useful for making a dashboard and getting statistics over time."), Category("Recurrent data export")]
        public bool SaveHistoricalStoreData { get; set; } = false;

        [Description("Enables saving tradeActions data - useful for list past tradings, making dashboards and getting statistics over time."), Category("Recurrent data export")]
        public bool SaveHistoricalTradesData { get; set; } = false;

        [Description("Enables saving crafting tables data - useful for list user tables and their upgrades."), Category("Recurrent data export")]
        public bool SaveCraftingTablesData { get; set; } = true;

        [Description("Enables saving block counting data - useful for list left blocks in the world."), Category("Recurrent data export")]
        public bool SaveBlockCountData { get; set; } = false;

        [Description("Enables debugging output to the console."), Category("Debugging")]
        public bool Debug { get; set; } = false;
    }
}
