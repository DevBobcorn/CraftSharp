using System.Collections.Generic;

namespace CraftSharp
{
    public static class BiomeData
    {
        public static readonly ResourceLocation BADLANDS_ID                    = new("badlands");
        public static readonly ResourceLocation BAMBOO_JUNGLE_ID               = new("bamboo_jungle");
        public static readonly ResourceLocation BASALT_DELTAS_ID               = new("basalt_deltas");
        public static readonly ResourceLocation BEACH_ID                       = new("beach");
        public static readonly ResourceLocation BIRCH_FOREST_ID                = new("birch_forest");
        public static readonly ResourceLocation CHERRY_GROVE_ID                = new("cherry_grove");
        public static readonly ResourceLocation COLD_OCEAN_ID                  = new("cold_ocean");
        public static readonly ResourceLocation CRIMSON_FOREST_ID              = new("crimson_forest");
        public static readonly ResourceLocation DARK_FOREST_ID                 = new("dark_forest");
        public static readonly ResourceLocation DEEP_COLD_OCEAN_ID             = new("deep_cold_ocean");
        public static readonly ResourceLocation DEEP_DARK_ID                   = new("deep_dark");
        public static readonly ResourceLocation DEEP_FROZEN_OCEAN_ID           = new("deep_frozen_ocean");
        public static readonly ResourceLocation DEEP_LUKEWARM_OCEAN_ID         = new("deep_lukewarm_ocean");
        public static readonly ResourceLocation DEEP_OCEAN_ID                  = new("deep_ocean");
        public static readonly ResourceLocation DESERT_ID                      = new("desert");
        public static readonly ResourceLocation DRIPSTONE_CAVES_ID             = new("dripstone_caves");
        public static readonly ResourceLocation END_BARRENS_ID                 = new("end_barrens");
        public static readonly ResourceLocation END_HIGHLANDS_ID               = new("end_highlands");
        public static readonly ResourceLocation END_MIDLANDS_ID                = new("end_midlands");
        public static readonly ResourceLocation ERODED_BADLANDS_ID             = new("eroded_badlands");
        public static readonly ResourceLocation FLOWER_FOREST_ID               = new("flower_forest");
        public static readonly ResourceLocation FOREST_ID                      = new("forest");
        public static readonly ResourceLocation FROZEN_OCEAN_ID                = new("frozen_ocean");
        public static readonly ResourceLocation FROZEN_PEAKS_ID                = new("frozen_peaks");
        public static readonly ResourceLocation FROZEN_RIVER_ID                = new("frozen_river");
        public static readonly ResourceLocation GROVE_ID                       = new("grove");
        public static readonly ResourceLocation ICE_SPIKES_ID                  = new("ice_spikes");
        public static readonly ResourceLocation JAGGED_PEAKS_ID                = new("jagged_peaks");
        public static readonly ResourceLocation JUNGLE_ID                      = new("jungle");
        public static readonly ResourceLocation LUKEWARM_OCEAN_ID              = new("lukewarm_ocean");
        public static readonly ResourceLocation LUSH_CAVES_ID                  = new("lush_caves");
        public static readonly ResourceLocation MANGROVE_SWAMP_ID              = new("mangrove_swamp");
        public static readonly ResourceLocation MEADOW_ID                      = new("meadow");
        public static readonly ResourceLocation MUSHROOM_FIELDS_ID             = new("mushroom_fields");
        public static readonly ResourceLocation NETHER_WASTES_ID               = new("nether_wastes");
        public static readonly ResourceLocation OCEAN_ID                       = new("ocean");
        public static readonly ResourceLocation OLD_GROWTH_BIRCH_FOREST_ID     = new("old_growth_birch_forest");
        public static readonly ResourceLocation OLD_GROWTH_PINE_TAIGA_ID       = new("old_growth_pine_taiga");
        public static readonly ResourceLocation OLD_GROWTH_SPRUCE_TAIGA_ID     = new("old_growth_spruce_taiga");
        public static readonly ResourceLocation PALE_GARDEN_ID                 = new("pale_garden");
        public static readonly ResourceLocation PLAINS_ID                      = new("plains");
        public static readonly ResourceLocation RIVER_ID                       = new("river");
        public static readonly ResourceLocation SAVANNA_ID                     = new("savanna");
        public static readonly ResourceLocation SAVANNA_PLATEAU_ID             = new("savanna_plateau");
        public static readonly ResourceLocation SMALL_END_ISLANDS_ID           = new("small_end_islands");
        public static readonly ResourceLocation SNOWY_BEACH_ID                 = new("snowy_beach");
        public static readonly ResourceLocation SNOWY_PLAINS_ID                = new("snowy_plains");
        public static readonly ResourceLocation SNOWY_SLOPES_ID                = new("snowy_slopes");
        public static readonly ResourceLocation SNOWY_TAIGA_ID                 = new("snowy_taiga");
        public static readonly ResourceLocation SOUL_SAND_VALLEY_ID            = new("soul_sand_valley");
        public static readonly ResourceLocation SPARSE_JUNGLE_ID               = new("sparse_jungle");
        public static readonly ResourceLocation STONY_PEAKS_ID                 = new("stony_peaks");
        public static readonly ResourceLocation STONY_SHORE_ID                 = new("stony_shore");
        public static readonly ResourceLocation SUNFLOWER_PLAINS_ID            = new("sunflower_plains");
        public static readonly ResourceLocation SWAMP_ID                       = new("swamp");
        public static readonly ResourceLocation TAIGA_ID                       = new("taiga");
        public static readonly ResourceLocation THE_END_ID                     = new("the_end");
        public static readonly ResourceLocation THE_VOID_ID                    = new("the_void");
        public static readonly ResourceLocation WARM_OCEAN_ID                  = new("warm_ocean");
        public static readonly ResourceLocation WARPED_FOREST_ID               = new("warped_forest");
        public static readonly ResourceLocation WINDSWEPT_FOREST_ID            = new("windswept_forest");
        public static readonly ResourceLocation WINDSWEPT_GRAVELLY_HILLS_ID    = new("windswept_gravelly_hills");
        public static readonly ResourceLocation WINDSWEPT_HILLS_ID             = new("windswept_hills");
        public static readonly ResourceLocation WINDSWEPT_SAVANNA_ID           = new("windswept_savanna");
        public static readonly ResourceLocation WOODED_BADLANDS_ID             = new("wooded_badlands");
        
        public static readonly Dictionary<ResourceLocation, int> BUILTIN_BIOME_NUM_IDS_1_20_6 = new()
        {
            [BADLANDS_ID]                  = 0,
            [BAMBOO_JUNGLE_ID]             = 1,
            [BASALT_DELTAS_ID]             = 2,
            [BEACH_ID]                     = 3,
            [BIRCH_FOREST_ID]              = 4,
            [CHERRY_GROVE_ID]              = 5,
            [COLD_OCEAN_ID]                = 6,
            [CRIMSON_FOREST_ID]            = 7,
            [DARK_FOREST_ID]               = 8,
            [DEEP_COLD_OCEAN_ID]           = 9,
            [DEEP_DARK_ID]                 = 10,
            [DEEP_FROZEN_OCEAN_ID]         = 11,
            [DEEP_LUKEWARM_OCEAN_ID]       = 12,
            [DEEP_OCEAN_ID]                = 13,
            [DESERT_ID]                    = 14,
            [DRIPSTONE_CAVES_ID]           = 15,
            [END_BARRENS_ID]               = 16,
            [END_HIGHLANDS_ID]             = 17,
            [END_MIDLANDS_ID]              = 18,
            [ERODED_BADLANDS_ID]           = 19,
            [FLOWER_FOREST_ID]             = 20,
            [FOREST_ID]                    = 21,
            [FROZEN_OCEAN_ID]              = 22,
            [FROZEN_PEAKS_ID]              = 23,
            [FROZEN_RIVER_ID]              = 24,
            [GROVE_ID]                     = 25,
            [ICE_SPIKES_ID]                = 26,
            [JAGGED_PEAKS_ID]              = 27,
            [JUNGLE_ID]                    = 28,
            [LUKEWARM_OCEAN_ID]            = 29,
            [LUSH_CAVES_ID]                = 30,
            [MANGROVE_SWAMP_ID]            = 31,
            [MEADOW_ID]                    = 32,
            [MUSHROOM_FIELDS_ID]           = 33,
            [NETHER_WASTES_ID]             = 34,
            [OCEAN_ID]                     = 35,
            [OLD_GROWTH_BIRCH_FOREST_ID]   = 36,
            [OLD_GROWTH_PINE_TAIGA_ID]     = 37,
            [OLD_GROWTH_SPRUCE_TAIGA_ID]   = 38,
            [PLAINS_ID]                    = 39,
            [RIVER_ID]                     = 40,
            [SAVANNA_ID]                   = 41,
            [SAVANNA_PLATEAU_ID]           = 42,
            [SMALL_END_ISLANDS_ID]         = 43,
            [SNOWY_BEACH_ID]               = 44,
            [SNOWY_PLAINS_ID]              = 45,
            [SNOWY_SLOPES_ID]              = 46,
            [SNOWY_TAIGA_ID]               = 47,
            [SOUL_SAND_VALLEY_ID]          = 48,
            [SPARSE_JUNGLE_ID]             = 49,
            [STONY_PEAKS_ID]               = 50,
            [STONY_SHORE_ID]               = 51,
            [SUNFLOWER_PLAINS_ID]          = 52,
            [SWAMP_ID]                     = 53,
            [TAIGA_ID]                     = 54,
            [THE_END_ID]                   = 55,
            [THE_VOID_ID]                  = 56,
            [WARM_OCEAN_ID]                = 57,
            [WARPED_FOREST_ID]             = 58,
            [WINDSWEPT_FOREST_ID]          = 59,
            [WINDSWEPT_GRAVELLY_HILLS_ID]  = 60,
            [WINDSWEPT_HILLS_ID]           = 61,
            [WINDSWEPT_SAVANNA_ID]         = 62,
            [WOODED_BADLANDS_ID]           = 63,

        };

        /// <summary>
        /// Builtin biome definitions
        /// </summary>
        public static readonly (ResourceLocation id, int numId, Dictionary<string, object> obj)[] BUILTIN_BIOMES_1_20_6 =
        {
            (BADLANDS_ID, 0, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["foliage_color"] = 10387789, ["grass_color"] = 9470285, ["sky_color"] = 7254527, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.0F, ["temperature"] = 2.0F, ["precipitation"] = "none"}),
            (BAMBOO_JUNGLE_ID, 1, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7842047, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.9F, ["temperature"] = 0.95F, ["precipitation"] = "none"}),
            (BASALT_DELTAS_ID, 2, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 6840176, ["sky_color"] = 7254527, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.0F, ["temperature"] = 2.0F, ["precipitation"] = "none"}),
            (BEACH_ID, 3, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7907327, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.4F, ["temperature"] = 0.8F, ["precipitation"] = "none"}),
            (BIRCH_FOREST_ID, 4, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8037887, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.6F, ["temperature"] = 0.6F, ["precipitation"] = "none"}),
            (CHERRY_GROVE_ID, 5, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["foliage_color"] = 11983713, ["grass_color"] = 11983713, ["sky_color"] = 8103167, ["water_color"] = 6141935, ["water_fog_color"] = 6141935, }, ["downfall"] = 0.8F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (COLD_OCEAN_ID, 6, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8103167, ["water_color"] = 4020182, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (CRIMSON_FOREST_ID, 7, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 3343107, ["sky_color"] = 7254527, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.0F, ["temperature"] = 2.0F, ["precipitation"] = "none"}),
            (DARK_FOREST_ID, 8, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7972607, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.8F, ["temperature"] = 0.7F, ["precipitation"] = "none"}),
            (DEEP_COLD_OCEAN_ID, 9, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8103167, ["water_color"] = 4020182, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (DEEP_DARK_ID, 10, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7907327, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.4F, ["temperature"] = 0.8F, ["precipitation"] = "none"}),
            (DEEP_FROZEN_OCEAN_ID, 11, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8103167, ["water_color"] = 3750089, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (DEEP_LUKEWARM_OCEAN_ID, 12, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8103167, ["water_color"] = 4566514, ["water_fog_color"] = 267827, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (DEEP_OCEAN_ID, 13, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8103167, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (DESERT_ID, 14, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7254527, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.0F, ["temperature"] = 2.0F, ["precipitation"] = "none"}),
            (DRIPSTONE_CAVES_ID, 15, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7907327, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.4F, ["temperature"] = 0.8F, ["precipitation"] = "none"}),
            (END_BARRENS_ID, 16, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 10518688, ["sky_color"] = 0, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (END_HIGHLANDS_ID, 17, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 10518688, ["sky_color"] = 0, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (END_MIDLANDS_ID, 18, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 10518688, ["sky_color"] = 0, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (ERODED_BADLANDS_ID, 19, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["foliage_color"] = 10387789, ["grass_color"] = 9470285, ["sky_color"] = 7254527, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.0F, ["temperature"] = 2.0F, ["precipitation"] = "none"}),
            (FLOWER_FOREST_ID, 20, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7972607, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.8F, ["temperature"] = 0.7F, ["precipitation"] = "none"}),
            (FOREST_ID, 21, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7972607, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.8F, ["temperature"] = 0.7F, ["precipitation"] = "none"}),
            (FROZEN_OCEAN_ID, 22, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8364543, ["water_color"] = 3750089, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.0F, ["precipitation"] = "none"}),
            (FROZEN_PEAKS_ID, 23, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8756735, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.9F, ["temperature"] = -0.7F, ["precipitation"] = "none"}),
            (FROZEN_RIVER_ID, 24, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8364543, ["water_color"] = 3750089, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.0F, ["precipitation"] = "none"}),
            (GROVE_ID, 25, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8495359, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.8F, ["temperature"] = -0.2F, ["precipitation"] = "none"}),
            (ICE_SPIKES_ID, 26, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8364543, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.0F, ["precipitation"] = "none"}),
            (JAGGED_PEAKS_ID, 27, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8756735, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.9F, ["temperature"] = -0.7F, ["precipitation"] = "none"}),
            (JUNGLE_ID, 28, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7842047, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.9F, ["temperature"] = 0.95F, ["precipitation"] = "none"}),
            (LUKEWARM_OCEAN_ID, 29, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8103167, ["water_color"] = 4566514, ["water_fog_color"] = 267827, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (LUSH_CAVES_ID, 30, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8103167, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (MANGROVE_SWAMP_ID, 31, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["foliage_color"] = 9285927, ["sky_color"] = 7907327, ["water_color"] = 3832426, ["water_fog_color"] = 5077600, }, ["downfall"] = 0.9F, ["temperature"] = 0.8F, ["precipitation"] = "none"}),
            (MEADOW_ID, 32, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8103167, ["water_color"] = 937679, ["water_fog_color"] = 329011, }, ["downfall"] = 0.8F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (MUSHROOM_FIELDS_ID, 33, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7842047, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 1.0F, ["temperature"] = 0.9F, ["precipitation"] = "none"}),
            (NETHER_WASTES_ID, 34, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 3344392, ["sky_color"] = 7254527, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.0F, ["temperature"] = 2.0F, ["precipitation"] = "none"}),
            (OCEAN_ID, 35, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8103167, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (OLD_GROWTH_BIRCH_FOREST_ID, 36, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8037887, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.6F, ["temperature"] = 0.6F, ["precipitation"] = "none"}),
            (OLD_GROWTH_PINE_TAIGA_ID, 37, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8168447, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.8F, ["temperature"] = 0.3F, ["precipitation"] = "none"}),
            (OLD_GROWTH_SPRUCE_TAIGA_ID, 38, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8233983, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.8F, ["temperature"] = 0.25F, ["precipitation"] = "none"}),
            (PLAINS_ID, 39, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7907327, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.4F, ["temperature"] = 0.8F, ["precipitation"] = "none"}),
            (RIVER_ID, 40, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8103167, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (SAVANNA_ID, 41, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7254527, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.0F, ["temperature"] = 2.0F, ["precipitation"] = "none"}),
            (SAVANNA_PLATEAU_ID, 42, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7254527, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.0F, ["temperature"] = 2.0F, ["precipitation"] = "none"}),
            (SMALL_END_ISLANDS_ID, 43, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 10518688, ["sky_color"] = 0, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (SNOWY_BEACH_ID, 44, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8364543, ["water_color"] = 4020182, ["water_fog_color"] = 329011, }, ["downfall"] = 0.3F, ["temperature"] = 0.05F, ["precipitation"] = "none"}),
            (SNOWY_PLAINS_ID, 45, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8364543, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.0F, ["precipitation"] = "none"}),
            (SNOWY_SLOPES_ID, 46, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8560639, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.9F, ["temperature"] = -0.3F, ["precipitation"] = "none"}),
            (SNOWY_TAIGA_ID, 47, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8625919, ["water_color"] = 4020182, ["water_fog_color"] = 329011, }, ["downfall"] = 0.4F, ["temperature"] = -0.5F, ["precipitation"] = "none"}),
            (SOUL_SAND_VALLEY_ID, 48, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 1787717, ["sky_color"] = 7254527, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.0F, ["temperature"] = 2.0F, ["precipitation"] = "none"}),
            (SPARSE_JUNGLE_ID, 49, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7842047, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.8F, ["temperature"] = 0.95F, ["precipitation"] = "none"}),
            (STONY_PEAKS_ID, 50, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7776511, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.3F, ["temperature"] = 1.0F, ["precipitation"] = "none"}),
            (STONY_SHORE_ID, 51, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8233727, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.3F, ["temperature"] = 0.2F, ["precipitation"] = "none"}),
            (SUNFLOWER_PLAINS_ID, 52, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7907327, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.4F, ["temperature"] = 0.8F, ["precipitation"] = "none"}),
            (SWAMP_ID, 53, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["foliage_color"] = 6975545, ["sky_color"] = 7907327, ["water_color"] = 6388580, ["water_fog_color"] = 2302743, }, ["downfall"] = 0.9F, ["temperature"] = 0.8F, ["precipitation"] = "none"}),
            (TAIGA_ID, 54, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8233983, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.8F, ["temperature"] = 0.25F, ["precipitation"] = "none"}),
            (THE_END_ID, 55, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 10518688, ["sky_color"] = 0, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (THE_VOID_ID, 56, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8103167, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (WARM_OCEAN_ID, 57, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8103167, ["water_color"] = 4445678, ["water_fog_color"] = 270131, }, ["downfall"] = 0.5F, ["temperature"] = 0.5F, ["precipitation"] = "none"}),
            (WARPED_FOREST_ID, 58, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 1705242, ["sky_color"] = 7254527, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.0F, ["temperature"] = 2.0F, ["precipitation"] = "none"}),
            (WINDSWEPT_FOREST_ID, 59, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8233727, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.3F, ["temperature"] = 0.2F, ["precipitation"] = "none"}),
            (WINDSWEPT_GRAVELLY_HILLS_ID, 60, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8233727, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.3F, ["temperature"] = 0.2F, ["precipitation"] = "none"}),
            (WINDSWEPT_HILLS_ID, 61, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 8233727, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.3F, ["temperature"] = 0.2F, ["precipitation"] = "none"}),
            (WINDSWEPT_SAVANNA_ID, 62, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["sky_color"] = 7254527, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.0F, ["temperature"] = 2.0F, ["precipitation"] = "none"}),
            (WOODED_BADLANDS_ID, 63, new Dictionary<string, object> {["effects"] = new Dictionary<string, object> {["fog_color"] = 12638463, ["foliage_color"] = 10387789, ["grass_color"] = 9470285, ["sky_color"] = 7254527, ["water_color"] = 4159204, ["water_fog_color"] = 329011, }, ["downfall"] = 0.0F, ["temperature"] = 2.0F, ["precipitation"] = "none"}),

        };
    }
}