namespace CraftSharp.Inventory.Recipe
{
    public record CraftingSpecialExtraData : RecipeExtraData
    {
        public CraftingRecipeCategory Category;

        public CraftingSpecialExtraData(CraftingRecipeCategory category)
        {
            Category = category;
        }
    }
}