using System.Collections.Generic;

namespace CraftSharp
{
    /// <summary>
    /// Represents a Minecraft BlockEntity Type
    /// </summary>
    public record BlockEntityType
    {
        public static readonly BlockEntityType DUMMY_BLOCK_ENTITY_TYPE = new(ResourceLocation.INVALID, new() { ResourceLocation.INVALID });

        public readonly ResourceLocation BlockEntityId;

        public readonly HashSet<ResourceLocation> Blocks;

        public BlockEntityType(ResourceLocation id, HashSet<ResourceLocation> blocks)
        {
            BlockEntityId = id;
            Blocks = blocks;
        }

        public override string ToString()
        {
            return BlockEntityId.ToString();
        }
    }
}