#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace CraftSharp
{
    /// <summary>
    /// Represents a Minecraft World
    /// </summary>
    public class World
    {
        // Using biome colors of minecraft:plains as default
        // See https://minecraft.fandom.com/wiki/Plains
        public static readonly int DEFAULT_FOLIAGE = 0x77AB2F;
        public static readonly int DEFAULT_GRASS   = 0x91BD59;
        public static readonly int DEFAULT_WATER   = 0x3F76E4;

        public static readonly Biome DUMMY_BIOME = new(ResourceLocation.INVALID,
                0, DEFAULT_FOLIAGE, DEFAULT_GRASS, DEFAULT_WATER, 0, 0);
        
        #region Static data storage and access
        
        /// <summary>
        /// The dimension info of the world
        /// </summary>
        protected static Dimension curDimension = new();
        protected static readonly Dictionary<string, Dimension> dimensionList = new();

        public static bool BiomesInitialized { get; private set; } = false;

        /// <summary>
        /// The biomes of the world
        /// </summary>
        public static readonly Dictionary<int, Biome> BiomeList = new();

        /// <summary>
        /// Storage of all dimensional data - 1.19.1 and above
        /// </summary>
        /// <param name="registryCodec">Registry Codec nbt data</param>
        public static void StoreDimensionList(Dictionary<string, object> registryCodec)
        {
            var dimensionListNbt = (object[])(((Dictionary<string, object>)registryCodec["minecraft:dimension_type"])["value"]);
            foreach (Dictionary<string, object> dimensionNbt in dimensionListNbt)
            {
                string dimensionName = (string)dimensionNbt["name"];
                Dictionary<string, object> dimensionType = (Dictionary<string, object>)dimensionNbt["element"];
                StoreOneDimension(dimensionName, dimensionType);
            }
        }

        /// <summary>
        /// Store one dimension - Directly used in 1.16.2 to 1.18.2
        /// </summary>
        /// <param name="dimensionName">Dimension name</param>
        /// <param name="dimensionType">Dimension Type nbt data</param>
        public static void StoreOneDimension(string dimensionName, Dictionary<string, object> dimensionType)
        {
            if (dimensionList.ContainsKey(dimensionName))
                dimensionList.Remove(dimensionName);
            dimensionList.Add(dimensionName, new Dimension(dimensionName, dimensionType));
        }

        /// <summary>
        /// Set current dimension - 1.16 and above
        /// </summary>
        /// <param name="name">	The name of the dimension type</param>
        /// <param name="nbt">The dimension type (NBT Tag Compound)</param>
        public static void SetDimension(string name)
        {
            curDimension = dimensionList[name]; // Should not fail
        }

        /// <summary>
        /// Get current dimension
        /// </summary>
        /// <returns>Current dimension</returns>
        public static Dimension GetDimension()
        {
            return curDimension;
        }

        public static Color32[] FoliageColormapPixels { get; set; } = { };
        public static Color32[] GrassColormapPixels { get; set; } = { };

        public static int ColormapSize { get; set; } = 0;
        
        /// <summary>
        /// Storage of all dimensional data - 1.19.1 and above
        /// </summary>
        /// <param name="registryCodec">Registry Codec nbt data</param>
        public static void StoreBiomeList(Dictionary<string, object> registryCodec)
        {
            var biomeListNbt = (object[])((Dictionary<string, object>)registryCodec["minecraft:worldgen/biome"])["value"];

            if (FoliageColormapPixels.Length == 0 || GrassColormapPixels.Length == 0)
            {
                Debug.LogWarning("Biome colormap is not available. Color lookup will not be performed.");
            }

            foreach (Dictionary<string, object> biomeNbt in biomeListNbt)
            {
                StoreOneBiome(biomeNbt);
            }

            BiomesInitialized = true;
        }

        /// <summary>
        /// Store one biome
        /// </summary>
        /// <param name="biomeName">Biome name</param>
        /// <param name="biomeData">Information of this biome</param>
        public static void StoreOneBiome(Dictionary<string, object> biomeData)
        {
            var biomeName = (string)biomeData["name"];
            var biomeNumId = (int)biomeData["id"];
            var biomeId = ResourceLocation.FromString(biomeName);

            if (BiomeList.ContainsKey(biomeNumId))
                BiomeList.Remove(biomeNumId);
            
            //Debug.Log($"Biome registered:\n{Json.Dictionary2Json(biomeData)}");

            int sky = 0, foliage = 0, grass = 0, water = 0, fog = 0, waterFog = 0;
            float temperature = 0F, downfall = 0F, adjustedTemp = 0F, adjustedRain = 0F;
            Precipitation precipitation = Precipitation.None;

            var biomeDef = (Dictionary<string, object>)biomeData["element"];

            if (biomeDef.ContainsKey("downfall"))
                downfall = (float) biomeDef["downfall"];
                            
            if (biomeDef.ContainsKey("temperature"))
                temperature = (float) biomeDef["temperature"];
            
            if (biomeDef.ContainsKey("precipitation"))
            {
                precipitation = ((string) biomeDef["precipitation"]).ToLower() switch
                {
                    "rain" => Precipitation.Rain,
                    "snow" => Precipitation.Snow,
                    "none" => Precipitation.None,

                    _      => Precipitation.Unknown
                };

                if (precipitation == Precipitation.Unknown)
                    Debug.LogWarning($"Unexpected precipitation type: {biomeDef["precipitation"]}");
            }

            if (biomeDef.ContainsKey("effects"))
            {
                var effects = (Dictionary<string, object>)biomeDef["effects"];

                if (effects.ContainsKey("sky_color"))
                    sky = (int) effects["sky_color"];
                
                adjustedTemp = Mathf.Clamp01(temperature);
                adjustedRain = Mathf.Clamp01(downfall) * adjustedTemp;

                int sampleX = (int)((1F - adjustedTemp) * ColormapSize);
                int sampleY = (int)(adjustedRain * ColormapSize);

                if (effects.ContainsKey("foliage_color"))
                    foliage = (int)effects["foliage_color"];
                else // Read foliage color from color map. See https://minecraft.fandom.com/wiki/Color
                {
                    var color = (FoliageColormapPixels.Length == 0) ? (Color32) Color.magenta :
                            FoliageColormapPixels[sampleY * ColormapSize + sampleX];
                    foliage = (color.r << 16) | (color.g << 8) | color.b;
                }
                
                if (effects.ContainsKey("grass_color"))
                    grass = (int)effects["grass_color"];
                else // Read grass color from color map. Same as above
                {
                    var color = (GrassColormapPixels.Length == 0) ? (Color32) Color.magenta :
                            GrassColormapPixels[sampleY * ColormapSize + sampleX];
                    grass = (color.r << 16) | (color.g << 8) | color.b;
                }
                
                if (effects.ContainsKey("fog_color"))
                    fog = (int)effects["fog_color"];
                
                if (effects.ContainsKey("water_color"))
                    water = (int)effects["water_color"];
                
                if (effects.ContainsKey("water_fog_color"))
                    waterFog = (int)effects["water_fog_color"];
            }

            Biome biome = new(biomeId, sky, foliage, grass, water, fog, waterFog)
            {
                Temperature = temperature,
                Downfall = downfall,
                Precipitation = precipitation
            };

            BiomeList.Add(biomeNumId, biome);
        }

        #endregion

        #region World instance data storage and access

        /// <summary>
        /// The chunks contained into the Minecraft world
        /// </summary>
        private readonly ConcurrentDictionary<int2, ChunkColumn> columns = new();

        /// <summary>
        /// Read, set or unload the specified chunk column
        /// </summary>
        /// <param name="chunkX">ChunkColumn X</param>
        /// <param name="chunkZ">ChunkColumn Z</param>
        /// <returns>Chunk at the given location</returns>
        public ChunkColumn? this[int chunkX, int chunkZ]
        {
            get
            {
                columns.TryGetValue(new(chunkX, chunkZ), out ChunkColumn? chunkColumn);
                return chunkColumn;
            }
            set
            {
                int2 chunkCoord = new(chunkX, chunkZ);
                if (value is null)
                    columns.TryRemove(chunkCoord, out _);
                else
                    columns.AddOrUpdate(chunkCoord, value, (_, _) => value);
            }
        }

        /// <summary>
        /// Check whether the data of a chunk column is loaded
        /// </summary>
        /// <param name="chunkX">ChunkColumn X</param>
        /// <param name="chunkZ">ChunkColumn Z</param>
        /// <returns>True if chunk column data is ready</returns>
        public bool IsChunkColumnLoaded(int chunkX, int chunkZ)
        {
            // Chunk column data is sent one whole column per time,
            // a whole air chunk is represent by null
            if (columns.TryGetValue(new(chunkX, chunkZ), out ChunkColumn? chunkColumn))
                return chunkColumn is not null && chunkColumn.FullyLoaded && chunkColumn.LightingPresent;
            return false;
        }

        /// <summary>
        /// Check whether the data of a chunk column is loaded
        /// </summary>
        /// <param name="chunkX">ChunkColumn X</param>
        /// <param name="chunkZ">ChunkColumn Z</param>
        /// <returns>True if chunk column data is ready</returns>
        public bool IsChunkColumnReady(int chunkX, int chunkZ)
        {
            // Chunk column data is sent one whole column per time,
            // a whole air chunk is represent by null
            if (columns.TryGetValue(new(chunkX, chunkZ), out ChunkColumn? chunkColumn))
                return chunkColumn is not null && chunkColumn.FullyLoaded && chunkColumn.LightingPresent;
            return false;
        }

        /// <summary>
        /// Store chunk at the specified location
        /// </summary>
        /// <param name="chunkX">ChunkColumn X</param>
        /// <param name="chunkY">ChunkColumn Y</param>
        /// <param name="chunkZ">ChunkColumn Z</param>
        /// <param name="chunkColumnSize">ChunkColumn size</param>
        /// <param name="chunk">Chunk data</param>
        public void StoreChunk(int chunkX, int chunkY, int chunkZ, int chunkColumnSize, Chunk? chunk)
        {
            ChunkColumn chunkColumn = columns.GetOrAdd(new(chunkX, chunkZ), (_) => new(chunkColumnSize));
            chunkColumn[chunkY] = chunk;
        }

        /// <summary>
        /// Create empty chunk column at the specified location
        /// </summary>
        /// <param name="chunkX">ChunkColumn X</param>
        /// <param name="chunkZ">ChunkColumn Z</param>
        /// <param name="chunkColumnSize">ChunkColumn size</param>
        public void CreateEmptyChunkColumn(int chunkX, int chunkZ, int chunkColumnSize)
        {
            columns.GetOrAdd(new(chunkX, chunkZ), (_) => new(chunkColumnSize));
        }

        /// <summary>
        /// Get chunk column at the specified location
        /// </summary>
        /// <param name="blockLoc">Location to retrieve chunk column</param>
        /// <returns>The chunk column</returns>
        public ChunkColumn? GetChunkColumn(BlockLoc blockLoc)
        {
            return this[blockLoc.GetChunkX(), blockLoc.GetChunkZ()];
        }

        public static readonly Block AIR_INSTANCE = new(0);

        /// <summary>
        /// Get block at the specified location
        /// </summary>
        /// <param name="blockLoc">Location to retrieve block from</param>
        /// <returns>Block at specified location or Air if the location is not loaded</returns>
        public Block GetBlock(BlockLoc blockLoc)
        {
            var column = GetChunkColumn(blockLoc);
            if (column != null)
            {
                return column.GetBlock(blockLoc);
            }
            return AIR_INSTANCE; // Air
        }

        /// <summary>
        /// Get block light at the specified location
        /// </summary>
        public byte GetBlockLight(BlockLoc blockLoc)
        {
            var column = GetChunkColumn(blockLoc);
            if (column != null)
                return column.GetBlockLight(blockLoc);
            
            return (byte) 0; // Not available
        }

        /// <summary>
        /// Set block light at the specified location
        /// </summary>
        public void SetBlockLight(BlockLoc blockLoc, byte newValue)
        {
            GetChunkColumn(blockLoc)?.SetBlockLight(blockLoc, newValue);
        }

        public T[,,] GetValuesFromSection<T>(int minX, int minY, int minZ, int sizeX, int sizeY, int sizeZ, Func<Block, T> valueGetter)
        {
            T[,,] result = new T[sizeX, sizeY, sizeZ];
            
            // Min coordinate on each axis (inclusive)
            // Max coordinate on each axis (exclusive)
            int maxX = minX + sizeX, maxZ = minZ + sizeZ, maxY = minY + sizeY;
            int minCX = minX >> 4;        // Min chunk X
            int minCZ = minZ >> 4;        // Min chunk Z
            int maxCX = (maxX - 1) >> 4;  // Max chunk X
            int maxCZ = (maxZ - 1) >> 4;  // Max chunk Z

            for (int cx = minCX; cx <= maxCX; cx++)
                for (int cz = minCZ; cz <= maxCZ; cz++)
                {
                    // Get the current chunk column
                    var chunkColumn = this[cx, cz];

                    if (chunkColumn is not null) // Chunk column is not empty
                    {
                        // Go through all valid xz locations within this chunk column
                        for (int blocX = math.max(minX, cx << 4); blocX < math.min(maxX, (cx + 1) << 4); blocX++)
                        {
                            int resX = blocX - minX;
                            for (int blocZ = math.max(minZ, cz << 4); blocZ < math.min(maxZ, (cz + 1) << 4); blocZ++)
                            {
                                int resZ = blocZ - minZ;
                                // Then go though all blocks in this line
                                for (int blocY = minY; blocY < maxY; blocY++)
                                {
                                    int resY = blocY - minY;
                                    var blocLoc = new BlockLoc(blocX, blocY, blocZ);

                                    result[resX, resY, resZ] = valueGetter(chunkColumn.GetBlock(blocLoc));
                                }
                            }
                        }
                    }
                    else // Chunk column is empty
                    {
                        var val = valueGetter(AIR_INSTANCE);

                        // Go through all valid xz locations within this chunk column
                        for (int blocX = math.max(minX, cx << 4); blocX < math.min(maxX, (cx + 1) << 4); blocX++)
                        {
                            int resX = blocX - minX;
                            for (int blocZ = math.max(minZ, cz << 4); blocZ < math.min(maxZ, (cz + 1) << 4); blocZ++)
                            {
                                int resZ = blocZ - minZ;
                                // Then go though all blocks in this line
                                for (int blocY = minY; blocY < maxY; blocY++)
                                {
                                    int resY = blocY - minY;

                                    result[resX, resY, resZ] = val;
                                }
                            }
                        }
                    }
                }
            
            return result;
        }

        /// <summary>
        /// Get all essential data for doing a chunk mesh build.
        /// </summary>
        public ChunkBuildData GetChunkBuildData(int chunkX, int chunkZ, int chunkYIndex)
        {
            var result = new ChunkBuildData();
            var blocs = result.Blocks = new Block[Chunk.PADDED, Chunk.PADDED, Chunk.PADDED];
            var light = result.Light = new byte[Chunk.PADDED, Chunk.PADDED, Chunk.PADDED];
            var allao = result.AO = new bool[Chunk.PADDED, Chunk.PADDED, Chunk.PADDED];
            var color = result.Color = new float3[Chunk.SIZE, Chunk.SIZE, Chunk.SIZE];
            
            int minCX = chunkX - 1;  // Min chunk X
            int minCZ = chunkZ - 1;  // Min chunk Z
            int maxCX = chunkX + 1;  // Max chunk X
            int maxCZ = chunkZ + 1;  // Max chunk Z

            // Max coordinate on each axis (inclusive)
            int minX = (chunkX << 4) - 1,              minZ = (chunkZ << 4) - 1,              minY = (chunkYIndex << 4) + GetDimension().minY - 1;
            // Max coordinate on each axis (exclusive)
            int maxX = (chunkX << 4) + Chunk.SIZE + 1, maxZ = (chunkZ << 4) + Chunk.SIZE + 1, maxY = (chunkYIndex << 4) + GetDimension().minY + Chunk.SIZE + 1;

            for (int cx = minCX; cx <= maxCX; cx++)
                for (int cz = minCZ; cz <= maxCZ; cz++)
                {
                    // Get the current chunk column
                    var chunkColumn = this[cx, cz];

                    if (chunkColumn is not null) // Chunk column is not empty
                    {
                        // Go through all valid xz locations within this chunk column
                        for (int blocX = math.max(minX, cx << 4); blocX < math.min(maxX, (cx + 1) << 4); blocX++)
                        {
                            int resX = blocX - minX;
                            for (int blocZ = math.max(minZ, cz << 4); blocZ < math.min(maxZ, (cz + 1) << 4); blocZ++)
                            {
                                int resZ = blocZ - minZ;
                                // Then go though all blocks in this line
                                for (int blocY = minY; blocY < maxY; blocY++)
                                {
                                    int resY = blocY - minY;
                                    var blocLoc = new BlockLoc(blocX, blocY, blocZ);

                                    var bloc = chunkColumn.GetBlock(blocLoc);
                                    blocs[resX, resY, resZ] = bloc;
                                    light[resX, resY, resZ] = chunkColumn.GetBlockLight(blocLoc);
                                    allao[resX, resY, resZ] = chunkColumn.GetAmbientOcclusion(blocLoc);
                                    
                                    if (resX > 0 && resX < Chunk.SIZE && resY > 0 && resY < Chunk.SIZE && resZ > 0 && resZ < Chunk.SIZE)
                                    {
                                        // No padding for block color
                                        color[resX - 1, resY - 1, resZ - 1] = BlockStatePalette.INSTANCE.GetBlockColor(bloc.StateId, this, blocLoc, bloc.State);
                                    }
                                }
                            }
                        }
                    }
                    else // Chunk column is empty
                    {
                        // Go through all valid xz locations within this chunk column
                        for (int blocX = math.max(minX, cx << 4); blocX < math.min(maxX, (cx + 1) << 4); blocX++)
                        {
                            int resX = blocX - minX;
                            for (int blocZ = math.max(minZ, cz << 4); blocZ < math.min(maxZ, (cz + 1) << 4); blocZ++)
                            {
                                int resZ = blocZ - minZ;
                                // Then go though all blocks in this line
                                for (int blocY = minY; blocY < maxY; blocY++)
                                {
                                    int resY = blocY - minY;

                                    blocs[resX, resY, resZ] = AIR_INSTANCE;
                                    light[resX, resY, resZ] = 0;
                                    allao[resX, resY, resZ] = false;
                                }
                            }
                        }
                    }
                }
            
            return result;
        }

        /// <summary>
        /// Check if the block at specified location causes ambient occlusion
        /// </summary>
        public bool GetAmbientOcclusion(BlockLoc blockLoc)
        {
            var column = GetChunkColumn(blockLoc);
            if (column != null)
                return column.GetAmbientOcclusion(blockLoc);
            
            return false; // Not available
        }

        /// <summary>
        /// Clear all terrain data from the world
        /// </summary>
        public void Clear()
        {
            columns.Clear();
        }

        public byte[] GetLiquidHeights(BlockLoc blockLoc)
        {
            // Height References
            //  NE---E---SE
            //  |         |
            //  N    @    S
            //  |         |
            //  NW---W---SW

            return new byte[] {
                16, 16, 16,
                16, 16, 16,
                16, 16, 16
            };
        }

        private const int COLOR_SAMPLE_RADIUS = 2;

        /// <summary>
        /// Get biome at the specified location
        /// </summary>
        public Biome GetBiome(BlockLoc blockLoc)
        {
            var column = GetChunkColumn(blockLoc);
            if (column != null)
                return BiomeList.GetValueOrDefault(column.GetBiomeId(blockLoc), DUMMY_BIOME);
            
            return DUMMY_BIOME; // Not available
        }

        public float3 GetFoliageColor(BlockLoc blockLoc)
        {
            int cnt = 0;
            float3 colorSum = float3.zero;
            for (int x = -COLOR_SAMPLE_RADIUS;x <= COLOR_SAMPLE_RADIUS;x++)
                for (int y = -COLOR_SAMPLE_RADIUS;y <= COLOR_SAMPLE_RADIUS;y++)
                    for (int z = -COLOR_SAMPLE_RADIUS;z < COLOR_SAMPLE_RADIUS;z++)
                    {
                        var b = GetBiome(blockLoc + new BlockLoc(x, y, z));
                        if (b != DUMMY_BIOME)
                        {
                            cnt++;
                            colorSum += b.FoliageColor;
                        }
                    }

            return cnt == 0 ? DUMMY_BIOME.FoliageColor : colorSum / cnt;
        }

        public float3 GetGrassColor(BlockLoc blockLoc)
        {
            int cnt = 0;
            float3 colorSum = float3.zero;
            for (int x = -COLOR_SAMPLE_RADIUS;x <= COLOR_SAMPLE_RADIUS;x++)
                for (int y = -COLOR_SAMPLE_RADIUS;y <= COLOR_SAMPLE_RADIUS;y++)
                    for (int z = -COLOR_SAMPLE_RADIUS;z < COLOR_SAMPLE_RADIUS;z++)
                    {
                        var b = GetBiome(blockLoc + new BlockLoc(x, y, z));
                        if (b != DUMMY_BIOME)
                        {
                            cnt++;
                            colorSum += b.GrassColor;
                        }
                    }
            
            return cnt == 0 ? DUMMY_BIOME.GrassColor : colorSum / cnt;
        }

        public float3 GetWaterColor(BlockLoc blockLoc)
        {
            int cnt = 0;
            float3 colorSum = float3.zero;
            for (int x = -COLOR_SAMPLE_RADIUS;x <= COLOR_SAMPLE_RADIUS;x++)
                for (int y = -COLOR_SAMPLE_RADIUS;y <= COLOR_SAMPLE_RADIUS;y++)
                    for (int z = -COLOR_SAMPLE_RADIUS;z < COLOR_SAMPLE_RADIUS;z++)
                    {
                        var b = GetBiome(blockLoc + new BlockLoc(x, y, z));
                        if (b != DUMMY_BIOME)
                        {
                            cnt++;
                            colorSum += b.WaterColor;
                        }
                    }
            
            return cnt == 0 ? DUMMY_BIOME.WaterColor : colorSum / cnt;
        }

        #endregion
    }
}
