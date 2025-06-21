#nullable enable
namespace CraftSharp.Inventory.Recipe
{
    public record CookingExtraData : RecipeExtraData
    {
        public string Group;
        public CookingRecipeCategory Category; // Individual definition for each recipe since 1.19.3
        public ItemStack?[] Ingredient;
        public ItemStack Result;
        public float Experience;
        public int CookingTime;
        
        public CookingExtraData(string group, CookingRecipeCategory category,
            ItemStack?[] ingredient, ItemStack result, float experience, int cookingTime)
        {
            Group = group;
            Category = category;
            Ingredient = ingredient;
            Result = result;
            Experience = experience;
            CookingTime = cookingTime;
        }
    }
}