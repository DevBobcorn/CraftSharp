#nullable enable
using System.Collections.Generic;

namespace CraftSharp.Inventory.Recipe
{
    public record CraftingShapedExtraData : RecipeExtraData
    {
        public string Group;
        public CraftingRecipeCategory Category; // Individual definition for each recipe since 1.19.3
        public int Width;
        public int Height;
        public List<ItemStack?[]> Ingredients; // Length is Width * Height. Indexed by x + (y * Width)
        public ItemStack Result;
        public bool ShowNotification; // Added in 1.19.4

        public CraftingShapedExtraData(string group, CraftingRecipeCategory category, int width, int height,
            List<ItemStack?[]> ingredients, ItemStack result, bool showNotification)
        {
            Group = group;
            Category = category;
            Width = width;
            Height = height;
            Ingredients = ingredients;
            Result = result;
            ShowNotification = showNotification;
        }
    }
}