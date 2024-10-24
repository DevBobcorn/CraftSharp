namespace CraftSharp
{
    /// <summary>
    /// See https://wiki.vg/Particles
    /// </summary>
    public enum ParticleExtraDataType
    {
        None,
        /// <summary>
        /// VarInt
        /// </summary>
        Block,
        /// <summary>
        /// Float x4
        /// </summary>
        Dust,
        /// <summary>
        /// Float x7
        /// </summary>
        DustColorTransition, // Added in 1.17
        /// <summary>
        /// Int
        /// </summary>
        Color,               // Added in 1.20.5 for entity_effect
        /// <summary>
        /// Float
        /// </summary>
        SculkCharge,         // Added in 1.19
        /// <summary>
        /// Slot
        /// </summary>
        Item,
        /// <summary>
        /// VarInt + Position + VarInt + Float + VarInt
        /// </summary>
        Vibration,           // Added in 1.17
        /// <summary>
        /// VarInt
        /// </summary>
        Shriek               // Added in 1.19
    }
}
