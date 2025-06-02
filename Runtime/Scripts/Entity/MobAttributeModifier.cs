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
        public readonly string Attribute;
        public readonly Operations Operation;
        public readonly float Value;

        public MobAttributeModifier(Guid uuid, string attribute, Operations operation, float value)
        {
            UUID = uuid;
            Attribute = attribute;
            Operation = operation;
            Value = value;
        }
        
        public static MobAttributeModifier FromJson(Json.JSONData data)
        {
            var uuid = Guid.Parse(data.Properties["uuid"].StringValue);
            var attr = data.Properties["attribute"].StringValue;
            var op = GetOperation(data.Properties["operation"].StringValue);
            var value = float.Parse(data.Properties["value"].StringValue, CultureInfo.InvariantCulture.NumberFormat);

            return new MobAttributeModifier(uuid, attr, op, value);
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