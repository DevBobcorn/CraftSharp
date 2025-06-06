using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_20_6
{
    public record CreativeSlotLockComponent : EmptyComponent
    {
        public CreativeSlotLockComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
    }
}