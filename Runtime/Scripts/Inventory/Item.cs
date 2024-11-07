namespace CraftSharp
{
    public record Item
    {
        public const int DEFAULT_STACK_LIMIT = 64;

        public static readonly ResourceLocation AIR_ID = new("air");

        public static readonly Item UNKNOWN  = new(ResourceLocation.INVALID, 64, ItemRarity.Epic, ItemActionType.None, false, null);            // Unsupported item type (Forge mod custom item...)
        public static readonly Item NULL     = new(new ResourceLocation("<null_item>"), 64, ItemRarity.Epic, ItemActionType.None, false, null); // Unspecified item type (Used in the network protocol)

        public readonly ResourceLocation ItemId; // Something like 'minecraft:grass_block'
        public readonly int StackLimit;
        public readonly ItemRarity Rarity;
        public readonly ItemActionType ActionType;
        public readonly bool IsEdible;

        // Associated block for block item
        public readonly ResourceLocation? ItemBlock;

        // Food settings for food item
        public bool? AlwaysEdible;
        public bool? FastFood;

        // Tier level for tiered item
        public TierLevel? TierLevel;

        public bool IsStackable => StackLimit > 1;

        public Item(ResourceLocation itemId, int stackLimit, ItemRarity rarity, ItemActionType actionType, bool edible, ResourceLocation? itemBlock)
        {
            this.ItemId = itemId;
            this.StackLimit = stackLimit;
            this.Rarity = rarity;
            this.ActionType = actionType;
            this.IsEdible = edible;
            this.ItemBlock = itemBlock;
        }

        public override string ToString()
        {
            return ItemId.ToString();
        }
    }
}