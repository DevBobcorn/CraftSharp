using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_20_6
{
    public record HideTooltipComponent : EmptyComponent
    {
        public HideTooltipComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
    }
}