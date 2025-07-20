#nullable enable
using System;
using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components
{
    public record BannerPatternsComponent : StructuredComponent
    {
        public int NumberOfLayers { get; set; }
        public List<BannerLayer> Layers { get; set; } = new();

        public BannerPatternsComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            NumberOfLayers = DataTypes.ReadNextVarInt(data);

            for (var i = 0; i < NumberOfLayers; i++)
            {
                var patternType = DataTypes.ReadNextVarInt(data);
                Layers.Add(new BannerLayer
                {
                    PatternType = patternType,
                    AssetId = patternType == 0 ? ResourceLocation.FromString(DataTypes.ReadNextString(data)) : null,
                    TranslationKey = patternType == 0 ? DataTypes.ReadNextString(data) : null,
                    DyeColor = (CommonColors) DataTypes.ReadNextVarInt(data)
                });
            }
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(NumberOfLayers));

            if (NumberOfLayers > 0)
            {
                if (NumberOfLayers != Layers.Count)
                    throw new Exception("Can't serialize BannerPatternsComponent because NumberOfLayers and Layers.Count differ!");

                foreach (var bannerLayer in Layers)
                {
                    data.AddRange(DataTypes.GetVarInt(bannerLayer.PatternType));

                    if (bannerLayer.PatternType == 0)
                    {
                        if(bannerLayer.AssetId is null || string.IsNullOrEmpty(bannerLayer.TranslationKey))
                            throw new Exception("Can't serialize BannerPatternsComponent because AssetId or TranslationKey is null/empty!");
                        
                        data.AddRange(DataTypes.GetString(bannerLayer.AssetId.ToString()));
                        data.AddRange(DataTypes.GetString(bannerLayer.TranslationKey));
                    }
                    
                    data.AddRange(DataTypes.GetVarInt((int) bannerLayer.DyeColor));
                }
            }
            
            return new Queue<byte>(data);
        }
    }

    public class BannerLayer
    {
        public int PatternType { get; set; }
        public ResourceLocation? AssetId { get; set; } = null!;
        public string? TranslationKey { get; set; } = null!;
        public CommonColors DyeColor { get; set; }
    }
}