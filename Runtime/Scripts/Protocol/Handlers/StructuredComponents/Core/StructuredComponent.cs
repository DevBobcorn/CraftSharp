using System.Collections.Generic;
using UnityEngine;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Core
{
    public abstract record StructuredComponent
    {
        protected SubComponentRegistry SubComponentRegistry { get; private set; }
        protected ItemPalette ItemPalette { get; private set; }

        public StructuredComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry)
        {
            ItemPalette = itemPalette;
            SubComponentRegistry = subComponentRegistry;
        }
        
        public abstract void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data);
        
        public abstract Queue<byte> Serialize(IMinecraftDataTypes dataTypes);

        public virtual void ParseFromJson(IMinecraftDataTypes dataTypes, Json.JSONData data)
        {
            Debug.LogWarning($"Json parser is not defined for {GetType()}!");
        }
    }
}