using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_20_6
{
    public record UnbreakableComponent1206 : StructuredComponent
    {
        public bool Unbreakable { get; set; }

        public UnbreakableComponent1206(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {
            
        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            Unbreakable = DataTypes.ReadNextBool(data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetBool(Unbreakable));
            return new Queue<byte>(data);
        }
    }
}