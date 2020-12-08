using Eco.Gameplay.GameActions;
using Eco.Plugins.EcoLiveDataExporter.Utils;

using System;
using System.Collections.Generic;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.ActionsProcessor
{
    public class InventoryActionProcess
    {
        public static void Process(InventoryAction inventoryAction)
        {
            if (inventoryAction == null) return;

            Logger.Debug($"Inventory action detected from {inventoryAction.Citizen.Name}: {inventoryAction.ItemsMoved} {inventoryAction.ItemUsed.Name}");
        }
    }
}
