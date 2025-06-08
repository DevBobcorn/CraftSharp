using System;
using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components
{
    public record AttributeModifiersComponent : StructuredComponent
    {
        public int NumberOfAttributes { get; set; }
        public List<AttributeSubComponent> Attributes { get; set; } = new();
        public bool ShowInTooltip { get; set; }

        public AttributeModifiersComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            NumberOfAttributes = DataTypes.ReadNextVarInt(data);

            for (var i = 0; i < NumberOfAttributes; i++)
                Attributes.Add((AttributeSubComponent)SubComponentRegistry.ParseSubComponent(SubComponents.Attribute, data));

            ShowInTooltip = DataTypes.ReadNextBool(data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(NumberOfAttributes));
            
            if(Attributes.Count != NumberOfAttributes)
                throw new ArgumentNullException($"Can not serialize a AttributeModifiersComponent when the Attributes count != NumberOfAttributes!");
            
            foreach (var attribute in Attributes)
                data.AddRange(attribute.Serialize(dataTypes));
            
            data.AddRange(DataTypes.GetBool(ShowInTooltip));
            return new Queue<byte>(data);
        }
    }
}