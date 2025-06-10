using System.IO;

namespace CraftSharp
{
    public enum ItemActionType
    {
        // Food actions are handled separately, for most food items
        // the action type is none, some others are placeable as blocks
        None,

        // Place blocks
        Block,
        Lighter,         // Flint & steel / FireCharge
        SolidBucket,     // Powder snow, etc. Added in 1.17
        FluidBucket,     // Water / Lava

        // Use in hand
        Sword,
        Bow,
        Crossbow,
        Trident,
        Mace,
        Shield,
        DrinkableBottle, // Potions / Honey bottle
        DrinkableBucket, // Milk bucket
        FoodOnAStick,
        EmptyMap,
        WritableBook,
        WrittenBook,
        FishingRod,
        KnowledgeBook,
        Spyglass,        // Added in 1.17
        Bundle,          // Added in 1.17
        Instrument,      // Goat horn, etc. Added in 1.19

        // Use on blocks
        Shears,
        Axe,
        Pickaxe,
        Shovel,
        Hoe,
        BoneMeal,
        Record,
        EmptyBottle,
        EmptyBucket,
        DebugStick,
        Honeycomb,       // Waxing action added in 1.17+
        Brush,           // Added 1.19.4

        // Use on entities
        Lead,
        NameTag,

        // Spawn entities
        SplashPotion,
        LingeringPotion,
        SpawnEgg,
        FireworkRocket,
        HangingEntity,
        ArmorStand,
        Boat,
        Minecart,
        EndCrystal,
        EyeOfEnder,
        EnderPearl,
        ThrowableItem,   // Egg / Snowball / Experience bottle
        MobBucket,       // Fish bucket / Axolotl bucket / Tadpole bucket
    }

    public static class ItemActionTypeHelper
    {
        public static ItemActionType GetItemActionType(string actionTypeName)
        {
            return actionTypeName switch
            {
                "none"             => ItemActionType.None,

                "block"            => ItemActionType.Block,
                "lighter"          => ItemActionType.Lighter,
                "solid_bucket"     => ItemActionType.SolidBucket,
                "fluid_bucket"     => ItemActionType.FluidBucket,
                
                "sword"            => ItemActionType.Sword,
                "bow"              => ItemActionType.Bow,
                "crossbow"         => ItemActionType.Crossbow,
                "trident"          => ItemActionType.Trident,
                "mace"             => ItemActionType.Mace,
                "shield"           => ItemActionType.Shield,
                "drinkable_bottle" => ItemActionType.DrinkableBottle,
                "drinkable_bucket" => ItemActionType.DrinkableBucket,
                "food_on_a_stick"  => ItemActionType.FoodOnAStick,
                "empty_map"        => ItemActionType.EmptyMap,
                "writable_book"    => ItemActionType.WritableBook,
                "written_book"     => ItemActionType.WrittenBook,
                "fishing_rod"      => ItemActionType.FishingRod,
                "knowledge_book"   => ItemActionType.KnowledgeBook,
                "spyglass"         => ItemActionType.Spyglass,
                "bundle"           => ItemActionType.Bundle,
                "instrument"       => ItemActionType.Instrument,

                "shears"           => ItemActionType.Shears,
                "axe"              => ItemActionType.Axe,
                "pickaxe"          => ItemActionType.Pickaxe,
                "shovel"           => ItemActionType.Shovel,
                "hoe"              => ItemActionType.Hoe,
                "bone_meal"        => ItemActionType.BoneMeal,
                "record"           => ItemActionType.Record,
                "empty_bottle"     => ItemActionType.EmptyBottle,
                "empty_bucket"     => ItemActionType.EmptyBucket,
                "debug_stick"      => ItemActionType.DebugStick,
                "honeycomb"        => ItemActionType.Honeycomb,
                "brush"            => ItemActionType.Brush,

                "lead"             => ItemActionType.Lead,
                "name_tag"         => ItemActionType.NameTag,

                "splash_potion"    => ItemActionType.SplashPotion,
                "lingering_potion" => ItemActionType.LingeringPotion,
                "spawn_egg"        => ItemActionType.SpawnEgg,
                "firework_rocket"  => ItemActionType.FireworkRocket,
                "hanging_entity"   => ItemActionType.HangingEntity,
                "armor_stand"      => ItemActionType.ArmorStand,
                "boat"             => ItemActionType.Boat,
                "minecart"         => ItemActionType.Minecart,
                "end_crystal"      => ItemActionType.EndCrystal,
                "eye_of_ender"     => ItemActionType.EyeOfEnder,
                "ender_pearl"      => ItemActionType.EnderPearl,
                "throwable_item"   => ItemActionType.ThrowableItem,
                "mob_bucket"       => ItemActionType.MobBucket,                            

                _                  => throw new InvalidDataException($"Item action type {actionTypeName} is not defined!")
            };
        }
    }
}