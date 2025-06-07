using System;
using System.Collections.Generic;

namespace CraftSharp
{

    /// <summary>
    /// The dimension type, available after 1.16.2
    /// </summary>
    public class DimensionType
    {
        /// <summary>
        /// The id of the dimension type (for example, "minecraft:overworld").
        /// </summary>
        public readonly ResourceLocation Id;

        /// <summary>
        /// Whether piglins shake and transform to zombified piglins.
        /// </summary>
        public readonly bool piglinSafe = false;

        /// <summary>
        /// Possibly the light level(s) at which monsters can spawn.
        /// </summary>
        public readonly int monsterSpawnMinLightLevel = 0;
        public readonly int monsterSpawnMaxLightLevel = 7;
        public readonly int monsterSpawnBlockLightLimit = 0;

        /// <summary>
        /// When false, compasses spin randomly. When true, nether portals can spawn zombified piglins.
        /// </summary>
        public readonly bool natural = true;

        /// <summary>
        /// How much light the dimension has.
        /// </summary>
        public readonly float ambientLight = 0.0f;


        /// <summary>
        /// If set, the time of the day is the specified value.
        /// Value: -1: not set
        /// Value: [0, 24000]: time of the day
        /// </summary>
        public readonly long fixedTime = -1;

        /// <summary>
        /// A resource location defining what block tag to use for infiniburn.
        /// Value above 1.18.2: "#" or minecraft resource "#minecraft:...".
        /// Value below 1.18.1: "" or minecraft resource "minecraft:...".
        /// </summary>
        public readonly string infiniburn = "#minecraft:infiniburn_overworld";

        /// <summary>
        /// Whether players can charge and use respawn anchors.
        /// </summary>
        public readonly bool respawnAnchorWorks = false;

        /// <summary>
        /// Whether the dimension has skylight access or not.
        /// </summary>
        public readonly bool hasSkylight = true;

        /// <summary>
        /// Whether players can use a bed to sleep.
        /// </summary>
        public readonly bool bedWorks = true;

        /// <summary>
        /// unknown
        /// Values: "minecraft:overworld", "minecraft:the_nether", "minecraft:the_end" or something else.
        /// </summary>
        public readonly string effects = "minecraft:overworld";

        /// <summary>
        /// Whether players with the Bad Omen effect can cause a raid.
        /// </summary>
        public readonly bool hasRaids = true;

        /// <summary>
        /// The minimum Y level.
        /// </summary>
        public readonly int minY = 0;

        /// <summary>
        /// The maximum Y level.
        /// </summary>
        public readonly int maxY = 255;

        /// <summary>
        /// The maximum height.
        /// </summary>
        public readonly int height = 256;

        /// <summary>
        /// The maximum height to which chorus fruits and nether portals can bring players within this dimension.
        /// </summary>
        public readonly int logicalHeight = 256;

        /// <summary>
        /// The multiplier applied to coordinates when traveling to the dimension.
        /// </summary>
        public readonly double coordinateScale = 1.0;

        /// <summary>
        /// Whether the dimensions behaves like the nether (water evaporates and sponges dry) or not. Also causes lava to spread thinner.
        /// </summary>
        public readonly bool ultrawarm = false;

        /// <summary>
        /// Whether the dimension has a bedrock ceiling or not. When true, causes lava to spread faster.
        /// </summary>
        public readonly bool hasCeiling = false;

        /// <summary>
        /// Default value used in version below 1.17
        /// </summary>
        public DimensionType()
        {
            Id = new ResourceLocation("overworld");

            minY = -64;
            height = 384;
            maxY = minY + height - 1;
            logicalHeight = 384;
        }

        /// <summary>
        /// Create from the "Dimension Codec" NBT Tag Compound
        /// </summary>
        /// <param name="id">Dimension id</param>
        /// <param name="nbt">The dimension type (NBT Tag Compound)</param>
        public DimensionType(ResourceLocation id, Dictionary<string, object> nbt)
        {
            if (nbt == null)
                throw new ArgumentNullException(nameof (nbt));

            Id = id;

            if (nbt.TryGetValue("piglin_safe", out var value))
                piglinSafe = 1 == (byte) value;
            
            if (nbt.TryGetValue("monster_spawn_light_level", out value))
            {
                try
                {
                    if (value is int lightLevel)
                        monsterSpawnMinLightLevel = monsterSpawnMaxLightLevel = lightLevel;
                    else
                    {
                        var inclusive = (Dictionary<string, object>)(((Dictionary<string, object>)value)["value"]);
                        monsterSpawnMinLightLevel = (int)inclusive["min_inclusive"];
                        monsterSpawnMaxLightLevel = (int)inclusive["max_inclusive"];
                    }

                }
                catch (KeyNotFoundException) { }
            }
            if (nbt.TryGetValue("monster_spawn_block_light_limit", out value))
                monsterSpawnBlockLightLimit = (int) value;
            
            if (nbt.TryGetValue("natural", out value))
                natural = 1 == (byte) value;
            
            if (nbt.TryGetValue("ambient_light", out value))
                ambientLight = (float) value;
            
            if (nbt.TryGetValue("fixed_time", out value))
                fixedTime = (long) value;
            
            if (nbt.TryGetValue("infiniburn", out value))
                infiniburn = (string) value;
            
            if (nbt.TryGetValue("respawn_anchor_works", out value))
                respawnAnchorWorks = 1 == (byte) value;
            
            if (nbt.TryGetValue("has_skylight", out value))
                hasSkylight = 1 == (byte) value;
            
            if (nbt.TryGetValue("bed_works", out value))
                bedWorks = 1 == (byte) value;
            
            if (nbt.TryGetValue("effects", out value))
                effects = (string) value;
            
            if (nbt.TryGetValue("has_raids", out value))
                hasRaids = 1 == (byte) value;
            
            if (nbt.TryGetValue("min_y", out value))
                minY = (int) value;
            
            if (nbt.TryGetValue("height", out value))
                height = (int) value;
            
            if (nbt.ContainsKey("min_y") && nbt.ContainsKey("height"))
                maxY = minY + height - 1;
            
            if (nbt.TryGetValue("logical_height", out value))
                logicalHeight = (int) value;
            
            if (nbt.TryGetValue("coordinate_scale", out value))
            {
                if (value is float scale)
                    coordinateScale = scale;
                else
                    coordinateScale = (double) value;
            }
            if (nbt.TryGetValue("ultrawarm", out value))
                ultrawarm = 1 == (byte) value;
            
            if (nbt.TryGetValue("has_ceiling", out value))
                hasCeiling = 1 == (byte) value;
            
        }
    }
}