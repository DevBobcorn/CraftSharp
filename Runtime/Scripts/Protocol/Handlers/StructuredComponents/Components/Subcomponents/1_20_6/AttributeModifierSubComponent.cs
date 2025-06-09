#nullable enable
using System;
using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents
{
    public record AttributeModifierSubComponent : SubComponent
    {
        public int TypeId { get; set; }
        public Guid UUID { get; set; }
        public string? Name { get; set; }
        public double Value { get; set; }
        public MobAttributeModifier.Operations Operation { get; set; }
        public EquipmentSlot Slot { get; set; }

        public AttributeModifierSubComponent(SubComponentRegistry subComponentRegistry)
            : base(subComponentRegistry)
        {
            
        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            TypeId = DataTypes.ReadNextVarInt(data);
            UUID = DataTypes.ReadNextUUID(data);
            Name = DataTypes.ReadNextString(data);
            Value = DataTypes.ReadNextDouble(data);
            Operation = (MobAttributeModifier.Operations) DataTypes.ReadNextVarInt(data);
            Slot = (EquipmentSlot) DataTypes.ReadNextVarInt(data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(TypeId));
            data.AddRange(DataTypes.GetUUID(UUID));
            
            if (string.IsNullOrEmpty(Name?.Trim()))
                throw new Exception("Name is null or empty!");
            
            data.AddRange(DataTypes.GetString(Name));
            data.AddRange(DataTypes.GetDouble(Value));
            data.AddRange(DataTypes.GetVarInt((int) Operation));
            data.AddRange(DataTypes.GetVarInt((int) Slot));
            return new Queue<byte>(data);
        }
    }
}