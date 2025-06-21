#nullable enable
namespace CraftSharp.Inventory.Recipe
{
    /// <summary>
    /// For legacy smithing(without a template item)
    /// </summary>
    public record SmithingExtraData : RecipeExtraData
    {
        public ItemStack?[] Base;
        public ItemStack?[] Addition;
        public ItemStack Result;

        public SmithingExtraData(ItemStack?[] @base, ItemStack?[] addition, ItemStack result)
        {
            Base = @base;
            Addition = addition;
            Result = result;
        }
    }
}