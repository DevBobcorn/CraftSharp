namespace CraftSharp
{
    /// <summary>
    /// See 
    /// </summary>
    public enum EntityMetadataType
    {
        Byte,
        Short,      // 1.8 only
        Int,        // 1.8 only
        Vector3Int, // 1.8 only (not used by the game)
        VarInt,
        VarLong,
        Float,
        String,
        Chat,
        OptionalChat,
        Slot,
        Boolean,
        /// <summary>
        /// Float x3
        /// </summary>
        Rotations,
        Position,
        OptionalPosition,
        /// <summary>
        /// VarInt
        /// </summary>
        Direction,
        /// <summary>
        /// Optional UUID, a.k.a. Optional Living Entity Reference
        /// </summary>
        OptionalUUID,
        /// <summary>
        /// VarInt
        /// </summary>
        BlockState,
        /// <summary>
        /// VarInt (0 for absent)
        /// </summary>
        OptionalBlockState,
        /// <summary>
        /// NBT Tag, a.k.a. compound tag
        /// </summary>
        Nbt, // Removed in 1.21.9
        Particle,
        Particles, // Added in 1.20.5
        /// <summary>
        /// VarInt x3
        /// </summary>
        VillagerData,
        OptionalVarInt,
        /// <summary>
        /// VarInt
        /// </summary>
        Pose,
        /// <summary>
        /// VarInt
        /// </summary>
        CatVariant,
        /// <summary>
        /// VarInt
        /// </summary>
        CowVariant, // Added in 1.21.5
        /// <summary>
        /// VarInt
        /// </summary>
        WolfVariant, // Added in 1.20.5
        /// <summary>
        /// VarInt
        /// </summary>
        WolfSoundVariant, // Added in 1.21.5
        /// <summary>
        /// VarInt
        /// </summary>
        FrogVariant,
        /// <summary>
        /// VarInt
        /// </summary>
        PigVariant, // Added in 1.21.5
        /// <summary>
        /// VarInt
        /// </summary>
        ChickenVariant, // Added in 1.21.5
        /// <summary>
        /// String + Position
        /// </summary>
        GlobalPosition,
        /// <summary>
        /// Boolean + String + Position
        /// </summary>
        OptionalGlobalPosition,
        /// <summary>
        /// VarInt
        /// </summary>
        PaintingVariant,
        /// <summary>
        /// VarInt
        /// </summary>
        SnifferState,
        /// <summary>
        /// VarInt
        /// </summary>
        ArmadilloState, // Added in 1.20.5
        /// <summary>
        /// VarInt
        /// </summary>
        CopperGolemState, // Added in 1.21.9
        /// <summary>
        /// VarInt
        /// </summary>
        WeatheringCopperState, // Added in 1.21.9
        /// <summary>
        /// Float x3
        /// </summary>
        Vector3,
        /// <summary>
        /// Float x4
        /// </summary>
        Quaternion,
        /// <summary>
        /// Complex. See https://minecraft.wiki/w/Java_Edition_protocol/Entity_metadata#Resolvable_Profile
        /// </summary>
        ResolvableProfile // Added in 1.21.9
    }
}