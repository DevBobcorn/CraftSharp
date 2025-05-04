namespace CraftSharp
{
    public record Item
    {
        public const int DEFAULT_STACK_LIMIT = 64;

        public static readonly ResourceLocation AIR_ID = new("air");

        public static readonly Item UNKNOWN  = new(ResourceLocation.INVALID, 64, ItemRarity.Epic,
            ItemActionType.None, false, null, EquipmentSlot.Mainhand); // Unsupported item type (Forge mod custom item...)
        
        public static readonly Item NULL     = new(new ResourceLocation("<null_item>"), 64,
            ItemRarity.Epic, ItemActionType.None, false, null, EquipmentSlot.Mainhand); // Unspecified item type (Used in the network protocol)

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

        // Tier type for tiered item
        public TierType? TierType;
        
        // Equipment slot for wearable item
        public EquipmentSlot EquipmentSlot;

        public bool IsStackable => StackLimit > 1;

        public Item(ResourceLocation itemId, int stackLimit, ItemRarity rarity, ItemActionType actionType, bool edible,
            ResourceLocation? itemBlock, EquipmentSlot equipmentSlot)
        {
            ItemId = itemId;
            StackLimit = stackLimit;
            Rarity = rarity;
            ActionType = actionType;
            IsEdible = edible;
            ItemBlock = itemBlock;
            EquipmentSlot = equipmentSlot;
        }

        public override string ToString()
        {
            return ItemId.ToString();
        }
    }
}