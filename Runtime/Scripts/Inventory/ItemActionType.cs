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
        Projectile,      // Egg / Snowball / Experience bottle
        MobBucket,       // Fish bucket / Axolotl bucket / Tadpole bucket
    }
}