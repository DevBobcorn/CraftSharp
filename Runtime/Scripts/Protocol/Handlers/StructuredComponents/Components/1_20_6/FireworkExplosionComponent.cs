#nullable enable
using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components
{
    public record FireworkExplosionComponent : StructuredComponent
    {
        public FireworkExplosionSubComponent? FireworkExplosionSubComponent { get; set; }

        public FireworkExplosionComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            FireworkExplosionSubComponent = (FireworkExplosionSubComponent)SubComponentRegistry.ParseSubComponent(SubComponents.FireworkExplosion, data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            return FireworkExplosionSubComponent!.Serialize(dataTypes);
        }
    }
}