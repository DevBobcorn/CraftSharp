#nullable enable
namespace CraftSharp.Inventory.Recipe
{
    public record StonecuttingExtraData : RecipeExtraData
    {
        public string Group;
        public ItemStack?[] Ingredient;
        public ItemStack Result;
        
        public StonecuttingExtraData(string group, ItemStack?[] ingredient, ItemStack result)
        {
            Group = group;
            Ingredient = ingredient;
            Result = result;
        }
    }
}