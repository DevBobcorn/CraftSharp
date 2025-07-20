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

        public bool IsDamageable => MaxDamage > 0;
        
        public bool IsStackable => MaxStackSize > 1;

        #nullable enable
        /// <summary>
        /// Item Metadata
        /// </summary>
        public readonly Dictionary<string, object>? NBT;

        /// <summary>
        /// Item Components
        /// </summary>
        public readonly Dictionary<ResourceLocation, StructuredComponent> Components = new();

        public Dictionary<int, byte[]>? ReceivedComponentsToAdd;
        public HashSet<int>? ReceivedComponentsToRemove;

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
        /// <br/>
        /// Note that even if enchantments/stored enchantments component exist, they might be empty,
        /// which means the item is NOT enchanted. So here we need to check the actual count.
        /// </summary>
        public bool IsEnchanted => (TryGetComponent<EnchantmentsComponent>(
            StructuredComponentIds.ENCHANTMENTS_ID, out var enchantmentsComp) && enchantmentsComp.NumberOfEnchantments > 0) ||
                (TryGetComponent<StoredEnchantmentsComponent>(
                    StructuredComponentIds.STORED_ENCHANTMENTS_ID, out var storedEnchantmentsComp) && storedEnchantmentsComp.NumberOfEnchantments > 0);

        /// <summary>
        /// Retrieve item enchantments from components. Returns empty list if no enchantment is defined.
        /// </summary>
        public List<Enchantment> Enchantments => TryGetComponent<EnchantmentsComponent>(
            StructuredComponentIds.ENCHANTMENTS_ID, out var enchantmentsComp) ? enchantmentsComp.Enchantments :
                TryGetComponent<StoredEnchantmentsComponent>(
                    StructuredComponentIds.STORED_ENCHANTMENTS_ID, out var storedEnchantmentsComp) ? storedEnchantmentsComp.Enchantments : new();

        /// <summary>
        /// Reads an item from nbt as a slot in a container.
        /// Returns the item stack and the slot it is in.
        /// </summary>
        public static (int, ItemStack?) FromSlotNBT(Dictionary<string, object> nbt)
        {
            int count = nbt.TryGetValue("Count", out var countVal) ? int.Parse(countVal!.ToString()) : 1;
            string idStr = nbt.TryGetValue("id", out var idVal) ? idVal!.ToString() : string.Empty;
            if (count <= 0 && string.IsNullOrEmpty(idStr))
            {
                return (-1, null);
            }

            var id = ResourceLocation.FromString(idVal!.ToString());
            var type = ItemPalette.INSTANCE.GetById(id);
            int slot = nbt.TryGetValue("Slot", out var slotVal) ? int.Parse(slotVal!.ToString()) : -1;
            var tag = nbt.GetValueOrDefault("tag") as Dictionary<string, object>;

            return (slot, new ItemStack(type, count, tag));
        }
        
        /// <summary>
        /// Create an item stack with ItemType, Count and Metadata
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

                // Post processing for legacy NBT items (Doesn't affect NBT data)

                if (IsEnchanted) // Enchantments increase the rarity of the item, from Common or Uncommon to Rare, or from Rare to Epic
                {
                    var oldRarity = Rarity;
                    var newRarity = oldRarity switch
                    {
                        ItemRarity.Common or ItemRarity.Uncommon => ItemRarity.Rare,
                        ItemRarity.Rare => ItemRarity.Epic,
                        _ => oldRarity
                    };

                    Components[StructuredComponentIds.RARITY_ID] = new RarityComponent(itemPalette, subComponentRegistry)
                    {
                        Rarity = newRarity
                    };
                }

                if (NBT.TryGetValue("BlockEntityTag", out var blockEntityTagObj) &&
                    blockEntityTagObj is Dictionary<string, object?> blockEntityTag)
                {
                    if (blockEntityTag.TryGetValue("patterns", out var patterns) && patterns is object[] patternList)
                    {
                        var bannerPatternsComp = new BannerPatternsComponent(itemPalette, subComponentRegistry);
                        
                        // New format. e.g. "{patterns:{pattern:"right_stripe",color:"white"} }"
                        foreach (Dictionary<string, object> patternData in patternList)
                        {
                            var color = CommonColorsHelper.GetCommonColor((string) patternData["color"]);
                            ResourceLocation patternId;
                            BannerLayer bannerLayer;

                            var pattern = patternData["pattern"];
                            if (pattern is string patternStr) // Given as an id
                            {
                                patternId = ResourceLocation.FromString(patternStr);
                                bannerLayer = new()
                                {
                                    PatternType = BannerPatternType.GetIndexFromId(patternId),
                                    DyeColor = color
                                };
                            }
                            else if (pattern is Dictionary<string, object> patternDef) // Given as an inline definition
                            {
                                patternId = ResourceLocation.FromString((string) patternDef["asset_id"]);
                                var translationKey = (string) patternDef.GetValueOrDefault("translation_key", string.Empty);
                                var newEntry = new BannerPatternType(patternId, translationKey);
                                BannerPatternPalette.INSTANCE.AddOrUpdateEntry(patternId, newEntry);
                                
                                bannerLayer = new()
                                {
                                    PatternType = 0,
                                    AssetId = ResourceLocation.FromString((string) patternDef["asset_id"]),
                                    TranslationKey = translationKey,
                                    DyeColor = color
                                };
                            }
                            else
                            {
                                bannerLayer = new();
                            }
                            
                            bannerPatternsComp.Layers.Add(bannerLayer);
                        }

                        bannerPatternsComp.NumberOfLayers = bannerPatternsComp.Layers.Count;
                        Components[StructuredComponentIds.BANNER_PATTERNS_ID] = bannerPatternsComp;
                    }
                    else if (blockEntityTag.TryGetValue("Patterns", out patterns) && patterns is object[] oldPatternList)
                    {
                        var bannerPatternsComp = new BannerPatternsComponent(itemPalette, subComponentRegistry);
                        
                        // Old format. e.g. "{Patterns:{Pattern:"rs",Color:0} }"
                        foreach (Dictionary<string, object> patternData in oldPatternList)
                        {
                            // Encoded as enum int (probably as a string)
                            var color = (CommonColors) int.Parse(patternData["Color"].ToString());

                            var patternCode = (string) patternData["Pattern"];
                            var patternId = BannerPatternType.GetIdFromCode(patternCode);
                            
                            var bannerLayer = new BannerLayer
                            {
                                PatternType = BannerPatternType.GetIndexFromId(patternId),
                                DyeColor = color
                            };
                            
                            bannerPatternsComp.Layers.Add(bannerLayer);
                        }
                        
                        bannerPatternsComp.NumberOfLayers = bannerPatternsComp.Layers.Count;
                        Components[StructuredComponentIds.BANNER_PATTERNS_ID] = bannerPatternsComp;
                    }
                    
                    if (blockEntityTag.TryGetValue("Items", out var items) && items is object[] itemList)
                    {
                        var containerComp = new ContainerComponent(itemPalette, subComponentRegistry);
                        
                        foreach (var containedItemStack in itemList
                                 .Select(x => FromSlotNBT((Dictionary<string, object>) x).Item2)
                                 .Where(x => x is not null))
                        {
                            containerComp.Items.Add(containedItemStack);
                        }
                        
                        containerComp.NumberOfItems = containerComp.Items.Count;
                        Components[StructuredComponentIds.CONTAINER_ID] = containerComp;
                    }
                }
                
            }
        }

        /// <summary>
        /// Create an item stack from an existing item stack
        /// </summary>
        /// <param name="itemStack">Source ItemStack</param>
        /// <param name="count">Item Count</param>
        public ItemStack(ItemStack itemStack, int count) : this(itemStack.ItemType, count, itemStack.NBT)
        {
            ReceivedComponentsToAdd = itemStack.ReceivedComponentsToAdd;
            ReceivedComponentsToRemove = itemStack.ReceivedComponentsToRemove;
        }

        private static PotionEffectSubComponent GetPotionEffectSubComponentFromNBT(Dictionary<string, object> x, SubComponentRegistry subComponentRegistry)
        {
            var effectId = ResourceLocation.FromString((string)x["id"]);
            int amplifier = x.TryGetValue("amplifier", out var value) ? (int)value : 0; // 0 (Level I) by default
            int duration = x.TryGetValue("duration", out value) ? (int)value : 1; // 1 tick by default

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
