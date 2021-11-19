using System;
using System.Threading.Tasks;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    public class ExportUtil
    {
        private DateTime LastExport = DateTime.MinValue;
        private Task ThrotleTask = Task.CompletedTask;
        private ExportUtil() { }
        public static ExportUtil Instance = new ExportUtil();
        public void DumpLiveDataToDatabase()
        {
            var timeSinceLastExport = DateTime.Now - this.LastExport;
            // Only allow exports every x configured ammount of minutes
            if (timeSinceLastExport > TimeSpan.FromMinutes(Config.Data.ThrotleDbUpdatesForMinutes))
            {
                ThrotleTask = ExportLiveData(true);
            } else if(ThrotleTask.IsCompleted)
            {
                Logger.Debug("Started a thread to export data when throtle period ends");
                // Start a task to dump store data right after the throtle period expired (unless task is already created)
                ThrotleTask = Task.Delay(TimeSpan.FromMinutes(Config.Data.ThrotleDbUpdatesForMinutes) - timeSinceLastExport).ContinueWith((tsk) => ExportLiveData(true));
            } else
            {
                Logger.Debug("Data export not queued since there is already an active data export queued up");
            }
        }

        // Exports data configured in "Recurrent data export" config section
        // Can be called by user command directly or by a recurrent timer function
        public async Task ExportLiveData(bool byCommand = false)
        {
            LastExport = DateTime.Now;

            await ExportLiveStoreData();
            await ExportLiveTradesData();
            await ExportLiveCraftingTablesData();

            EcoLiveData.Status = $"Live data exported at {DateTime.Now.ToShortTimeString()}";
        }

        private async Task ExportLiveStoreData(bool byCommand = false)
        {
            if (Config.Data.SaveStoreData)
            {
                // Overrides current store data in file
                Logger.Debug("Exporting store data");
                var storesString = TradeUtil.GetStoresString();
                if (storesString == null || storesString.Length == 0)
                {
                    return;
                }

                await DataExporter.WriteToFile("stores", "/", storesString[0]);
                await JsonStorageExporter.WriteToJsonStorage("Stores", storesString[0]);
                Logger.Debug($"Store data exported at {DateTime.Now.ToShortTimeString()}");

                if (!byCommand && Config.Data.SaveHistoricalStoreData)
                {
                    Logger.Debug("Saving stores data to history file");
                    await DataExporter.AddToFile("storesHistoric", "/", storesString[1]);
                }
                Logger.Debug("Finished UpdateStoreData");
            }
        }

        public async Task ExportLiveTradesData()
        {
            if (Config.Data.SaveHistoricalTradesData)
            {
                Logger.Debug("Exporting trades data");
                var tradesString = TradeUtil.GetTradesString();
                if (tradesString == null || tradesString.Length == 0)
                {
                    return;
                }
                try
                {
                    Logger.Debug("Saving trades to file");
                    await DataExporter.AddToFile("trades", "/", tradesString);
                }
                catch (Exception e)
                {
                    Logger.Error($"Got an exception trying to export trades data: \n {e}");
                }
                Logger.Debug("Finished exporting trades data");
            }
        }
        public async Task ExportLiveCraftingTablesData()
        {
            if (Config.Data.SaveCraftingTablesData)
            {
                // Overrides current crafting tables data in file
                Logger.Debug("Exporting crafting tables data");
                var craftingTablesString = RecipeUtil.GetCraftingTablesString();
                if (craftingTablesString == null || craftingTablesString.Length == 0)
                {
                    return;
                }

                await DataExporter.WriteToFile("craftingTables", "/", craftingTablesString);
                await JsonStorageExporter.WriteToJsonStorage("CraftingTables", craftingTablesString);
                Logger.Debug($"Finished exporting crafting tables data");
            }
        }

        public void DumpRecipesAndItemsToDatabase()
        {
            if (ThrotleTask.IsCompleted)
            {
                // Start a task to dump store data right after the throtle period expired (unless task is already created)
                Logger.Debug($"Exporting Recipes");
                ThrotleTask = ExportRecipes().ContinueWith((tsk) => ExportItems());
            } else
            {
                Logger.Debug($"Not exporting recipes since there is already an export in progress");
            }
        }

        public async Task ExportRecipes()
        {
            // Overrides current recipes in file
            var recipesString = RecipeUtil.GetRecipesString();
            if (recipesString == null || recipesString.Length == 0)
            {
                return;
            }

            await DataExporter.WriteToFile("recipes", "/", recipesString);
            await JsonStorageExporter.WriteToJsonStorage("Recipes", recipesString);
            Logger.Debug($"Recipes exported at {DateTime.Now.ToShortTimeString()}");
        }

        public async Task ExportItems()
        {
            // Overrides current recipes in file
            var tagsString = RecipeUtil.GetCraftableTagItems();
            if (tagsString == null || tagsString.Length == 0)
            {
                return;
            }

            await DataExporter.WriteToFile("tags", "/", tagsString);
            await JsonStorageExporter.WriteToJsonStorage("Tags", tagsString);
            Logger.Debug($"Item tags exported at {DateTime.Now.ToShortTimeString()}");
        }
    }
}
