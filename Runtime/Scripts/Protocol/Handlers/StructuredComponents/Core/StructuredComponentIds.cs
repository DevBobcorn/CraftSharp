using System;
using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_20_6;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_21;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Core
{
    // List from https://minecraft.wiki/w/Data_component_format
    public static class StructuredComponentIds
    {
        public static readonly ResourceLocation ATTRIBUTE_MODIFIERS_ID         = new("attribute_modifiers");
        public static readonly ResourceLocation BANNER_PATTERNS_ID             = new("banner_patterns");
        public static readonly ResourceLocation BASE_COLOR_ID                  = new("base_color");
        public static readonly ResourceLocation BEES_ID                        = new("bees");
        public static readonly ResourceLocation BLOCK_ENTITY_DATA_ID           = new("block_entity_data");
        public static readonly ResourceLocation BLOCK_STATE_ID                 = new("block_state");
        public static readonly ResourceLocation BLOCKS_ATTACKS_ID              = new("blocks_attacks");
        public static readonly ResourceLocation BREAK_SOUND_ID                 = new("break_sound");
        public static readonly ResourceLocation BUCKET_ENTITY_DATA_ID          = new("bucket_entity_data");
        public static readonly ResourceLocation BUNDLE_CONTENTS_ID             = new("bundle_contents");
        public static readonly ResourceLocation CAN_BREAK_ID                   = new("can_break");
        public static readonly ResourceLocation CAN_PLACE_ON_ID                = new("can_place_on");
        public static readonly ResourceLocation CHARGED_PROJECTILES_ID         = new("charged_projectiles");
        public static readonly ResourceLocation CONSUMABLE_ID                  = new("consumable");
        public static readonly ResourceLocation CONTAINER_ID                   = new("container");
        public static readonly ResourceLocation CONTAINER_LOOT_ID              = new("container_loot");
        public static readonly ResourceLocation CREATIVE_SLOT_LOCK_ID          = new("creative_slot_lock"); // Used internally only
        public static readonly ResourceLocation CUSTOM_DATA_ID                 = new("custom_data");
        public static readonly ResourceLocation CUSTOM_MODEL_DATA_ID           = new("custom_model_data");
        public static readonly ResourceLocation CUSTOM_NAME_ID                 = new("custom_name");
        public static readonly ResourceLocation DAMAGE_ID                      = new("damage");
        public static readonly ResourceLocation DAMAGE_RESISTANT_ID            = new("damage_resistant");
        public static readonly ResourceLocation DEBUG_STICK_STATE_ID           = new("debug_stick_state");
        public static readonly ResourceLocation DEATH_PROTECTION_ID            = new("death_protection");
        public static readonly ResourceLocation DYED_COLOR_ID                  = new("dyed_color");
        public static readonly ResourceLocation ENCHANTABLE_ID                 = new("enchantable");
        public static readonly ResourceLocation ENCHANTMENT_GLINT_OVERRIDE_ID  = new("enchantment_glint_override");
        public static readonly ResourceLocation ENCHANTMENTS_ID                = new("enchantments");
        public static readonly ResourceLocation ENTITY_DATA_ID                 = new("entity_data");
        public static readonly ResourceLocation EQUIPPABLE_ID                  = new("equippable");
        public static readonly ResourceLocation FIRE_RESISTANT_ID              = new("fire_resistant");  // Replaced in 1.21.2 with damage_resistant
        public static readonly ResourceLocation FIREWORK_EXPLOSION_ID          = new("firework_explosion");
        public static readonly ResourceLocation FIREWORKS_ID                   = new("fireworks");
        public static readonly ResourceLocation FOOD_ID                        = new("food");
        public static readonly ResourceLocation GLIDER_ID                      = new("glider");
        public static readonly ResourceLocation HIDE_TOOLTIP_ID                = new("hide_tooltip"); // Removed in 1.21.5 in favor of tooltip_display
        public static readonly ResourceLocation HIDE_ADDITIONAL_TOOLTIP_ID     = new("hide_additional_tooltip"); // Removed in 1.21.5 in favor of tooltip_display
        public static readonly ResourceLocation INSTRUMENT_ID                  = new("instrument");
        public static readonly ResourceLocation INTANGIBLE_PROJECTILE_ID       = new("intangible_projectile");
        public static readonly ResourceLocation ITEM_MODEL_ID                  = new("item_model");
        public static readonly ResourceLocation ITEM_NAME_ID                   = new("item_name");
        public static readonly ResourceLocation JUKEBOX_PLAYABLE_ID            = new("jukebox_playable");
        public static readonly ResourceLocation LOCK_ID                        = new("lock");
        public static readonly ResourceLocation LODESTONE_TRACKER_ID           = new("lodestone_tracker");
        public static readonly ResourceLocation LORE_ID                        = new("lore");
        public static readonly ResourceLocation MAP_COLOR_ID                   = new("map_color");
        public static readonly ResourceLocation MAP_DECORATIONS_ID             = new("map_decorations");
        public static readonly ResourceLocation MAP_ID_ID                      = new("map_id");
        public static readonly ResourceLocation MAP_POST_PROCESSING_ID            = new("map_post_processing"); // Used internally only
        public static readonly ResourceLocation MAX_DAMAGE_ID                  = new("max_damage");
        public static readonly ResourceLocation MAX_STACK_SIZE_ID              = new("max_stack_size");
        public static readonly ResourceLocation NOTE_BLOCK_SOUND_ID            = new("note_block_sound");
        public static readonly ResourceLocation OMINOUS_BOTTLE_AMPLIFIER_ID    = new("ominous_bottle_amplifier");
        public static readonly ResourceLocation POT_DECORATIONS_ID             = new("pot_decorations");
        public static readonly ResourceLocation POTION_CONTENTS_ID             = new("potion_contents");
        public static readonly ResourceLocation POTION_DURATION_SCALE_ID       = new("potion_duration_scale");
        public static readonly ResourceLocation PROFILE_ID                     = new("profile");
        public static readonly ResourceLocation PROVIDES_BANNER_PATTERNS_ID    = new("provides_banner_patterns");
        public static readonly ResourceLocation PROVIDES_TRIM_MATERIAL_ID      = new("provides_trim_material");
        public static readonly ResourceLocation RARITY_ID                      = new("rarity");
        public static readonly ResourceLocation RECIPES_ID                     = new("recipes");
        public static readonly ResourceLocation REPAIRABLE_ID                  = new("repairable");
        public static readonly ResourceLocation REPAIR_COST_ID                 = new("repair_cost");
        public static readonly ResourceLocation STORED_ENCHANTMENTS_ID         = new("stored_enchantments");
        public static readonly ResourceLocation SUSPICIOUS_STEW_EFFECTS_ID     = new("suspicious_stew_effects");
        public static readonly ResourceLocation TOOL_ID                        = new("tool");
        public static readonly ResourceLocation TOOLTIP_DISPLAY_ID             = new("tooltip_display");
        public static readonly ResourceLocation TOOLTIP_STYLE_ID               = new("tooltip_style");
        public static readonly ResourceLocation TRIM_ID                        = new("trim");
        public static readonly ResourceLocation UNBREAKABLE_ID                 = new("unbreakable");
        public static readonly ResourceLocation USE_COOLDOWN_ID                = new("use_cooldown");
        public static readonly ResourceLocation USE_REMAINDER_ID               = new("use_remainder");
        public static readonly ResourceLocation WEAPON_ID                      = new("weapon");
        public static readonly ResourceLocation WRITABLE_BOOK_CONTENT_ID       = new("writable_book_content");
        public static readonly ResourceLocation WRITTEN_BOOK_CONTENT_ID        = new("written_book_content");
        
    }
}