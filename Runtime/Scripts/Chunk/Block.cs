namespace CraftSharp
{
    /// <summary>
    /// Represents a Minecraft Block State
    /// </summary>
    public readonly struct Block
    {
        /// <summary>
        /// Storage for block ID, as ushort for compatibility, performance and lower memory footprint
        /// For Minecraft 1.13 and greater, all 16 bits are used to store block state ID (0-65535)
        /// </summary>
        private readonly ushort stateId;

        /// <summary>
        /// Id of the block state
        /// </summary>
        public int StateId => stateId;

        /// <summary>
        /// Get a block of the specified block state
        /// </summary>
        /// <param name="stateId">block state</param>
        public Block(ushort stateId)
        {
            this.stateId = stateId;
        }

        public BlockState State => BlockStatePalette.INSTANCE.GetByNumId(StateId);

        public ResourceLocation BlockId => BlockStatePalette.INSTANCE.GetGroupIdByNumId(StateId);

        /// <summary>
        /// String representation of the block
        /// </summary>
        public override string ToString()
        {
            return $"[{StateId}] {State}";
        }
    }
}
