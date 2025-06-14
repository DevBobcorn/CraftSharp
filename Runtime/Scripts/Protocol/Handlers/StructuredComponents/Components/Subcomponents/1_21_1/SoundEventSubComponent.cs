#nullable enable
using System;
using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents
{
    public record SoundEventSubComponent : SubComponent
    {
        public int Type { get; set; }
        public string? SoundName { get; set; }
        public bool HasFixedRange { get; set; }
        public float FixedRange { get; set; }

        public SoundEventSubComponent(SubComponentRegistry subComponentRegistry)
            : base(subComponentRegistry)
        {
            
        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            Type = DataTypes.ReadNextVarInt(data);

            if (Type != 0) return;
            
            SoundName = DataTypes.ReadNextString(data);
            HasFixedRange = DataTypes.ReadNextBool(data);

            if (HasFixedRange)
                FixedRange = DataTypes.ReadNextFloat(data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(Type));

            if (Type != 0) return new Queue<byte>(data);
            
            if (string.IsNullOrEmpty(SoundName?.Trim()))
                throw new ArgumentNullException($"Can not serialize SoundEventSubComponent due to SoundName being null or empty!");
                
            data.AddRange(DataTypes.GetString(SoundName));
            data.AddRange(DataTypes.GetBool(HasFixedRange));
                
            if(HasFixedRange)
                data.AddRange(DataTypes.GetFloat(FixedRange));

            return new Queue<byte>(data);
        }
    }
}