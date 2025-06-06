using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_20_6
{
    public record HideAdditionalTooltipComponent : EmptyComponent
    {
        public HideAdditionalTooltipComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
    }
}