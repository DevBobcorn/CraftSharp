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
    /// Represents a Minecraft Biome
    /// </summary>
    public record Biome
    {
        public float Temperature = 0F;
        public float Downfall    = 0F;
        
        public Precipitation Precipitation = Precipitation.None;

        public readonly int SkyColorInt, FoliageColorInt, GrassColorInt;
        public readonly int FogColorInt, WaterColorInt, WaterFogColorInt;
        public readonly float3 SkyColor, FoliageColor, GrassColor;
        public readonly float3 FogColor, WaterColor, WaterFogColor;
        private readonly string colorsText;

        private static float3 GetFloat3Color(int color)
        {
            float r = ((color & 0xFF0000) >> 16) / 255F;
            float g = ((color &   0xFF00) >>  8) / 255F;
            float b = ((color &     0xFF))       / 255F;

            return new(r, g, b);
        }

        public ResourceLocation BiomeBiomeId { get; }

        private static string GetColorText(int color)
        {
            var colorCode = $"{color:x}".PadLeft(6, '0');
            return $"<color=#{colorCode}>{colorCode}</color>";
        }
        
        public Biome(ResourceLocation biomeId, int sky, int foliage, int grass, int water, int fog, int waterFog)
        {
            BiomeBiomeId = biomeId;

            // Set biome colors
            SkyColor = GetFloat3Color(sky);
            SkyColorInt = sky;
            FoliageColor = GetFloat3Color(foliage);
            FoliageColorInt = foliage;
            GrassColor = GetFloat3Color(grass);
            GrassColorInt = grass;
            WaterColor = GetFloat3Color(water);
            WaterColorInt = water;
            FogColor = GetFloat3Color(fog);
            FogColorInt = fog;
            WaterFogColor = GetFloat3Color(waterFog);
            WaterFogColorInt = waterFog;

            colorsText = $"{GetColorText(SkyColorInt)} {GetColorText(FoliageColorInt)} {GetColorText(GrassColorInt)}\n" +
                         $"{GetColorText(FogColorInt)} {GetColorText(WaterColorInt)} {GetColorText(WaterFogColorInt)}";
        }

        public Biome(ResourceLocation biomeId, Dictionary<string, object> nbt)
        {
            if (nbt == null)
                throw new ArgumentNullException(nameof (nbt));
            
            BiomeBiomeId = biomeId;
            
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
                    SkyColor = GetFloat3Color(SkyColorInt);
                }
                
                var adjustedTemp = Mathf.Clamp01(Temperature);
                var adjustedRain = Mathf.Clamp01(Downfall) * adjustedTemp;

                int sampleX = (int) ((1F - adjustedTemp) * World.ColormapSize);
                int sampleY = (int) (adjustedRain * World.ColormapSize);

                if (effects.TryGetValue("foliage_color", out val))
                {
                    FoliageColorInt = (int) val;
                    FoliageColor = GetFloat3Color(FoliageColorInt);
                }
                else // Read foliage color from color map. See https://minecraft.fandom.com/wiki/Color
                {
                    var color = (World.FoliageColormapPixels.Length == 0) ? (Color32) Color.magenta :
                        World.FoliageColormapPixels[sampleY * World.ColormapSize + sampleX];
                    FoliageColorInt = (color.r << 16) | (color.g << 8) | color.b;
                    FoliageColor = GetFloat3Color(FoliageColorInt);
                }

                if (effects.TryGetValue("grass_color", out val))
                {
                    GrassColorInt = (int) val;
                    GrassColor = GetFloat3Color(GrassColorInt);
                }
                else // Read grass color from color map. Same as above
                {
                    var color = (World.GrassColormapPixels.Length == 0) ? (Color32) Color.magenta :
                        World.GrassColormapPixels[sampleY * World.ColormapSize + sampleX];
                    GrassColorInt = (color.r << 16) | (color.g << 8) | color.b;
                    GrassColor = GetFloat3Color(GrassColorInt);
                }

                if (effects.TryGetValue("fog_color", out val))
                {
                    FogColorInt = (int) val;
                    FogColor = GetFloat3Color(FogColorInt);
                }

                if (effects.TryGetValue("water_color", out val))
                {
                    WaterColorInt = (int) val;
                    WaterColor = GetFloat3Color(WaterColorInt);
                }

                if (effects.TryGetValue("water_fog_color", out val))
                {
                    WaterFogColorInt = (int) val;
                    WaterFogColor = GetFloat3Color(WaterFogColorInt);
                }
                
                colorsText = $"{GetColorText(SkyColorInt)} {GetColorText(FoliageColorInt)} {GetColorText(GrassColorInt)}\n" +
                             $"{GetColorText(FogColorInt)} {GetColorText(WaterColorInt)} {GetColorText(WaterFogColorInt)}";
            }
        }
        
        public string GetDescription()
        {
            return $"{BiomeBiomeId}\nTemperature: {Temperature:0.00}\tDownfall: {Precipitation} {Downfall:0.00}\n{colorsText}";
        }

        public override string ToString() => BiomeBiomeId.ToString();
    }
}