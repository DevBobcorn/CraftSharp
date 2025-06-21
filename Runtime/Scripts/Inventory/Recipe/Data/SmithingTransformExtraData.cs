#nullable enable
namespace CraftSharp.Inventory.Recipe
{
    /// <summary>
    /// For upgrade smithing
    /// </summary>
    public record SmithingTransformExtraData : RecipeExtraData
    {
        public ItemStack?[] Template;
        public ItemStack?[] Base;
        public ItemStack?[] Addition;
        public ItemStack Result;

        public SmithingTransformExtraData(ItemStack?[] template, ItemStack?[] @base, ItemStack?[] addition, ItemStack result)
        {
            Template = template;
            Base = @base;
            Addition = addition;
            Result = result;
        }
    }
}