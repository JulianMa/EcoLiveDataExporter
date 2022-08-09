using Eco.Gameplay.Items;
using System.Collections.Generic;
using System.Linq;
using Eco.Gameplay.DynamicValues;
using Eco.Plugins.EcoLiveDataExporter.Utils;
using System;
using Eco.Gameplay.Modules;
using System.Reflection;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonRecipe
    {
        public string Key { get; set; }
        public string Untranslated { get; set; }
        public float BaseCraftTime { get; set; }
        public float BaseLaborCost { get; set; }
        public float BaseXPGain { get; set; }
        public string CraftingTable { get; set; }
        public bool CraftingTableCanUseModules { get; set; }
        public string DefaultVariant { get; set; }
        public int NumberOfVariants { get; set; }
        public List<JsonSkillNeeds> SkillNeeds { get; set; }
        public List<JsonRecipeVariant> Variants { get; set; }


        public JsonRecipe(RecipeFamily recipe)
        {
            Key = recipe.RecipeName;
            Untranslated = recipe.DisplayName.NotTranslated;
            BaseCraftTime = recipe.CraftMinutes?.GetBaseValue ?? 0;
            BaseLaborCost = recipe.Labor;
            BaseXPGain = recipe.ExperienceOnCraft;
            CraftingTable = recipe.CraftingTable.DisplayName;
            CraftingTableCanUseModules = recipe.CraftingTable?.GetType()?.IsDefined(typeof(AllowPluginModulesAttribute), false) ?? false;
            DefaultVariant = recipe.DefaultRecipe.DisplayName;
            NumberOfVariants = recipe.Recipes.Count;
            SkillNeeds = recipe.RequiredSkills.Select(t => new JsonSkillNeeds { Skill = t.SkillItem.DisplayName, Level = t.Level }).ToList();
            Variants = recipe.Recipes.Select(recipe => new JsonRecipeVariant {
                Key = recipe.Name,
                Name = recipe.DisplayName.NotTranslated,
                Ingredients = recipe.Ingredients.Select(ingredient => new JsonRecipeIngredient {
                    IsSpecificItem = ingredient.IsSpecificItem,
                    Tag = ingredient.Tag?.DisplayName,
                    Name = ingredient.Item?.DisplayName.NotTranslated ?? "",
                    Ammount = ingredient.Quantity?.GetBaseValue ?? 0,
                    IsStatic = ingredient.Quantity is ConstantValue
                }).ToList(),
                Products = recipe.Items.Select(prod => new JsonRecipeProduct
                {
                    Name = prod.Item.DisplayName,
                    Ammount = prod.Quantity.GetBaseValue,
                }).ToList()
            }).ToList();
        }
    }
}
