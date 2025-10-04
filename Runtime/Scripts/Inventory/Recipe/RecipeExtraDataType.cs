namespace CraftSharp.Inventory.Recipe
{
    /// <summary>
    /// See https://minecraft.wiki/w/Java_Edition_protocol?oldid=2772660#Declare_Recipes
    /// </summary>
    public enum RecipeExtraDataType
    {
        CraftingShaped,
        CraftingShapeless,
        CraftingSpecial,
        CraftingTransmute, // Introduced in 1.21.2
        Cooking,           // Includes Smelting/Blasting/Smoking/Campfire Cooking
        Stonecutting,
        Smithing,          // Legacy Smithing(without a template item)
        SmithingTransform,
        SmithingTrim
    }
}