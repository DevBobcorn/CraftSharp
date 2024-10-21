using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Unity.Mathematics;
using System.Globalization;

namespace CraftSharp
{
    public class BlockStatePalette : IdentifierGroupPalette<BlockState>
    {
        private static readonly char SP = Path.DirectorySeparatorChar;

        public static readonly BlockStatePalette INSTANCE = new();
        public override string Name => "BlockState Palette";
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

        private readonly Dictionary<int, Func<World, BlockLoc, BlockState, float3>> blockColorRules = new();

        private static readonly float3 ORIGINAL_BLOCK_COLOR = new(1F);

        public float3 GetBlockColor(int stateId, World world, BlockLoc loc, BlockState state)
        {
            if (blockColorRules.ContainsKey(stateId))
                return blockColorRules[stateId].Invoke(world, loc, state);
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

        protected override void ClearEntries()
        {
            base.ClearEntries();
            blockColorRules.Clear();
            RenderTypeTable.Clear();
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

                foreach (var block in blocks.Properties)
                {
                    ResourceLocation blockId = ResourceLocation.FromString(block.Key);
                    int defaultStateId = int.MaxValue;
                    var states = new Dictionary<int, BlockState>();

                    // Properties shared by all state of this block
                    float blastResistance = float.Parse(block.Value.Properties["blast_resistance"]
                            .StringValue, CultureInfo.InvariantCulture);
                    float friction = float.Parse(block.Value.Properties["friction"]
                            .StringValue, CultureInfo.InvariantCulture);
                    float jumpFactor = float.Parse(block.Value.Properties["jump_factor"]
                            .StringValue, CultureInfo.InvariantCulture);
                    float speedFactor = float.Parse(block.Value.Properties["speed_factor"]
                            .StringValue, CultureInfo.InvariantCulture);

                    bool noSolidMesh = BlockState.NO_SOLID_MESH_IDS.Contains(blockId);

                    foreach (Json.JSONData state in block.Value.Properties["states"].DataArray)
                    {
                        int stateId = int.Parse(state.Properties["id"].StringValue);

                        if (state.Properties.ContainsKey("default"))
                        {
                            if (bool.Parse(state.Properties["default"].StringValue))
                            {
                                defaultStateId = stateId;
                            }
                        }

                        var props = new Dictionary<string, string>();

                        if (state.Properties.ContainsKey("properties"))
                        {
                            foreach (var prop in state.Properties["properties"].Properties)
                            {
                                props.Add(prop.Key, prop.Value.StringValue);
                            }
                        }

                        float hardness = float.Parse(state.Properties["hardness"]
                                .StringValue, CultureInfo.InvariantCulture);

                        int fullFaces = int.Parse(state.Properties["full_faces"].StringValue);
                        bool noCollision = bool.Parse(state.Properties["no_collision"].StringValue);
                        bool noOcclusion = bool.Parse(state.Properties["no_occlusion"].StringValue);

                        byte lightEmission = byte.Parse(state.Properties["light_emission"].StringValue);
                        byte lightBlockage = byte.Parse(state.Properties["light_blockage"].StringValue);

                        ResourceLocation? fluidStateId = null;

                        if (state.Properties.TryGetValue("fluid_state", out Json.JSONData fluidState))
                        {
                            fluidStateId = ResourceLocation.FromString(fluidState.StringValue);
                        }

                        states[stateId] = new BlockState(blockId, blastResistance, hardness, noSolidMesh, fullFaces,
                                noCollision, noOcclusion, lightBlockage, lightEmission, fluidStateId, props)
                        {
                            // Assign non-readonly fields
                            Friction = friction,
                            JumpFactor = jumpFactor,
                            SpeedFactor = speedFactor,
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

                if (colorRules.Properties.ContainsKey("dynamic"))
                {
                    foreach (var dynamicRule in colorRules.Properties["dynamic"].Properties)
                    {
                        var ruleName = dynamicRule.Key;

                        Func<World, BlockLoc, BlockState, float3> ruleFunc = ruleName switch
                        {
                            "foliage"  => (world, loc, state) => world.GetFoliageColor(loc),
                            "grass"    => (world, loc, state) => world.GetGrassColor(loc),
                            "redstone" => (world, loc, state) => {
                                if (state.Properties.ContainsKey("power"))
                                    return new(int.Parse(state.Properties["power"]) / 16F, 0F, 0F);
                                return float3.zero;
                            },

                            _         => (world, loc, state) => float3.zero
                        };

                        foreach (var block in dynamicRule.Value.DataArray)
                        {
                            var blockId = ResourceLocation.FromString(block.StringValue);

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
                            else
                            {
                                //Debug.LogWarning($"Applying dynamic color rules to undefined block {blockId}!");
                            }
                        }
                    }
                }

                if (colorRules.Properties.ContainsKey("fixed"))
                {
                    foreach (var fixedRule in colorRules.Properties["fixed"].Properties)
                    {
                        var blockId = ResourceLocation.FromString(fixedRule.Key);
                        var fixedColor = VectorUtil.Json2Float3(fixedRule.Value) / 255F;
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
                        else
                        {
                            //Debug.LogWarning($"Applying fixed color rules to undefined block {blockId}!");
                        }
                    }
                }
            
                // Load and apply block render types...
            
                var renderTypeText = File.ReadAllText(renderTypePath, Encoding.UTF8);
                var renderTypes = Json.ParseJson(renderTypeText);

                var allBlockIds = groupIdToGroup.Keys.ToHashSet();

                foreach (var pair in renderTypes.Properties)
                {
                    var blockId = ResourceLocation.FromString(pair.Key);

                    if (allBlockIds.Contains(blockId))
                    {
                        var type = pair.Value.StringValue.ToLower() switch
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

                        RenderTypeTable.Add(blockId, type);
                        allBlockIds.Remove(blockId);

                        Debug.Log($"{blockId} uses {type}");
                    }
                }

                foreach (var blockId in allBlockIds) // Other blocks which doesn't have its render type specifically stated
                {
                    RenderTypeTable.Add(blockId, RenderType.SOLID); // Default to solid
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