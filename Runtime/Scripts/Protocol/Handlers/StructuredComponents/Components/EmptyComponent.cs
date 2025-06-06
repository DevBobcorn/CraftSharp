using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components
{
    public record EmptyComponent : StructuredComponent
    {
        public EmptyComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {
            
        }

        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            return new Queue<byte>();
        }
    }
}