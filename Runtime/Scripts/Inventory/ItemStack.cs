using System.Collections.Generic;
using System.Text;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp
{
    /// <summary>
    /// Represents an item stack
    /// </summary>
    public class ItemStack
    {
        public const int DEFAULT_STACK_LIMIT = 64;
        
        /// <summary>
        /// Item Type
        /// </summary>
        public readonly Item ItemType;

        /// <summary>
        /// Item Count
        /// </summary>
        public int Count;

        public int StackLimit
        {
            get
            {
                if (Components.TryGetValue(StructuredComponentIds.MAX_STACK_SIZE_ID, out var comp) &&
                    comp is MaxStackSizeComponent maxStackSizeComp)
                {
                    return maxStackSizeComp.MaxStackSize;
                }
                return DEFAULT_STACK_LIMIT;
            }
        }
        
        public int MaxDurability
        {
            get
            {
                if (Components.TryGetValue(StructuredComponentIds.MAX_DAMAGE_ID, out var comp) &&
                    comp is MaxDamageComponent maxDamageComp)
                {
                    return maxDamageComp.MaxDamage;
                }
                return 0; // Item cannot take damage
            }
        }
        
        public ItemRarity Rarity
        {
            get
            {
                if (Components.TryGetValue(StructuredComponentIds.RARITY_ID, out var comp) &&
                    comp is RarityComponent rarityComp)
                {
                    return rarityComp.Rarity;
                }
                return ItemRarity.Common;
            }
        }
        
        public bool IsStackable => StackLimit > 1;
        public bool IsDepletable => MaxDurability > 0;

        #nullable enable
        /// <summary>
        /// Item Metadata
        /// </summary>
        public readonly Dictionary<string, object>? NBT;

        /// <summary>
        /// Item Components
        /// </summary>
        public readonly Dictionary<ResourceLocation, StructuredComponent> Components = new();

        /// <summary>
        /// Create an item with ItemType, Count and Metadata
        /// </summary>
        /// <param name="itemType">Type of the item</param>
        /// <param name="count">Item Count</param>
        /// <param name="nbt">Item Metadata</param>
        public ItemStack(Item itemType, int count, Dictionary<string, object>? nbt = null)
        {
            ItemType = itemType;
            Count = count;
            NBT = nbt;

            // Apply default components of this item type
            foreach (var defaultComponent in itemType.DefaultComponents)
            {
                Components.Add(defaultComponent.Key, defaultComponent.Value);
            }
        }
        #nullable disable

        /// <summary>
        /// Check if the item slot is empty
        /// </summary>
        /// <returns>TRUE if the item is empty</returns>
        public bool IsEmpty => ItemType.ItemId == Item.AIR_ID || Count == 0;

        /// <summary>
        /// Retrieve item display name from NBT properties. NULL if no display name is defined.
        /// </summary>
        public string DisplayName
        {
            get
            {
                if (NBT != null && NBT.TryGetValue("display", out var displayValue))
                {
                    if (displayValue is Dictionary<string, object> displayProperties && displayProperties.ContainsKey("Name"))
                    {
                        string displayName = displayProperties["Name"] as string;
                        if (!string.IsNullOrEmpty(displayName))
                            return displayProperties["Name"].ToString();
                    }
                }
                return null;
            }
        }
        
        /// <summary>
        /// Check item enchanted status name from NBT properties.
        /// </summary>
        public bool IsEnchanted => NBT != null && (NBT.ContainsKey("Enchantments") || NBT.ContainsKey("StoredEnchantments"));

        /// <summary>
        /// Retrieve item lores from NBT properties. Returns null if no lores is defined.
        /// </summary>
        public object[] Lores
        {
            get
            {
                if (NBT != null && NBT.TryGetValue("display", out var displayValue))
                {
                    if (displayValue is Dictionary<string, object> displayProperties && displayProperties.TryGetValue("Lore", out var loreValue))
                    {
                        return loreValue as object[];
                    }
                }
                return null;
            }
        }
        
        /// <summary>
        /// Retrieve item damage from NBT properties. Returns 0 if no damage is defined.
        /// </summary>
        public int Damage
        {
            get
            {
                if (NBT != null && NBT.TryGetValue("Damage", out var damageValue))
                {
                    if (damageValue != null)
                    {
                        return int.Parse(damageValue.ToString());
                    }
                }
                return 0;
            }
        }
        
        public static ItemStack FromJson(Json.JSONData data)
        {
            var typeId = data.Properties.TryGetValue("id", out var val) || data.Properties.TryGetValue("item_id", out val) ?
                ResourceLocation.FromString(val.StringValue) : ResourceLocation.INVALID;
            
            var count = data.Properties.TryGetValue("count", out val) || data.Properties.TryGetValue("Count", out val) ?
                int.Parse(val.StringValue) : 1; // Count is 1 by default
            
            // TODO: Parse NBTs and components

            return new ItemStack(ItemPalette.INSTANCE.GetById(typeId), count);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("x{0,-2} {1}", Count, ItemType.ToString());
            string displayName = DisplayName;
            if (!string.IsNullOrEmpty(displayName))
            {
                sb.AppendFormat(" - {0}§8", displayName);
            }
            int damage = Damage;
            if (damage != 0)
            {
                sb.AppendFormat(" | Damage: {0}", damage);
            }
            return sb.ToString();
        }
    }
}
