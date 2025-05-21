using System.Collections.Generic;
using System.Text;

namespace CraftSharp
{
    /// <summary>
    /// Represents an item stack
    /// </summary>
    public class ItemStack
    {
        /// <summary>
        /// Item Type
        /// </summary>
        public readonly Item ItemType;

        /// <summary>
        /// Item Count
        /// </summary>
        public int Count;

        #nullable enable
        /// <summary>
        /// Item Metadata
        /// </summary>
        public readonly Dictionary<string, object>? NBT;

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
