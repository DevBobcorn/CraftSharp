using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;
using CraftSharp.Protocol.Message;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_20_6
{
    public record CustomNameComponent : StructuredComponent
    {
        public string CustomName { get; set; } = string.Empty;

        public CustomNameComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            CustomName = ChatParser.ParseText(DataTypes.ReadNextString(data));
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetString(CustomName));
            return new Queue<byte>(data);
        }
    }
}