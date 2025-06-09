using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using CraftSharp.Inventory;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp
{
    /// <summary>
    /// Represents an item stack
    /// </summary>
    public class ItemStack
    {
        private const int DEFAULT_MAX_STACK_SIZE = 64;
        
        /// <summary>
        /// Item Type
        /// </summary>
        public readonly Item ItemType;

        /// <summary>
        /// Item Count
        /// </summary>
        public int Count;

        public int MaxStackSize => TryGetComponent<MaxStackSizeComponent>(
            StructuredComponentIds.MAX_STACK_SIZE_ID, out var maxStackSizeComp) ? maxStackSizeComp.MaxStackSize : DEFAULT_MAX_STACK_SIZE;
        
        /// <summary>
        /// Retrieve item damage from components. Returns 0 if no damage is defined.
        /// </summary>
        public int Damage => TryGetComponent<DamageComponent>(
            StructuredComponentIds.DAMAGE_ID, out var damageComp) ? damageComp.Damage : 0;
        
        /// <summary>
        /// Retrieve max damage from components. Returns 0 if no max damage is defined.
        /// </summary>
        public int MaxDamage => TryGetComponent<MaxDamageComponent>(
            StructuredComponentIds.MAX_DAMAGE_ID, out var maxDamageComp) ? maxDamageComp.MaxDamage : 0;
        
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

        public bool TryGetComponent<T>(ResourceLocation typeId, [MaybeNullWhen(false)] out T tComp) where T : StructuredComponent
        {
            if (Components.TryGetValue(typeId, out var comp) && comp is T c)
            {
                tComp = c;
                return true;
            }
            tComp = null;
            return false;
        }

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
        /// Retrieve item custom name from components. Returns null if no custom name is defined.
        /// </summary>
        public string? CustomName => TryGetComponent<CustomNameComponent>(
            StructuredComponentIds.CUSTOM_NAME_ID, out var customNameComp) ? customNameComp.CustomName : null;

        /// <summary>
        /// Retrieve item lores from components. Returns null if no lore is defined.
        /// </summary>
        public List<string>? Lores => TryGetComponent<LoreComponent>(
            StructuredComponentIds.LORE_ID, out var loreComp) ? loreComp.Lines : null;

        /// <summary>
        /// Check if item is enchanted from components.
        /// </summary>
        public bool IsEnchanted => Components.ContainsKey(StructuredComponentIds.ENCHANTMENTS_ID);

        /// <summary>
        /// Retrieve item enchantments from components. Returns empty list if no enchantment is defined.
        /// </summary>
        public List<Enchantment> Enchantments => TryGetComponent<EnchantmentsComponent>(
            StructuredComponentIds.ENCHANTMENTS_ID, out var enchantmentsComp) ? enchantmentsComp.Enchantments : new();
        
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
                
                if (NBT.TryGetValue("display", out var displayValue) &&
                    displayValue is Dictionary<string, object> displayProperties)
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
                            Components[StructuredComponentIds.CUSTOM_NAME_ID] = customNameComp;
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
                        Components[StructuredComponentIds.LORE_ID] = loreComp;
                    }
                }

                if (NBT.TryGetValue("Damage", out var damageValue) && damageValue is not null)
                {
                    var damage = int.Parse(damageValue.ToString());
                    var damageComp = new DamageComponent(itemPalette, subComponentRegistry)
                    {
                        Damage = damage
                    };
                    Components[StructuredComponentIds.DAMAGE_ID] = damageComp;
                }

                if (NBT.TryGetValue("Enchantments", out var enchantmentsValue) ||
                    NBT.TryGetValue("StoredEnchantments", out enchantmentsValue) && enchantmentsValue is not null)
                {
                    List<Enchantment> enchantments = new();
                    foreach (Dictionary<string, object> enchantment in (object[]) enchantmentsValue)
                    {
                        var level = (short) enchantment["lvl"];
                        var id = ResourceLocation.FromString((string) enchantment["id"]);
                        
                        enchantments.Add(new Enchantment(id, level));
                    }

                    var enchantmentsComp = new EnchantmentsComponent(itemPalette, subComponentRegistry)
                    {
                        NumberOfEnchantments = enchantments.Count,
                        Enchantments = enchantments
                    };
                    Components[StructuredComponentIds.ENCHANTMENTS_ID] = enchantmentsComp;
                }

                if (NBT.TryGetValue("Potion", out var potionValue) && potionValue is not null)
                {
                    var potionContentsComp = new PotionContentsComponent(itemPalette, subComponentRegistry)
                    {
                        HasPotionId = true,
                        PotionId = ResourceLocation.FromString((string) potionValue)
                    };
                    
                    // Check potion NBTs. https://minecraft.wiki/w/Item_format/Before_1.20.5#Potion_Effects
                    // Potion color override
                    if (NBT.TryGetValue("CustomPotionColor", out var value))
                    {
                        potionContentsComp.HasCustomColor = true;
                        potionContentsComp.CustomColor = (int) value;
                    }

                    if (NBT.TryGetValue("CustomPotionEffects ", out value) ||
                        NBT.TryGetValue("custom_potion_effects", out value))
                    {
                        var customEffectList = (object[]) value;

                        potionContentsComp.NumberOfCustomEffects = customEffectList.Length;
                        potionContentsComp.CustomEffects = customEffectList.Select(
                            x => GetPotionEffectSubComponentFromNBT((Dictionary<string, object>) x, subComponentRegistry)).ToList();
                    }
                    
                    Components[StructuredComponentIds.POTION_CONTENTS_ID] = potionContentsComp;
                }
            }
        }
        
        private static PotionEffectSubComponent GetPotionEffectSubComponentFromNBT(Dictionary<string, object> x, SubComponentRegistry subComponentRegistry)
        {
            var effectId = ResourceLocation.FromString((string) x["id"]);
            int amplifier = x.TryGetValue("amplifier", out var value) ? (int) value : 0; // 0 (Level I) by default
            int duration = x.TryGetValue("duration", out value) ? (int) value : 1; // 1 tick by default

            return new PotionEffectSubComponent(subComponentRegistry)
            {
                EffectId = effectId,
                Details = new DetailsSubComponent(subComponentRegistry)
                {
                    Amplifier = amplifier,
                    Duration = duration,
                    Ambient = false,
                    ShowIcon = true,
                    ShowParticles = true,
                    HasHiddenEffects = false
                }
            };
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
