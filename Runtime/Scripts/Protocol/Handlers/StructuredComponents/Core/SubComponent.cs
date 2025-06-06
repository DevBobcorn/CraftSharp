using System.Collections.Generic;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Core
{
    public abstract record SubComponent
    {
        protected SubComponentRegistry SubComponentRegistry { get; private set; }

        public SubComponent(SubComponentRegistry subComponentRegistry)
        {
            SubComponentRegistry = subComponentRegistry;
        }

        protected abstract void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data);
        
        public abstract Queue<byte> Serialize(IMinecraftDataTypes dataTypes);
    }
}