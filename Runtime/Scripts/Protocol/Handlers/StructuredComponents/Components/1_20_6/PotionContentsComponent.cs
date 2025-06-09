using System;
using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components
{
    public record PotionContentsComponent : StructuredComponent
    {
        public bool HasPotionId { get; set; }
        public ResourceLocation PotionId { get; set; }
        public bool HasCustomColor { get; set; }
        public int CustomColor { get; set; }
        public int NumberOfCustomEffects { get; set; }
        public List<PotionEffectSubComponent> CustomEffects { get; set; } = new();

        public PotionContentsComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            HasPotionId = DataTypes.ReadNextBool(data);
            PotionId = HasPotionId ? PotionPalette.INSTANCE.GetIdByNumId(DataTypes.ReadNextVarInt(data)) : ResourceLocation.INVALID;
            HasCustomColor = DataTypes.ReadNextBool(data);
            CustomColor = HasCustomColor ? DataTypes.ReadNextInt(data) : 0;
            NumberOfCustomEffects = DataTypes.ReadNextVarInt(data);
            
            for(var i = 0; i < NumberOfCustomEffects; i++)
                CustomEffects.Add((PotionEffectSubComponent) SubComponentRegistry.ParseSubComponent(SubComponents.PotionEffect, data));
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetBool(HasPotionId));
            if (HasPotionId)
            {
                var potionNumId = PotionPalette.INSTANCE.GetNumIdById(PotionId);
                data.AddRange(DataTypes.GetVarInt(potionNumId));
            }
            data.AddRange(DataTypes.GetBool(HasCustomColor));
            if (HasCustomColor)
            {
                data.AddRange(DataTypes.GetInt(CustomColor));
            }

            if (NumberOfCustomEffects > 0)
            {
                if (CustomEffects.Count != NumberOfCustomEffects)
                    throw new Exception("NumberOfCustomEffects is different from the count of elements in the Effects list!");
                
                foreach (var effect in CustomEffects)
                    data.AddRange(effect.Serialize(dataTypes));
            }
            
            return new Queue<byte>(data);
        }
    }
}