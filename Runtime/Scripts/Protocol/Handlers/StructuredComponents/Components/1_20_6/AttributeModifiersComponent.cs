using System;
using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components
{
    public record AttributeModifiersComponent : StructuredComponent
    {
        public int NumberOfModifiers { get; set; }
        public List<AttributeModifierSubComponent> Modifiers { get; set; } = new();
        public bool ShowInTooltip { get; set; }

        public AttributeModifiersComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            NumberOfModifiers = DataTypes.ReadNextVarInt(data);

            for (var i = 0; i < NumberOfModifiers; i++)
                Modifiers.Add((AttributeModifierSubComponent) SubComponentRegistry.ParseSubComponent(SubComponents.Attribute, data));

            ShowInTooltip = DataTypes.ReadNextBool(data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(NumberOfModifiers));
            
            if (Modifiers.Count != NumberOfModifiers)
                throw new Exception("Attributes count != NumberOfAttributes!");
            
            foreach (var attribute in Modifiers)
                data.AddRange(attribute.Serialize(dataTypes));
            
            data.AddRange(DataTypes.GetBool(ShowInTooltip));
            return new Queue<byte>(data);
        }
        
        public override void ParseFromJson(IMinecraftDataTypes dataTypes, Json.JSONData data)
        {
            var modifiers = data.Properties["modifiers"].DataArray;

            NumberOfModifiers = modifiers.Count;

            foreach (var modifierData in modifiers)
            {
                Modifiers.Add((AttributeModifierSubComponent) SubComponentRegistry
                    .ParseSubComponentFromJson(SubComponents.Attribute, modifierData));
            }
            
            ShowInTooltip = bool.Parse(data.Properties["show_in_tooltip"].StringValue);
        }
    }
}