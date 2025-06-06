using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_20_6
{
    public record MaxDamageComponent : StructuredComponent
    {
        public int MaxDamage { get; set; }

        public MaxDamageComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            MaxDamage = DataTypes.ReadNextVarInt(data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(MaxDamage));
            return new Queue<byte>(data);
        }
        
        public override void ParseFromJson(IMinecraftDataTypes dataTypes, Json.JSONData data)
        {
            MaxDamage = int.Parse(data.StringValue);
        }
    }
}