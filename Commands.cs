using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Plugins.EcoLiveDataExporter.Utils;

namespace Eco.Plugins.EcoLiveDataExporter
{
    public partial class Commands : IChatCommandHandler
    {
        [ChatCommand("Start fething live data and exporting it to json database", ChatAuthorizationLevel.Admin)]
        public static void StartLiveDataTimers(User user)
        {
            Logger.Debug("Admin starting timmers");
            TimerUtil.Instance.RestartTimers();
        }

        [ChatCommand("Stop fething live data and exporting it to json database", ChatAuthorizationLevel.Admin)]
        public static void StopLiveDataTimers(User user)
        {
            Logger.Debug("Admin stoping timmers");
            TimerUtil.Instance.StopTimers();
        }

        [ChatCommand("Dumps all available store listings to file", ChatAuthorizationLevel.User)]
        public static void DumpStoreData(User user)
        {
            ExportUtil.Instance.DumpStoreDataToDatabase();
            user.Player.InfoBoxLocStr("Data dump complete");
        }

        [ChatCommand("Dump crafting recipes to file", ChatAuthorizationLevel.Admin)]
        public static void DumpRecipes(User user)
        {
            Logger.Debug("Updating recipe file");
            ExportUtil.Instance.DumpRecipesToDatabase();
        }
    }
}
