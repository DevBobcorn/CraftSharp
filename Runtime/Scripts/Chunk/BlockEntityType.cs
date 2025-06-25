using System.Collections.Generic;

namespace CraftSharp
{
    /// <summary>
    /// Represents a Minecraft BlockEntity Type
    /// </summary>
    public record BlockEntityType
    {
        public static readonly ResourceLocation CHEST_ID            = new("chest");
        public static readonly ResourceLocation ENDER_CHEST_ID      = new("ender_chest");
        public static readonly ResourceLocation TRAPPED_CHEST_ID    = new("trapped_chest");
        public static readonly ResourceLocation SIGN_ID             = new("sign");
        public static readonly ResourceLocation MOB_SPAWNER_ID      = new("mob_spawner");
        public static readonly ResourceLocation PISTON_ID           = new("piston");
        public static readonly ResourceLocation ENCHANTING_TABLE_ID = new("enchanting_table");
        public static readonly ResourceLocation BANNER_ID           = new("banner");
        public static readonly ResourceLocation SHULKER_BOX_ID      = new("shulker_box");
        public static readonly ResourceLocation CONDUIT_ID          = new("conduit");
        public static readonly ResourceLocation LECTERN_ID          = new("lectern");
        public static readonly ResourceLocation CAMPFIRE_ID         = new("campfire");
        
        public static readonly BlockEntityType DUMMY_BLOCK_ENTITY_TYPE = new(ResourceLocation.INVALID, new() { ResourceLocation.INVALID });

        public readonly ResourceLocation TypeId;

        public readonly HashSet<ResourceLocation> Blocks;

        public BlockEntityType(ResourceLocation id, HashSet<ResourceLocation> blocks)
        {
            TypeId = id;
            Blocks = blocks;
        }

        public override string ToString()
        {
            return TypeId.ToString();
        }
    }
}