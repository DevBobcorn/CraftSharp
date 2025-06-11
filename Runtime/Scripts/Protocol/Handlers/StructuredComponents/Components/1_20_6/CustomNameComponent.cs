using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components
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
            CustomName = dataTypes.ReadNextChatAsJson(data).ToJson();
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetString(CustomName)); // TODO: Get chat as NBT
            return new Queue<byte>(data);
        }
    }
}