#nullable enable
using System;
using System.Globalization;
using System.IO;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents;

namespace CraftSharp
{
    public record MobAttributeModifier
    {
        public enum Operations
        {
            AddValue = 0,
            AddMultipliedBase,
            AddMultipliedTotal
        }

        public Guid UUID; // Used in 1.20.5-1.20.6, replaced with ResourceLocation in 1.21
        public ResourceLocation ModifierId; // Used 1.20.5-1.20.6, removed in 1.21
        public readonly ResourceLocation Attribute;
        public readonly Operations Operation;
        public readonly double Value;

        // Used 1.20.5-1.20.6
        public MobAttributeModifier(Guid uuid, ResourceLocation attribute, Operations operation, double value)
        {
            UUID = uuid;
            Attribute = attribute;
            Operation = operation;
            Value = value;
        }
        
        // Used 1.21+
        public MobAttributeModifier(ResourceLocation modifierId, ResourceLocation attribute, Operations operation, double value)
        {
            ModifierId = modifierId;
            Attribute = attribute;
            Operation = operation;
            Value = value;
        }
        
        public static MobAttributeModifier FromJson(Json.JSONData data, bool useResourceLocationForMobAttributeModifierId)
        {
            var attr = ResourceLocation.FromString(data.Properties["attribute"].StringValue);
            var op = GetOperation(data.Properties["operation"].StringValue);
            var value = double.Parse(data.Properties["value"].StringValue, CultureInfo.InvariantCulture.NumberFormat);

            if (useResourceLocationForMobAttributeModifierId) // 1.21+
            {
                var modifierId = ResourceLocation.FromString(data.Properties["id"].StringValue);
                
                return new MobAttributeModifier(modifierId, attr, op, value);
            }
            else // 1.20.5-1.20.6
            {
                var uuid = Guid.Parse(data.Properties["uuid"].StringValue);
                
                return new MobAttributeModifier(uuid, attr, op, value);
            }
        }

        public static MobAttributeModifier FromComponent(AttributeModifierSubComponent component)
        {
            if (component.UUID == Guid.Empty) // UUID is not used. 1.21+
            {
                return new MobAttributeModifier(component.ModifierId, component.MobAttributeId, component.Operation, component.Value);
            }
            else
            {
                return new MobAttributeModifier(component.UUID, component.MobAttributeId, component.Operation, component.Value);
            }
        }

        public static Operations GetOperation(string operationName)
        {
            return operationName switch
            {
                "addition" => Operations.AddValue,
                "add_value" => Operations.AddValue,
                
                "multiply_base" => Operations.AddMultipliedBase,
                "add_multiplied_base" => Operations.AddMultipliedBase,
                
                "multiply_total" => Operations.AddMultipliedTotal,
                "add_multiplied_total" => Operations.AddMultipliedTotal,

                _ => throw new InvalidDataException($"Mob attribute modifier {operationName} is not defined!")
            };
        }
    }
}