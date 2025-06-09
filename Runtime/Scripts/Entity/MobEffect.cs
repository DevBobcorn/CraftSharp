using System;

namespace CraftSharp
{
    /// <summary>
    /// Represents different categories of mob effects
    /// </summary>
    public enum MobEffectCategory
    {
        Beneficial,
        Harmful,
        Neutral
    }

    /// <summary>
    /// Represents a Minecraft Mob Effect
    /// </summary>
    public record MobEffect
    {
        public static readonly MobEffect DUMMY_MOB_EFFECT = new(ResourceLocation.INVALID, MobEffectCategory.Neutral, 0, false, Array.Empty<MobAttributeModifier>());
        public ResourceLocation MobEffectId { get; }

        public readonly MobEffectCategory Category;
        public readonly int Color;
        public readonly bool Instant;
        public readonly MobAttributeModifier[] Modifiers;
        private readonly string colorText;

        public MobEffect(ResourceLocation id, MobEffectCategory category, int color, bool instant, MobAttributeModifier[] modifiers)
        {
            MobEffectId = id;
            Category = category;
            Color = color;
            Instant = instant;
            Modifiers = modifiers;

            var colorCode = $"{color:x}".PadLeft(6, '0');
            colorText = $"<color=#{colorCode}>{colorCode}</color>";
        }

        public override string ToString()
        {
            return MobEffectId.ToString();
        }
    }
}