using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace CraftSharp
{
    /// <summary>
    /// Represents different types of precipitations
    /// </summary>
    public enum Precipitation
    {
        None,
        Snow,
        Rain,
        Unknown
    }

    /// <summary>
    /// Overrides for biome colors
    /// <br/>
    /// See https://minecraft.wiki/w/Biome#Special_plant_tints
    /// </summary>
    public enum BiomeColorOverride
    {
        None = 0,
        Swamp
    }

    /// <summary>
    /// Represents a Minecraft Biome
    /// </summary>
    public record Biome
    {
        public float Temperature = 0F;
        public float Downfall    = 0F;
        
        public Precipitation Precipitation = Precipitation.None;

        public readonly int SkyColorInt, FoliageColorInt, GrassColorInt;
        public readonly int FogColorInt, WaterColorInt, WaterFogColorInt;
        private readonly string colorsText;
        
        public BiomeColorOverride ColorOverride = BiomeColorOverride.None;

        private static readonly PerlinSimplexNoise BIOME_INFO_NOISE = new(new LegacyRandomSource(2345L), new List<int> { 0 });

        public ResourceLocation BiomeId { get; }

        private static string GetColorText(int color)
        {
            var colorCode = $"{color:x}".PadLeft(6, '0');
            return $"<color=#{colorCode}>{colorCode}</color>";
        }

        private void UpdateColorOverride()
        {
            if (BiomeId.Path.Contains("swamp"))
            {
                ColorOverride = BiomeColorOverride.Swamp;
            }
        }

        public static int GetOverrideGrassColor(BiomeColorOverride o, int x, int z)
        {
            return o switch
            {
                BiomeColorOverride.Swamp => BIOME_INFO_NOISE.GetValue(x * 0.0225, z * 0.0225, false) < -0.1F
                    ? 5011004 : 6975545,
                
                _ => 0x00FFFFFF
            };
        }
        
        public Biome(ResourceLocation biomeId, int sky, int foliage, int grass, int water, int fog, int waterFog)
        {
            BiomeId = biomeId;
            
            UpdateColorOverride();

            // Set biome colors
            SkyColorInt = sky;
            FoliageColorInt = foliage;
            GrassColorInt = grass;
            WaterColorInt = water;
            FogColorInt = fog;
            WaterFogColorInt = waterFog;

            colorsText = $"{GetColorText(SkyColorInt)} {GetColorText(FoliageColorInt)} {GetColorText(GrassColorInt)}\n" +
                         $"{GetColorText(FogColorInt)} {GetColorText(WaterColorInt)} {GetColorText(WaterFogColorInt)}";
        }

        public Biome(ResourceLocation biomeId, Dictionary<string, object> nbt)
        {
            if (nbt == null)
                throw new ArgumentNullException(nameof (nbt));
            
            BiomeId = biomeId;

            UpdateColorOverride();
            
            if (nbt.TryGetValue("downfall", out var val))
                Downfall = (float) val;
                            
            if (nbt.TryGetValue("temperature", out val))
                Temperature = (float) val;
            
            if (nbt.TryGetValue("precipitation", out val))
            {
                Precipitation = ((string) val).ToLower() switch
                {
                    "rain" => Precipitation.Rain,
                    "snow" => Precipitation.Snow,
                    "none" => Precipitation.None,

                    _      => Precipitation.Unknown
                };

                if (Precipitation == Precipitation.Unknown)
                    Debug.LogWarning($"Unexpected precipitation type: {nbt["precipitation"]}");
            }

            if (nbt.TryGetValue("effects", out val))
            {
                var effects = (Dictionary<string, object>) val;

                if (effects.TryGetValue("sky_color", out val))
                {
                    SkyColorInt = (int) val;
                }
                
                var adjustedTemp = Mathf.Clamp01(Temperature);
                var adjustedRain = Mathf.Clamp01(Downfall) * adjustedTemp;

                int sampleX = (int) ((1F - adjustedTemp) * World.ColormapSize);
                int sampleY = (int) (adjustedRain * World.ColormapSize);

                if (effects.TryGetValue("foliage_color", out val))
                {
                    FoliageColorInt = (int) val;
                }
                else // Read foliage color from color map. See https://minecraft.fandom.com/wiki/Color
                {
                    var color = (World.FoliageColormapPixels.Length == 0) ? (Color32) Color.magenta :
                        World.FoliageColormapPixels[sampleY * World.ColormapSize + sampleX];
                    FoliageColorInt = (color.r << 16) | (color.g << 8) | color.b;
                }

                if (effects.TryGetValue("grass_color", out val))
                {
                    GrassColorInt = (int) val;
                }
                else // Read grass color from color map. Same as above
                {
                    var color = (World.GrassColormapPixels.Length == 0) ? (Color32) Color.magenta :
                        World.GrassColormapPixels[sampleY * World.ColormapSize + sampleX];
                    GrassColorInt = (color.r << 16) | (color.g << 8) | color.b;
                }

                if (effects.TryGetValue("fog_color", out val))
                {
                    FogColorInt = (int) val;
                }

                if (effects.TryGetValue("water_color", out val))
                {
                    WaterColorInt = (int) val;
                }

                if (effects.TryGetValue("water_fog_color", out val))
                {
                    WaterFogColorInt = (int) val;
                }
                
                colorsText = $"{GetColorText(SkyColorInt)} {GetColorText(FoliageColorInt)} {GetColorText(GrassColorInt)}\n" +
                             $"{GetColorText(FogColorInt)} {GetColorText(WaterColorInt)} {GetColorText(WaterFogColorInt)}";
            }
        }
        
        public string GetDescription()
        {
            return $"{BiomeId}\nTemperature: {Temperature:0.00}\tDownfall: {Precipitation} {Downfall:0.00}\n{colorsText}";
        }

        public override string ToString() => BiomeId.ToString();
    }
}