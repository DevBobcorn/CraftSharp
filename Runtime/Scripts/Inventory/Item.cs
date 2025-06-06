using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp
{
    public record Item
    {
        public static readonly ResourceLocation AIR_ID = new("air");

        public static readonly Item UNKNOWN  = new(ResourceLocation.INVALID, ItemActionType.None,
            null, EquipmentSlot.Mainhand, new()); // Unsupported item type (Forge mod custom item...)
        
        public static readonly Item NULL     = new(new ResourceLocation("<null_item>"), ItemActionType.None,
            null, EquipmentSlot.Mainhand, new()); // Unspecified item type (Used in the network protocol)

        public readonly ResourceLocation ItemId; // Something like 'minecraft:grass_block'
        public readonly ItemActionType ActionType;

        // Associated block for block item
        public readonly ResourceLocation? ItemBlock;

        // Tier type for tiered item
        public TierType? TierType;
        
        // Equipment slot for wearable item
        public readonly EquipmentSlot EquipmentSlot;

        public readonly Dictionary<ResourceLocation, StructuredComponent> DefaultComponents;

        public Item(ResourceLocation itemId, ItemActionType actionType,
            ResourceLocation? itemBlock, EquipmentSlot equipmentSlot,
            Dictionary<ResourceLocation, StructuredComponent> defaultComponents)
        {
            ItemId = itemId;
            ActionType = actionType;
            ItemBlock = itemBlock;
            EquipmentSlot = equipmentSlot;
            DefaultComponents = defaultComponents;
        }

        public override string ToString()
        {
            return ItemId.ToString();
        }
    }
}