#nullable enable
using System;
using UnityEngine;

namespace CraftSharp
{
    /// <summary>
    /// Represent a column of chunks of terrain in a Minecraft world
    /// </summary>
    public class ChunkColumn
    {
        /// <summary>
        /// How many chunks (including empty ones) are there in this column
        /// </summary>
        public readonly int ColumnSize;
        public readonly int MinimumY;

        public bool FullyLoaded = false;

        /// <summary>
        /// Blocks contained into the chunk
        /// </summary>
        private readonly Chunk?[] chunks;

        public bool ChunkIsEmpty(int chunkYIndex)
        {
            if (chunkYIndex >= 0 && chunkYIndex < ColumnSize)
                return chunks[chunkYIndex] is null;
            return true;
        }

        private readonly short[] biomes;
        private readonly byte[] skyLight, blockLight;
        public byte[] SkyLight => skyLight;
        public byte[] BlockLight => blockLight;
        private readonly byte[] lightBlockageCache;
        public byte[] LightBlockageCache => lightBlockageCache;
        private readonly byte[] lightEmissionCache;
        public byte[] LightEmissionCache => lightEmissionCache;

        private bool lightingPresent = false;
        public bool LightingPresent => lightingPresent;

        /// <summary>
        /// Create a new ChunkColumn
        /// </summary>
        public ChunkColumn(int size = 16)
        {
            ColumnSize = size;
            MinimumY = World.GetDimensionType().minY;

            chunks = new Chunk?[size];
            biomes = new short[64 * size];
            skyLight = new byte[4096 * (size + 2)];
            blockLight = new byte[4096 * (size + 2)];

            lightBlockageCache = new byte[4096 * size];
            lightEmissionCache = new byte[4096 * size];
        }

        /// <summary>
        /// Get or set the specified chunk
        /// </summary>
        /// <param name="chunkYIndex">Chunk Y Index</param>
        /// <returns>chunk at the given location</returns>
        public Chunk? this[int chunkYIndex]
        {
            get
            {
                return chunks[chunkYIndex];
            }
            set
            {
                chunks[chunkYIndex] = value;
            }
        }

        /// <summary>
        /// Get chunk at the specified location
        /// </summary>
        /// <param name="blockLoc">Location, a modulo will be applied</param>
        /// <returns>The chunk, or null if not loaded</returns>
        public Chunk? GetChunk(BlockLoc blockLoc)
        {
            try
            {
                return this[blockLoc.GetChunkYIndex(MinimumY)];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        /// <summary>
        /// Get block at the specified location
        /// </summary>
        /// <param name="blockLoc">Location to retrieve block from</param>
        /// <returns>Block at specified location or Air if the location is not loaded</returns>
        public Block GetBlock(BlockLoc blockLoc)
        {
            var chunk = GetChunk(blockLoc);
            if (chunk != null)
            {
                return chunk.GetBlock(blockLoc);
            }
            return World.AIR_INSTANCE; // Air
        }

        public void SetBiomeIds(short[] biomes)
        {
            if (biomes.Length == this.biomes.Length)
                Array.Copy(biomes, this.biomes, biomes.Length);
            else
                Debug.LogWarning($"Biomes data length inconsistent: {biomes.Length} {this.biomes.Length}");
        }

        public short GetBiomeId(BlockLoc blockLoc)
        {
            int index = (((blockLoc.Y - MinimumY) >> 2) << 4) | ((blockLoc.GetChunkBlockZ() >> 2) << 2) | (blockLoc.GetChunkBlockX() >> 2);

            if (index < 0 || index >= biomes.Length)
                return (short) -1;

            return biomes[index];
        }

        public void SetLights(byte[] skyLight, byte[] blockLight)
        {
            if (skyLight.Length == this.skyLight.Length && blockLight.Length == this.blockLight.Length)
            {
                Array.Copy(skyLight,   this.skyLight,   skyLight.Length);
                Array.Copy(blockLight, this.blockLight, blockLight.Length);

                lightingPresent = true;
            }
            else
                Debug.LogWarning($"Lighting data length inconsistent: Sky Light: {skyLight.Length} {this.skyLight.Length} Block Light: {blockLight.Length} {this.blockLight.Length}");
        }

        public byte GetSkyLight(BlockLoc blockLoc)
        {
            // Move up by one chunk
            int index = ((blockLoc.Y - MinimumY + Chunk.SIZE) << 8) | (blockLoc.GetChunkBlockZ() << 4) | blockLoc.GetChunkBlockX();
            
            if (index < 0 || index >= skyLight.Length)
                return (byte) 0;
            
            return lightingPresent ? skyLight[index] : (byte) 0;
        }

        public byte GetBlockLight(BlockLoc blockLoc)
        {
            // Move up by one chunk
            int index = ((blockLoc.Y - MinimumY + Chunk.SIZE) << 8) | (blockLoc.GetChunkBlockZ() << 4) | blockLoc.GetChunkBlockX();
            
            if (index < 0 || index >= blockLight.Length)
                return (byte) 0;

            return lightingPresent ? blockLight[index] : (byte) 0;
        }

        public void SetBlockLight(BlockLoc blockLoc, byte newValue)
        {
            // Move up by one chunk
            int index = ((blockLoc.Y - MinimumY + Chunk.SIZE) << 8) | (blockLoc.GetChunkBlockZ() << 4) | blockLoc.GetChunkBlockX();
            
            if (index < 0 || index >= blockLight.Length)
                return;

            blockLight[index] = newValue;
        }

        public void InitializeBlockLightCache()
        {
            for (int ci = 0; ci < ColumnSize; ci++)
            {
                int firstIndexInChunk = ci << 12; // i.e. ci * 4096
                var chunk = chunks[ci];

                if (chunk is null) // Empty chunk, no light blockage, no light emission
                {
                    Array.Fill(lightBlockageCache, (byte) 0, firstIndexInChunk, 4096);
                    Array.Fill(lightEmissionCache, (byte) 0, firstIndexInChunk, 4096);
                }
                else
                {
                    for (int x = 0; x < 16; x++) for (int y = 0; y < 16; y++) for (int z = 0; z < 16; z++)
                    {
                        var index = firstIndexInChunk | (y << 8) | (z << 4) | x;
                        lightBlockageCache[index] = chunk[x, y, z].State.LightBlockageLevel;
                        lightEmissionCache[index] = chunk[x, y, z].State.LightEmissionLevel;
                    }
                }
            }
        }

        /// <summary>
        /// Update cached block data (AO, light data) when a block change takes place
        /// Returns true if this updates block light, which will require recalculating
        /// </summary>
        /// <param name="blockLoc"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool UpdateCachedBlockData(BlockLoc blockLoc, BlockState state)
        {
            int index = ((blockLoc.Y - MinimumY) << 8) | (blockLoc.GetChunkBlockZ() << 4) | blockLoc.GetChunkBlockX();
            
            // Lengths of lightBlockageCache and
            // lightEmissionCache should be the same
            if (index < 0 || index >= lightBlockageCache.Length)
                return false;

            if (lightBlockageCache[index] != state.LightBlockageLevel || lightEmissionCache[index] != state.LightEmissionLevel)
            {
                lightBlockageCache[index] = state.LightBlockageLevel;
                lightEmissionCache[index] = state.LightEmissionLevel;

                return true;
            }
            
            return false;
        }
    }
}
