using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace CraftSharp.Inventory.Recipe
{
    public class RecipeTypePalette : IdentifierPalette<BaseRecipeType>
    {
        private static readonly char SP = Path.DirectorySeparatorChar;
        
        public static readonly RecipeTypePalette INSTANCE = new();
        protected override string Name => "RecipeType Palette";
        protected override BaseRecipeType UnknownObject => RecipeType<CraftingSpecialExtraData>.DUMMY_RECIPE_TYPE;

        public static readonly ResourceLocation CRAFTING_SHAPED    = new("crafting_shaped");
        public static readonly ResourceLocation CRAFTING_SHAPELESS = new("crafting_shapeless");
        public static readonly ResourceLocation SMELTING           = new("smelting");
        public static readonly ResourceLocation BLASTING           = new("blasting");
        public static readonly ResourceLocation SMOKING            = new("smoking");
        public static readonly ResourceLocation STONECUTTING       = new("stonecutting");
        public static readonly ResourceLocation SMITHING           = new("smithing"); // Legacy smithing
        public static readonly ResourceLocation SMITHING_TRANSFORM = new("smithing_transform");
        public static readonly ResourceLocation SMITHING_TRIM      = new("smithing_trim");
        
        public static readonly HashSet<ResourceLocation> CRAFTING_SPECIALS = new()
        {
            new("crafting_special_armordye"),
            new("crafting_special_bookcloning"),
            new("crafting_special_mapcloning"),
            new("crafting_special_mapextending"),
            new("crafting_special_firework_rocket"),
            new("crafting_special_firework_star"),
            new("crafting_special_firework_star_fade"),
            new("crafting_special_tippedarrow"),
            new("crafting_special_bannerduplicate"),
            new("crafting_special_shielddecoration"),
            new("crafting_special_shulkerboxcoloring"), // Removed in 1.21.2
            new("crafting_special_suspiciousstew"),
            new("crafting_special_repairitem"),
            new("crafting_decorated_pot"),
        };

        /// <summary>
        /// Load recipe data from external files.
        /// </summary>
        /// <param name="dataVersion">Recipe data version</param>
        /// <param name="flag">Data load flag</param>
        public void PrepareData(string dataVersion, DataLoadFlag flag)
        {
            // Clear loaded stuff...
            ClearEntries();

            var recipeTypePath = PathHelper.GetExtraDataFile($"inventories{SP}recipe_types-{dataVersion}.json");

            if (!File.Exists(recipeTypePath))
            {
                Debug.LogWarning("Recipe data not complete!");
                flag.Finished = true;
                flag.Failed = true;
                return;
            }

            try
            {
                var recipeTypes = Json.ParseJson(File.ReadAllText(recipeTypePath, Encoding.UTF8));

                foreach (var (recipeKey, recipeDef) in recipeTypes.Properties)
                {
                    if (int.TryParse(recipeDef.Properties["protocol_id"].StringValue, out int numId))
                    {
                        var recipeTypeId = ResourceLocation.FromString(recipeKey);

                        var extraDataValue = recipeDef.Properties["extra_data"];

                        var optionType = extraDataValue.StringValue switch
                        {
                            "crafting_shaped" => RecipeExtraDataType.CraftingShaped,
                            "crafting_shapeless" => RecipeExtraDataType.CraftingShapeless,
                            "crafting_special" => RecipeExtraDataType.CraftingSpecial,
                            "cooking" => RecipeExtraDataType.Cooking,
                            "stonecutting" => RecipeExtraDataType.Stonecutting,
                            "smithing" => RecipeExtraDataType.Smithing,
                            "smithing_transform" => RecipeExtraDataType.SmithingTransform,
                            "smithing_trim" => RecipeExtraDataType.SmithingTrim,

                            _ => throw new InvalidDataException($"Undefined recipe extra data type: {extraDataValue.StringValue}"),
                        };

                        var optionClassType = optionType.GetDataType();
                        var recipeClassType = typeof(RecipeType<>);
                        var newRecipeClassType = recipeClassType.MakeGenericType(optionClassType);

                        var newRecipeType = (BaseRecipeType) Activator.CreateInstance(
                            newRecipeClassType, recipeTypeId, optionType);

                        AddEntry(recipeTypeId, numId, newRecipeType);
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid numeral recipe type key [{recipeKey}]");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading recipe types: {e.Message}");
                flag.Failed = true;
            }
            finally
            {
                FreezeEntries();
                flag.Finished = true;
            }
        }
    }
}