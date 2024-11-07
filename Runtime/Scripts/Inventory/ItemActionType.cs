namespace CraftSharp
{
    public enum ItemActionType
    {
        None, // Food actions are handled separately, for most food items the action type is none

        // Place blocks
        Block,
        Lighter,      // Fire block
        FilledBucket, // Liquid source block

        // Dig/attack/defend tools
        Shears,
        Axe,
        Pickaxe,
        Shovel,
        Hoe,
        Sword,
        Bow,
        Crossbow,
        Trident,
        Shield,

        // Use in hand
        DrinkableBottle, // Potions & honey bottle
        DrinkableBucket, // Milk bucket
        FoodOnAStick,
        EmptyMap,
        WritableBook,
        WrittenBook,

        // Use on blocks
        BoneMeal,
        Honeycomb,   // Waxing in 1.17+
        Record,
        EmptyBottle,
        EmptyBucket,

        // Use on entities
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
        EnderPearl,
        Projectile, // Egg/snowball/experience bottle
        FishingRod,
        Lead,
        KnowledgeBook,
        DebugStick
    }
}