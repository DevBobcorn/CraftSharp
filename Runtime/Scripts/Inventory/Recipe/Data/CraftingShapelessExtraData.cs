#nullable enable
using System.Collections.Generic;

namespace CraftSharp.Inventory.Recipe
{
    public record CraftingShapelessExtraData : RecipeExtraData
    {
        public string Group;
        public CraftingRecipeCategory Category; // Individual definition for each recipe since 1.19.3
        public int IngredientCount;
        public List<ItemStack?[]> Ingredients;
        public ItemStack Result;

        public CraftingShapelessExtraData(string group, CraftingRecipeCategory category,
            int ingredientCount, List<ItemStack?[]> ingredients, ItemStack result)
        {
            Group = group;
            Category = category;
            IngredientCount = ingredientCount;
            Ingredients = ingredients;
            Result = result;
        }
    }
}