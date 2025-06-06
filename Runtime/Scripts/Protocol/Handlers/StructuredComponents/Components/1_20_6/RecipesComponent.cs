#nullable enable
using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_20_6
{
    public record RecipesComponent : StructuredComponent
    {
        public Dictionary<string, object>? Nbt { get; set; }

        public RecipesComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            Nbt = DataTypes.ReadNextNbt(data, dataTypes.UseAnonymousNBT);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetNbt(Nbt));
            return new Queue<byte>(data);
        }
    }
}