using System;

namespace CraftSharp
{
    /// <summary>
    /// Represents a Minecraft Potion
    /// </summary>
    public record Potion
    {
        public static readonly Potion DUMMY_POTION = new(ResourceLocation.INVALID, Array.Empty<MobEffectInstance>());
        public ResourceLocation PotionId { get; }

        public readonly MobEffectInstance[] Effects;

        public Potion(ResourceLocation id, MobEffectInstance[] effects)
        {
            PotionId = id;
            Effects = effects;
        }

        public override string ToString() => PotionId.ToString();
    }
}