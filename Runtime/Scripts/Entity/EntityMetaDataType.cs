namespace CraftSharp
{
    /// <summary>
    /// See 
    /// </summary>
    public enum EntityMetaDataType
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
        Rotation,
        Position,
        OptionalPosition,
        /// <summary>
        /// VarInt
        /// </summary>
        Direction,
        OptionalUUID,
        /// <summary>
        /// VarInt
        /// </summary>
        BlockId,
        /// <summary>
        /// VarInt (0 for absent)
        /// </summary>
        OptionalBlockId,
        Nbt,
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
        WolfVariant, // Added in 1.20.5
        FrogVariant,
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
        ArmadilloState,
        /// <summary>
        /// Float x3
        /// </summary>
        Vector3,
        /// <summary>
        /// Float x4
        /// </summary>
        Quaternion
    }
}