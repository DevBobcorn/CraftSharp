using System;
using System.Collections.Generic;
using System.Globalization;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components
{
    public record FoodComponent : StructuredComponent
    {
        public int Nutrition { get; set; }
        public float Saturation { get; set; }
        public bool CanAlwaysEat { get; set; }
        public float SecondsToEat { get; set; }
        public int NumberOfEffects { get; set; }
        public List<EffectSubComponent> Effects { get; set; } = new();

        public FoodComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            Nutrition = DataTypes.ReadNextVarInt(data);
            Saturation = DataTypes.ReadNextFloat(data);
            CanAlwaysEat = DataTypes.ReadNextBool(data);
            SecondsToEat = DataTypes.ReadNextFloat(data);
            NumberOfEffects = DataTypes.ReadNextVarInt(data);
            
            for (var i = 0; i < NumberOfEffects; i++)
                Effects.Add((EffectSubComponent) SubComponentRegistry.ParseSubComponent(SubComponents.Effect, data));
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(Nutrition));
            data.AddRange(DataTypes.GetFloat(Saturation));
            data.AddRange(DataTypes.GetBool(CanAlwaysEat));
            data.AddRange(DataTypes.GetFloat(SecondsToEat));
            data.AddRange(DataTypes.GetVarInt(NumberOfEffects));

            if (NumberOfEffects > 0)
            {
                if(Effects.Count != NumberOfEffects)
                    throw new ArgumentNullException($"Can not serialize FoodComponent1206 due to NumberOfEffcets being different from the count of elements in the Effects list!");
                
                foreach(var effect in Effects)
                    data.AddRange(effect.Serialize(dataTypes));
            }
            
            return new Queue<byte>(data);
        }
        
        public override void ParseFromJson(IMinecraftDataTypes dataTypes, Json.JSONData data)
        {
            Nutrition = int.Parse(data.Properties["nutrition"].StringValue);
            Saturation = float.Parse(data.Properties["saturation"].StringValue,
                CultureInfo.InvariantCulture.NumberFormat);
            CanAlwaysEat = bool.Parse(data.Properties["can_always_eat"].StringValue);

            if (data.Properties.TryGetValue("fast_food", out var val) && bool.Parse(val.StringValue))
            {
                SecondsToEat = 0.8F;
            }
            else
            {
                SecondsToEat = 1.6F;
            }

            // TODO: Check if this data is potion or mob effect instance
            NumberOfEffects = 0;
        }
    }
}