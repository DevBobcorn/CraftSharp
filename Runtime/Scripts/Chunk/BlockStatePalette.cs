using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Unity.Mathematics;

namespace CraftSharp
{
    public class BlockStatePalette : IdentifierGroupPalette<BlockState>
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        public static readonly BlockStatePalette INSTANCE = new();
        protected override string Name => "BlockState Palette";
        protected override BlockState UnknownObject => BlockState.UNKNOWN;

        /// <summary>
        /// Returns true if blockId is present AND results are not empty.
        /// </summary>
        public bool TryGetStateIdCandidatesFromString(string str, out int[] stateIds)
        {
            var parts = str.Trim().Split('[');

            if (parts.Length == 1) // No predicate specified
            {
                var blockId = ResourceLocation.FromString(parts[0]);

                if (TryGetDefaultNumId(blockId, out int stateId))
                {
                    stateIds = new int[] { stateId };
                    return true;
                }
            }
            else if (parts.Length == 2 && parts[1].EndsWith(']')) // With predicates
            {
                var blockId = ResourceLocation.FromString(parts[0]);
                var filter = parts[1][..^1]; // Remove trailing ']'
                var predicate = BlockStatePredicate.FromString(filter);

                return TryGetAllNumIds(blockId, out stateIds, (x) => predicate.Check(x));
            }

            stateIds = Array.Empty<int>();
            return false;
        }

        /// <summary>
        /// Get block state numIds for input hint.
        /// </summary>
        public int[] GetStateIdCandidatesFromString(string state)
        {
            var parts = state.Trim().Split('[');

            if (parts.Length == 1) // No predicate specified
            {
                var blockId = ResourceLocation.FromString(parts[0]);

                if (TryGetDefaultNumId(blockId, out int stateId))
                {
                    return new int[] { stateId };
                }
            }
            else if (parts.Length == 2 && parts[1].EndsWith(']')) // With predicates
            {
                var blockId = ResourceLocation.FromString(parts[0]);
                var filter = parts[1][..^1]; // Remove trailing ']'
                var predicate = BlockStatePredicate.FromString(filter);

                return GetAllNumIds(blockId, (x) => predicate.Check(x));
            }
            
            return Array.Empty<int>();
        }

        /// <summary>
        /// Get one stateId from a given string, this string is expected to be complete and valid in most cases.
        /// </summary>
        public int GetStateIdFromString(string str, int defaultOverride = UNKNOWN_NUM_ID)
        {
            var parts = str.Trim().Split('[');

            if (parts.Length == 1) // No predicate specified
            {
                var blockId = ResourceLocation.FromString(parts[0]);

                if (TryGetDefaultNumId(blockId, out int stateId))
                {
                    return stateId;
                }
            }
            else if (parts.Length == 2 && parts[1].EndsWith(']')) // With predicates
            {
                var blockId = ResourceLocation.FromString(parts[0]);
                var filter = parts[1][..^1]; // Remove trailing ']'
                var predicate = BlockStatePredicate.FromString(filter);

                var state = GetAll(blockId).FirstOrDefault(x => predicate.Check(x));
                if (state is not null)
                {
                    return objectToNumId[state];
                }
            }

            return defaultOverride;
        }

        /// <summary>
        /// Try to get one stateId from a given string, this string is expected to be complete and valid in most cases.
        /// </summary>
        public bool TryGetStateIdFromString(string str, out int stateId, int defaultOverride = UNKNOWN_NUM_ID)
        {
            var parts = str.Trim().Split('[');

            if (parts.Length == 1) // No predicate specified
            {
                var blockId = ResourceLocation.FromString(parts[0]);

                if (TryGetDefaultNumId(blockId, out stateId))
                {
                    return true;
                }
            }
            else if (parts.Length == 2 && parts[1].EndsWith(']')) // With predicates
            {
                var blockId = ResourceLocation.FromString(parts[0]);
                var filter = parts[1][..^1]; // Remove trailing ']'
                var predicate = BlockStatePredicate.FromString(filter);

                var state = GetAll(blockId).FirstOrDefault(x => predicate.Check(x));
                if (state is not null)
                {
                    stateId = objectToNumId[state];
                    return true;
                }
            }

            stateId = defaultOverride;
            return false;
        }

        /// <summary>
        /// Get an array of possible blockIds guessed from given incomplete id.
        /// </summary>
        public ResourceLocation[] GetBlockIdCandidates(ResourceLocation incompleteBlockId)
        {
            return groupIdToGroup.Keys.Where(
                    x => x.Namespace == incompleteBlockId.Namespace &&
                            x.Path.StartsWith(incompleteBlockId.Path)).Take(3).ToArray();
        }
        
        public readonly Dictionary<ResourceLocation, RenderType> RenderTypeTable = new();

        public readonly Dictionary<ResourceLocation, OffsetType> OffsetTypeTable = new();

        private readonly Dictionary<int, Func<World, BlockLoc, BlockState, float3>> blockColorRules = new();

        private static readonly float3 ORIGINAL_BLOCK_COLOR = new(1F);

        public float3 GetBlockColor(int stateId, World world, BlockLoc loc)
        {
            if (blockColorRules.TryGetValue(stateId, out var rule))
                return rule.Invoke(world, loc, GetByNumId(stateId));
            return ORIGINAL_BLOCK_COLOR;
        }

        public Dictionary<string, HashSet<string>> GetBlockProperties(ResourceLocation blockId)
        {
            Dictionary<string, HashSet<string>> result = new();

            foreach (var stateId in GetAllNumIds(blockId)) // For each possible state of this block
            {
                foreach (var pair in numIdToObject[stateId].Properties) // For each property in this state
                {
                    if (!result.ContainsKey(pair.Key))
                    {
                        result.Add(pair.Key, new HashSet<string>());
                    }

                    result[pair.Key].Add(pair.Value);
                }
            }

            return result;
        }

        public (int, BlockState) GetBlockStateWithProperty(int sourceId, BlockState source, string key, string value)
        {
            var blockId = source.BlockId;
            // Copy properties from source blockstate, with the property of given key set to given value
            var props = new Dictionary<string, string>(source.Properties) { [key] = value };
            var predicate = new BlockStatePredicate(props);

            foreach (var stateId in GetAllNumIds(blockId)) // For each blockstate of this block
            {
                if (predicate.Check(numIdToObject[stateId]))
                {
                    return (stateId, numIdToObject[stateId]);
                }
            }

            return (sourceId, source);
        }

        public (int, BlockState) GetBlockStateWithProperties(ResourceLocation blockId, Dictionary<string, string> props)
        {
            var predicate = new BlockStatePredicate(props);

            foreach (var stateId in GetAllNumIds(blockId)) // For each blockstate of this block
            {
                if (predicate.Check(numIdToObject[stateId]))
                {
                    return (stateId, numIdToObject[stateId]);
                }
            }

            return (GetDefaultNumId(blockId), GetDefault(blockId));
        }

        protected override void ClearEntries()
        {
            base.ClearEntries();
            blockColorRules.Clear();
            RenderTypeTable.Clear();
            OffsetTypeTable.Clear();
        }

        /// <summary>
        /// Load block data from external files.
        /// </summary>
        /// <param name="dataVersion">Block data version</param>
        /// <param name="flag">Data load flag</param>
        public void PrepareData(string dataVersion, DataLoadFlag flag)
        {
            // Clean up first...
            ClearEntries();

            string blocksPath = PathHelper.GetExtraDataFile($"blocks{SP}blocks-{dataVersion}.json");
            string colorsPath = PathHelper.GetExtraDataFile("block_colors.json");
            string renderTypePath = PathHelper.GetExtraDataFile("block_render_type.json");

            if (!File.Exists(blocksPath) || !File.Exists(colorsPath) || !File.Exists(renderTypePath))
            {
                Debug.LogWarning("Block data not complete!");
                flag.Finished = true;
                flag.Failed = true;
                return;
            }

            try
            {
                var blocks = Json.ParseJson(File.ReadAllText(blocksPath, Encoding.UTF8));

                foreach (var (key, blockDef) in blocks.Properties)
                {
                    ResourceLocation blockId = ResourceLocation.FromString(key);
                    int defaultStateId = int.MaxValue;
                    var states = new Dictionary<int, BlockState>();

                    // Properties shared by all state of this block
                    float blastResistance = float.Parse(blockDef.Properties["blast_resistance"]
                            .StringValue, CultureInfo.InvariantCulture);
                    float friction = float.Parse(blockDef.Properties["friction"]
                            .StringValue, CultureInfo.InvariantCulture);
                    float jumpFactor = float.Parse(blockDef.Properties["jump_factor"]
                            .StringValue, CultureInfo.InvariantCulture);
                    float speedFactor = float.Parse(blockDef.Properties["speed_factor"]
                            .StringValue, CultureInfo.InvariantCulture);

                    bool noSolidMesh = BlockState.NO_SOLID_MESH_IDS.Contains(blockId);

                    foreach (Json.JSONData state in blockDef.Properties["states"].DataArray)
                    {
                        int stateId = int.Parse(state.Properties["id"].StringValue);

                        if (state.Properties.TryGetValue("default", out var property))
                        {
                            if (bool.Parse(property.StringValue))
                            {
                                defaultStateId = stateId;
                            }
                        }

                        var props = new Dictionary<string, string>();

                        if (state.Properties.TryGetValue("properties", out var stateProperty))
                        {
                            foreach (var (propKey, value) in stateProperty.Properties)
                            {
                                props.Add(propKey, value.StringValue);
                            }
                        }

                        var hardness = float.Parse(state.Properties["hardness"]
                                .StringValue, CultureInfo.InvariantCulture);

                        var fullFaces = int.Parse(state.Properties["full_faces"].StringValue);
                        var noCollision = bool.Parse(state.Properties["no_collision"].StringValue);
                        var noOcclusion = bool.Parse(state.Properties["no_occlusion"].StringValue);

                        var lightEmission = byte.Parse(state.Properties["light_emission"].StringValue);
                        var lightBlockage = byte.Parse(state.Properties["light_blockage"].StringValue);

                        ResourceLocation? fluidStateId = null;

                        if (state.Properties.TryGetValue("fluid_state", out Json.JSONData fluidState))
                        {
                            fluidStateId = ResourceLocation.FromString(fluidState.StringValue);
                        }

                        BlockShape shape, collisionShape;

                        if (state.Properties.TryGetValue("aabbs", out var aabbList))
                        {
                            var aabbs = (aabbList.DataArray).Select(x => BlockShapeAABB.FromString(x.StringValue)).ToArray();
                            shape = new BlockShape(aabbs);

                            if (state.Properties.TryGetValue("collision_aabbs", out var colAabbList))
                            {
                                var colAabbs = (colAabbList.DataArray).Select(x => BlockShapeAABB.FromString(x.StringValue)).ToArray();
                                collisionShape = new BlockShape(colAabbs);
                            }
                            else
                            {
                                collisionShape = shape;
                            }
                        }
                        else
                        {
                            shape = BlockShape.EMPTY;
                            collisionShape = BlockShape.EMPTY;
                        }

                        states[stateId] = new BlockState(blockId, blastResistance, hardness, noSolidMesh, fullFaces,
                                noCollision, noOcclusion, lightBlockage, lightEmission, fluidStateId, props)
                        {
                            // Assign non-readonly fields
                            Friction = friction,
                            JumpFactor = jumpFactor,
                            SpeedFactor = speedFactor,
                            Shape = shape,
                            CollisionShape = collisionShape,
                        };
                    }
            
                    if (defaultStateId == int.MaxValue) // Default block state of this block is not specified
                    {
                        var firstStateId = states.First().Key;
                        defaultStateId = firstStateId;
                        Debug.LogWarning($"Default blockstate of {blockId} is not specified, using first state ({firstStateId})");
                    }

                    AddEntry(blockId, defaultStateId, states);
                }

                //Debug.Log($"{numIdToObject.Count} block states loaded.");

                // Load block color rules...
                Json.JSONData colorRules = Json.ParseJson(File.ReadAllText(colorsPath, Encoding.UTF8));

                if (colorRules.Properties.TryGetValue("dynamic", out var dynamicRulesProperty))
                {
                    foreach (var (ruleName, ruleValue) in dynamicRulesProperty.Properties)
                    {
                        Func<World, BlockLoc, BlockState, float3> ruleFunc = ruleName switch
                        {
                            "foliage"  => (world, loc, _) => world.GetFoliageColor(loc),
                            "grass"    => (world, loc, _) => world.GetGrassColor(loc),
                            "redstone" => (_, _, state) => state.Properties
                                .TryGetValue("power", out var property) ?
                                    new(int.Parse(property) / 16F, 0F, 0F) : float3.zero,

                            _          => (_, _, _) => float3.zero
                        };

                        foreach (var blockId in ruleValue.DataArray
                                     .Select(block => ResourceLocation.FromString(block.StringValue)))
                        {
                            if (TryGetAllNumIds(blockId, out int[] stateIds))
                            {
                                foreach (var stateId in stateIds)
                                {
                                    if (!blockColorRules.TryAdd(stateId, ruleFunc))
                                    {
                                        Debug.LogWarning($"Failed to apply dynamic color rules to {blockId} ({stateId})!");
                                    }
                                }
                            }
                        }
                    }
                }

                if (colorRules.Properties.TryGetValue("fixed", out var fixedRulesProperty))
                {
                    foreach (var (key, ruleValue) in fixedRulesProperty.Properties)
                    {
                        var blockId = ResourceLocation.FromString(key);
                        var fixedColor = VectorUtil.Json2Float3(ruleValue) / 255F;
                        float3 ruleFunc(World world, BlockLoc loc, BlockState state) => fixedColor;

                        if (TryGetAllNumIds(blockId, out int[] stateIds))
                        {
                            foreach (var stateId in stateIds)
                            {
                                if (!blockColorRules.TryAdd(stateId, ruleFunc))
                                {
                                    Debug.LogWarning($"Failed to apply fixed color rules to {blockId} ({stateId})!");
                                }
                            }
                        }
                    }
                }
            
                // Load and apply block render types...
            
                var renderTypeText = File.ReadAllText(renderTypePath, Encoding.UTF8);
                var renderTypes = Json.ParseJson(renderTypeText);

                var restBlockIds = groupIdToGroup.Keys.ToHashSet();

                foreach (var (key, renderTypeValue) in renderTypes.Properties)
                {
                    var blockId = ResourceLocation.FromString(key);

                    var renderTypeStr = renderTypeValue.StringValue.ToLower();
                    var offsetTypeStr = string.Empty;

                    if (renderTypeStr.Contains('+'))
                    {
                        var split = renderTypeStr.Split('+', 2);
                        renderTypeStr = split[0];
                        offsetTypeStr = split[1];
                    }

                    if (restBlockIds.Contains(blockId))
                    {
                        var renderType = renderTypeStr switch
                        {
                            "solid"         => RenderType.SOLID,
                            "cutout"        => RenderType.CUTOUT,
                            "cutout_mipped" => RenderType.CUTOUT_MIPPED,
                            "translucent"   => RenderType.TRANSLUCENT,

                            "water"         => RenderType.WATER,
                            "foliage"       => RenderType.FOLIAGE,
                            "plants"        => RenderType.PLANTS,
                            "tall_plants"   => RenderType.TALL_PLANTS,

                            _               => RenderType.SOLID
                        };

                        var offsetType = offsetTypeStr switch
                        {
                            "xz"            => OffsetType.XZ,
                            "xyz"           => OffsetType.XYZ,

                            _               => OffsetType.NONE
                        };

                        RenderTypeTable.Add(blockId, renderType);
                        OffsetTypeTable.Add(blockId, offsetType);

                        restBlockIds.Remove(blockId);
                    }
                }

                foreach (var blockId in restBlockIds) // Other blocks which doesn't have its render type specifically stated
                {
                    RenderTypeTable.Add(blockId, RenderType.SOLID); // Default to solid
                    OffsetTypeTable.Add(blockId, OffsetType.NONE);  // Default to none
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading blocks: {e.Message}");
                flag.Failed = true;
            }
            finally
            {
                FreezeEntries();
                flag.Finished = true;
            }
        }
    }
}