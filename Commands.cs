using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Messaging.Chat.Commands;
using Eco.Plugins.EcoLiveDataExporter.Utils;

namespace Eco.Plugins.EcoLiveDataExporter
{
    [ChatCommandHandler]
    public partial class Commands
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

        [ChatCommand("Dumps all available live data to database (this is ran recurrently by a timer)", ChatAuthorizationLevel.User)]
        public static void DumpLiveData(User user)
        {
            ExportUtil.Instance.DumpLiveDataToDatabase();
            user.Player.InfoBoxLocStr("Live data saved in database");
        }

        [ChatCommand("Dump crafting recipes to file", ChatAuthorizationLevel.Admin)]
        public static void DumpRecipes(User user)
        {
            Logger.Debug("Updating recipes file");
            ExportUtil.Instance.DumpRecipesAndItemsToDatabase();
        }

        [ChatCommand("Return dll version", ChatAuthorizationLevel.Admin)]
        public static void LiveDataVersion(User user)
        {
            user.Player.InfoBoxLocStr("Plugin EcoLiveData version is " + EcoLiveData.PluginVersion);
            Logger.Debug("Plugin EcoLiveData version is " + EcoLiveData.PluginVersion);
        }

        //[ChatCommand("Create test user", ChatAuthorizationLevel.Admin)]
        //public static void CreateUser(User user)
        //{
        //    Logger.Debug("Creating test user");
        //    UserManager.GetOrCreateUser("12345", "12345", "FakeUser");
        //}
    }
}
