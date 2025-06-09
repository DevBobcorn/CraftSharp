#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents
{
    public record AttributeModifierSubComponent : SubComponent
    {
        public ResourceLocation MobAttributeId { get; set; }
        public Guid UUID { get; set; } // Used in 1.20.5-1.21.3, replaced with ResourceLocation in 1.21.4
        public string? Name { get; set; } // Used 1.20.5-1.21.3, removed in 1.21.4
        public ResourceLocation ModifierId { get; set; } // Used 1.20.5-1.21.3, removed in 1.21.4
        public double Value { get; set; }
        public MobAttributeModifier.Operations Operation { get; set; }
        public EquipmentSlot Slot { get; set; }

        public AttributeModifierSubComponent(SubComponentRegistry subComponentRegistry)
            : base(subComponentRegistry)
        {
            
        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            var mobAttributeNumId = DataTypes.ReadNextVarInt(data);
            MobAttributeId = MobAttributePalette.INSTANCE.GetIdByNumId(mobAttributeNumId);
            
            if (dataTypes.UseResourceLocationForMobAttributeModifierId) // 1.21.4+
            {
                UUID = Guid.Empty;
                ModifierId = ResourceLocation.FromString(DataTypes.ReadNextString(data));
            }
            else // 1.20.5-1.21.3
            {
                UUID = DataTypes.ReadNextUUID(data);
                Name = DataTypes.ReadNextString(data);
            }
            Value = DataTypes.ReadNextDouble(data);
            Operation = (MobAttributeModifier.Operations) DataTypes.ReadNextVarInt(data);
            Slot = (EquipmentSlot) DataTypes.ReadNextVarInt(data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            var mobAttributeNumId = MobAttributePalette.INSTANCE.GetNumIdById(MobAttributeId);
            data.AddRange(DataTypes.GetVarInt(mobAttributeNumId));

            if (dataTypes.UseResourceLocationForMobAttributeModifierId) // 1.21.4+
            {
                data.AddRange(DataTypes.GetString(ModifierId.ToString()));
            }
            else // 1.20.5-1.21.3
            {
                data.AddRange(DataTypes.GetUUID(UUID));
            
                if (string.IsNullOrEmpty(Name?.Trim()))
                    throw new Exception("Name is null or empty!");
            
                data.AddRange(DataTypes.GetString(Name));
            }
            data.AddRange(DataTypes.GetDouble(Value));
            data.AddRange(DataTypes.GetVarInt((int) Operation));
            data.AddRange(DataTypes.GetVarInt((int) Slot));
            return new Queue<byte>(data);
        }
        
        public override void ParseFromJson(IMinecraftDataTypes dataTypes, Json.JSONData data)
        {
            var modifier = data.Properties["modifier"];
            
            MobAttributeId = ResourceLocation.FromString(modifier.Properties["attribute"].StringValue);
            
            if (dataTypes.UseResourceLocationForMobAttributeModifierId) // 1.21.4+
            {
                UUID = Guid.Empty;
                ModifierId = ResourceLocation.FromString(modifier.Properties["modifier_id"].StringValue); 
            }
            else // 1.20.5-1.21.3
            {
                UUID = Guid.Parse(modifier.Properties["uuid"].StringValue);
                // Note that this shouldn't be used. In 1.16-1.20.4 all components are dummy (won't send to server),
                // in 1.20.5+ actual component data will be sent from server
                Name = "UwU";
            }
            Value = double.Parse(modifier.Properties["value"].StringValue, CultureInfo.InvariantCulture.NumberFormat);
            Operation = MobAttributeModifier.GetOperation(modifier.Properties["operation"].StringValue);
            
            Slot = EquipmentSlotHelper.GetEquipmentSlot(data.Properties["slot"].StringValue);
        }
    }
}