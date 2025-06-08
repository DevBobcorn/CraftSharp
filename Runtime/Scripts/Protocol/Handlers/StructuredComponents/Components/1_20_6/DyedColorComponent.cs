using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components
{
    public record DyedColorComponent : StructuredComponent
    {
        public int Color { get; set; }
        public bool ShowInTooltip { get; set; }

        public DyedColorComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            Color = DataTypes.ReadNextInt(data);
            ShowInTooltip = DataTypes.ReadNextBool(data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetInt(Color));
            data.AddRange(DataTypes.GetBool(ShowInTooltip));
            return new Queue<byte>(data);
        }
    }
}