using System.Collections.Generic;
using System.Linq;
using System.Text;
using CraftSharp.Inventory;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp
{
    /// <summary>
    /// Represents an item stack
    /// </summary>
    public class ItemStack
    {
        private const int DEFAULT_STACK_LIMIT = 64;
        
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
        
        /// <summary>
        /// Retrieve item damage from components. Returns 0 if no damage is defined.
        /// </summary>
        public int Damage
        {
            get
            {
                if (Components.TryGetValue(StructuredComponentIds.DAMAGE_ID, out var comp) &&
                    comp is DamageComponent damageComp)
                {
                    return damageComp.Damage;
                }
                return 0; // Item did not take damage
            }
        }
        
        /// <summary>
        /// Retrieve max damage from components. Returns 0 if no max damage is defined.
        /// </summary>
        public int MaxDamage
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
        
        /// <summary>
        /// Retrieve rarity from components. Returns common if no rarity is defined.
        /// </summary>
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

        public bool IsDepletable => MaxDamage > 0;

        #nullable enable
        /// <summary>
        /// Item Metadata
        /// </summary>
        public readonly Dictionary<string, object>? NBT;

        /// <summary>
        /// Item Components
        /// </summary>
        public readonly Dictionary<ResourceLocation, StructuredComponent> Components = new();

        public void ApplyComponents(Dictionary<ResourceLocation, StructuredComponent> componentsToAdd,
            List<ResourceLocation> componentsToRemove)
        {
            foreach (var component in componentsToAdd)
            {
                Components[component.Key] = component.Value; // Add or overwrite
            }
            
            foreach (var componentId in componentsToRemove)
            {
                Components.Remove(componentId);
            }
        }
        
        /// <summary>
        /// Check if the item stack is empty
        /// </summary>
        /// <returns>TRUE if the item stack is empty</returns>
        public bool IsEmpty => ItemType.ItemId == Item.AIR_ID || Count == 0;

        /// <summary>
        /// Retrieve item custom name from components. NULL if no custom name is defined.
        /// </summary>
        public string? CustomName
        {
            get
            {
                if (Components.TryGetValue(StructuredComponentIds.CUSTOM_NAME_ID, out var comp) &&
                    comp is CustomNameComponent customNameComp)
                {
                    return customNameComp.CustomName;
                }
                return null;
            }
        }
        
        /// <summary>
        /// Retrieve item lores from components. Returns null if no lores is defined.
        /// </summary>
        public List<string>? Lores
        {
            get
            {
                if (Components.TryGetValue(StructuredComponentIds.LORE_ID, out var comp) &&
                    comp is LoreComponent loreComp)
                {
                    return loreComp.Lines;
                }
                return null;
            }
        }

        /// <summary>
        /// Check if item is enchanted from components.
        /// </summary>
        public bool IsEnchanted => Components.ContainsKey(StructuredComponentIds.ENCHANTMENTS_ID);

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

            // Read components from legacy nbt
            if (NBT is not null)
            {
                var itemPalette = ItemPalette.INSTANCE;
                var subComponentRegistry = itemPalette.ComponentRegistry.SubComponentRegistry;
                
                if (NBT.TryGetValue("display", out var displayValue))
                {
                    if (displayValue is Dictionary<string, object> displayProperties)
                    {
                        if (displayProperties.TryGetValue("Name", out var nameValue)) // Custom Name
                        {
                            var displayName = (nameValue as string)!;
                            if (!string.IsNullOrEmpty(displayName))
                            {
                                var customNameComp = new CustomNameComponent(itemPalette, subComponentRegistry)
                                {
                                    CustomName = displayName
                                };
                                Components.Add(StructuredComponentIds.CUSTOM_NAME_ID, customNameComp);
                            }
                        }
                        
                        if (displayProperties.TryGetValue("Lore", out var loreValue)) // Lore
                        {
                            var displayLore = (loreValue as object[])!
                                .Select(x => x.ToString()).ToList();
                            var loreComp = new LoreComponent(itemPalette, subComponentRegistry)
                            {
                                NumberOfLines = displayLore.Count,
                                Lines = displayLore
                            };
                            Components.Add(StructuredComponentIds.LORE_ID, loreComp);
                        }
                    }
                }

                if (NBT.TryGetValue("Damage", out var damageValue))
                {
                    if (damageValue != null)
                    {
                        var damage = int.Parse(damageValue.ToString());
                        var damageComp = new DamageComponent(itemPalette, subComponentRegistry)
                        {
                            Damage = damage
                        };
                        Components.Add(StructuredComponentIds.DAMAGE_ID, damageComp);
                    }
                }

                if (NBT.TryGetValue("Enchantments", out var enchantmentsValue) ||
                    NBT.TryGetValue("StoredEnchantments", out enchantmentsValue))
                {
                    if (enchantmentsValue != null)
                    {
                        List<Enchantment> enchantments = new();
                        foreach (Dictionary<string, object> enchantment in (object[]) enchantmentsValue)
                        {
                            // TODO: Fill in the data
                        }

                        var enchantmentsComp = new EnchantmentsComponent(itemPalette, subComponentRegistry)
                        {
                            Enchantments = enchantments
                        };
                        Components.Add(StructuredComponentIds.ENCHANTMENTS_ID, enchantmentsComp);
                    }
                }
            }
        }
        #nullable disable
        
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
            string displayName = CustomName;
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
