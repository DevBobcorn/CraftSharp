using System;
using System.IO;

namespace CraftSharp.Inventory.Recipe
{
    public static class RecipeExtraDataTypeExtension
    {
        public static Type GetDataType(this RecipeExtraDataType enumType)
        {
            return enumType switch
            {
                RecipeExtraDataType.CraftingShapeless     => typeof (CraftingShapelessExtraData),
                RecipeExtraDataType.CraftingShaped        => typeof (CraftingShapedExtraData),
                RecipeExtraDataType.CraftingSpecial       => typeof (CraftingSpecialExtraData),
                RecipeExtraDataType.Cooking               => typeof (CookingExtraData),
                RecipeExtraDataType.Stonecutting          => typeof (StonecuttingExtraData),
                RecipeExtraDataType.Smithing              => typeof (SmithingExtraData),
                RecipeExtraDataType.SmithingTransform     => typeof (SmithingTransformExtraData),
                RecipeExtraDataType.SmithingTrim          => typeof (SmithingTrimExtraData),
                
                _                                         => throw new InvalidDataException($"Extra data type not defined for {nameof (enumType)}"),
            };
        }
    }
}