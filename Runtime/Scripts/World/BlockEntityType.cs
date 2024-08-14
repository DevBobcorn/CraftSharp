namespace CraftSharp
{
    /// <summary>
    /// Represents a Minecraft BlockEntity Type
    /// </summary>
    public record BlockEntityType
    {
        public static readonly BlockEntityType DUMMY_BLOCK_ENTITY_TYPE = new(ResourceLocation.INVALID);

        public ResourceLocation BlockEntityId { get; }

        public BlockEntityType(ResourceLocation id)
        {
            BlockEntityId = id;
        }

        public override string ToString()
        {
            return BlockEntityId.ToString();
        }
    }
}