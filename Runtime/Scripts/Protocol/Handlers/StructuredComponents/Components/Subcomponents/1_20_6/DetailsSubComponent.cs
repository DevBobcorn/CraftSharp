#nullable enable
using System;
using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents._1_20_6
{
    public record DetailsSubComponent : SubComponent
    {
        public int Amplifier { get; set; }
        public int Duration { get; set; }
        public bool Ambient { get; set; }
        public bool ShowParticles { get; set; }
        public bool ShowIcon { get; set; }
        public bool HasHiddenEffects { get; set; }
        public DetailsSubComponent? Detail { get; set; }

        public DetailsSubComponent(SubComponentRegistry subComponentRegistry)
            : base(subComponentRegistry)
        {
            
        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            Amplifier = DataTypes.ReadNextVarInt(data);
            Duration = DataTypes.ReadNextVarInt(data);
            Ambient = DataTypes.ReadNextBool(data);
            ShowParticles = DataTypes.ReadNextBool(data);
            ShowIcon = DataTypes.ReadNextBool(data);
            HasHiddenEffects = DataTypes.ReadNextBool(data);
            
            if (HasHiddenEffects)
                Detail = (DetailsSubComponent)SubComponentRegistry.ParseSubComponent(SubComponents.Details, data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(Amplifier));
            data.AddRange(DataTypes.GetVarInt(Duration));
            data.AddRange(DataTypes.GetBool(Ambient));
            data.AddRange(DataTypes.GetBool(ShowParticles));
            data.AddRange(DataTypes.GetBool(ShowIcon));
            data.AddRange(DataTypes.GetBool(HasHiddenEffects));

            if (HasHiddenEffects)
            {
                if(Detail is null)
                    throw new ArgumentNullException($"Can not serialize a DetailSubComponent1206 when the Detail is empty but HasHiddenEffects is true!");
                    
                data.AddRange(Detail.Serialize(dataTypes));
            }

            return new Queue<byte>(data);
        }
    }
}