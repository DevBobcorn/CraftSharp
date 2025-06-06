using System;
using System.Collections.Generic;
using System.Globalization;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents._1_20_6
{
    public record EffectSubComponent : SubComponent
    {
        public PotionEffectSubComponent PotionEffect { get; set; }
        public float Probability { get; set; }

        public EffectSubComponent(SubComponentRegistry subComponentRegistry)
            : base(subComponentRegistry)
        {
            
        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            PotionEffect = (PotionEffectSubComponent) SubComponentRegistry.ParseSubComponent(SubComponents.PotionEffect, data);
            Probability = DataTypes.ReadNextFloat(data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(PotionEffect.Serialize(dataTypes));
            data.AddRange(DataTypes.GetFloat(Probability));
            return new Queue<byte>(data);
        }
    }
}