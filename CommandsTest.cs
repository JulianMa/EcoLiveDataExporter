using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Plugins.EcoLiveDataExporter.Utils;

using System;
using System.Collections.Generic;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter
{
    public partial class CommandsTest : IChatCommandHandler
    {   
        [ChatCommand("Dumps all available store listings to file", ChatAuthorizationLevel.User)]
        public static void DumpStoreData(User user)
        {
            ExportUtil.Instance.DumpStoreDataToDatabase();
            user.Player.InfoBoxLocStr("Data dump complete");
        }
    }
}
