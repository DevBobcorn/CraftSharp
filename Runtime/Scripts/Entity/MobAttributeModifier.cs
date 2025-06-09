using System;
using System.Globalization;
using System.IO;

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

        public readonly Guid UUID;
        public readonly ResourceLocation ModifierId;
        public readonly ResourceLocation Attribute;
        public readonly Operations Operation;
        public readonly double Value;

        public MobAttributeModifier(Guid uuid, ResourceLocation modifierId, ResourceLocation attribute, Operations operation, double value)
        {
            UUID = uuid;
            ModifierId = modifierId;
            Attribute = attribute;
            Operation = operation;
            Value = value;
        }
        
        public static MobAttributeModifier FromJson(Json.JSONData data)
        {
            var uuid = Guid.Parse(data.Properties["uuid"].StringValue);
            var m_id = ResourceLocation.FromString(data.Properties["id"].StringValue);
            var attr = ResourceLocation.FromString(data.Properties["attribute"].StringValue);
            var op = GetOperation(data.Properties["operation"].StringValue);
            var value = double.Parse(data.Properties["value"].StringValue, CultureInfo.InvariantCulture.NumberFormat);

            return new MobAttributeModifier(uuid, m_id, attr, op, value);
        }

        private static Operations GetOperation(string operationName)
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