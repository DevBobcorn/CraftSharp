namespace CraftSharp.Inventory.Recipe
{
    public record BaseRecipeType
    {
        public readonly ResourceLocation TypeId;
        public readonly RecipeExtraDataType ExtraDataType;

        protected BaseRecipeType(ResourceLocation id, RecipeExtraDataType optionType)
        {
            TypeId = id;
            ExtraDataType = optionType;
        }

        public override string ToString()
        {
            return TypeId.ToString();
        }
    }

    /// <summary>
    /// Represents a Minecraft Recipe Type
    /// </summary>
    public record RecipeType<T> : BaseRecipeType where T : RecipeExtraData
    {
        public static readonly RecipeType<CraftingSpecialExtraData> DUMMY_RECIPE_TYPE =
            new(ResourceLocation.INVALID, RecipeExtraDataType.CraftingSpecial);

        public RecipeType(ResourceLocation id, RecipeExtraDataType optionType) : base(id, optionType)
        {

        }
    }
}