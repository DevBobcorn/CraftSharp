using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;
using CraftSharp.Protocol.Message;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components
{
    public record ItemNameComponent : StructuredComponent
    {
        public string ItemName { get; set; } = string.Empty;

        public ItemNameComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            ItemName = ChatParser.ParseText(DataTypes.ReadNextString(data));
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetString(ItemName));
            return new Queue<byte>(data);
        }
    }
}