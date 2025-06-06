using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_20_6
{
    public record BaseColorComponent : StructuredComponent
    {
        public int DyeColor { get; set; }

        public BaseColorComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            DyeColor = DataTypes.ReadNextVarInt(data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(DyeColor));
            return new Queue<byte>(data);
        }
    }
}