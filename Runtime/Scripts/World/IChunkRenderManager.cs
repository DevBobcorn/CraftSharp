#nullable enable
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.Mathematics;

namespace CraftSharp
{
    public interface IChunkRenderManager
    {
        /// <summary>
        /// Set block at the specified location, invoked from network thread
        /// </summary>
        public void SetBlock(BlockLoc blockLoc, Block block);

        /// <summary>
        /// Add a new block entity render to the world
        /// </summary>
        public void AddBlockEntityRender(BlockLoc blockLoc, BlockEntityType blockEntityType, Dictionary<string, object>? tags = null);

        /// <summary>
        /// Remove a block entity render from the world
        /// </summary>
        public void RemoveBlockEntityRender(BlockLoc blockLoc);

        /// <summary>
        /// Get lighting cache for this chunk render manager
        /// </summary>
        /// <returns></returns>
        public ConcurrentDictionary<int2, Queue<byte>> GetLightingCache();

        /// <summary>
        /// Create an empty chunk column, invoked from network thread
        /// </summary>
        public void CreateEmptyChunkColumn(int chunkX, int chunkZ, int chunkColumnSize);

        /// <summary>
        /// Store a chunk, invoked from network thread
        /// </summary>
        public void StoreChunk(int chunkX, int chunkY, int chunkZ, int chunkColumnSize, Chunk? chunk);

        /// <summary>
        /// Get a chunk column from the world
        /// </summary>
        public ChunkColumn? GetChunkColumn(int chunkX, int chunkZ);

        /// <summary>
        /// Unload a chunk column, invoked from network thread
        /// </summary>
        public void UnloadChunkColumn(int chunkX, int chunkZ);
    }
}