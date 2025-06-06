using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_20_6
{
    public record OmniousBottleAmplifierComponent : StructuredComponent
    {
        public int Amplifier { get; set; }

        public OmniousBottleAmplifierComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            Amplifier = DataTypes.ReadNextVarInt(data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(Amplifier));
            return new Queue<byte>(data);
        }
    }
}