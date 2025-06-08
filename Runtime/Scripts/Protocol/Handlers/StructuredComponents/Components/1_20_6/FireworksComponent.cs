using System;
using System.Collections.Generic;
using System.Linq;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components
{
    public record FireworksComponent : StructuredComponent
    {
        public int FlightDuration { get; set; }
        public int NumberOfExplosions { get; set; }

        public List<FireworkExplosionSubComponent> Explosions { get; set; } = new();

        public FireworksComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            FlightDuration = DataTypes.ReadNextVarInt(data);
            NumberOfExplosions = DataTypes.ReadNextVarInt(data);

            if (NumberOfExplosions > 0)
            {
                for(var i = 0; i < NumberOfExplosions; i++)
                    Explosions.Add(
                        (FireworkExplosionSubComponent)SubComponentRegistry.ParseSubComponent(SubComponents.FireworkExplosion,
                            data));
            }
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(FlightDuration));
            data.AddRange(DataTypes.GetVarInt(NumberOfExplosions));
            if (NumberOfExplosions > 0)
            {
                if (NumberOfExplosions != Explosions.Count)
                    throw new Exception("Can't serialize FireworksComponent because NumberOfExplosions and the lenght of Explosions differ!");
                
                foreach(var explosion in Explosions)
                    data.AddRange(explosion.Serialize(dataTypes).ToList());
            }
            return new Queue<byte>(data);
        }
    }
}