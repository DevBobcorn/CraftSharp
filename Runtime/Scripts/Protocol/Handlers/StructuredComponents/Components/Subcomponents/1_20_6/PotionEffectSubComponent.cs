using System;
using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents
{
    public record PotionEffectSubComponent : SubComponent
    {
        public int TypeId { get; set; }
        public DetailsSubComponent Details { get; set; }

        public PotionEffectSubComponent(SubComponentRegistry subComponentRegistry)
            : base(subComponentRegistry)
        {
            
        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            TypeId = DataTypes.ReadNextVarInt(data);
            Details = (DetailsSubComponent)SubComponentRegistry.ParseSubComponent(SubComponents.Details, data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(TypeId));
            data.AddRange(Details.Serialize(dataTypes));
            return new Queue<byte>(data);
        }
    }
}