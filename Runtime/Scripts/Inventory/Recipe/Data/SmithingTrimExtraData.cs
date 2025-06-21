#nullable enable
namespace CraftSharp.Inventory.Recipe
{
    /// <summary>
    /// For trim smithing
    /// </summary>
    public record SmithingTrimExtraData : RecipeExtraData
    {
        public ItemStack?[] Template;
        public ItemStack?[] Base;
        public ItemStack?[] Addition;

        public SmithingTrimExtraData(ItemStack?[] template, ItemStack?[] @base, ItemStack?[] addition)
        {
            Template = template;
            Base = @base;
            Addition = addition;
        }
    }
}