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
        public static readonly MobEffect DUMMY_MOB_EFFECT = new(ResourceLocation.INVALID, MobEffectCategory.Neutral, 0);
        public ResourceLocation MobEffectId { get; }

        public readonly MobEffectCategory Category;
        private readonly string colorText;

        public MobEffect(ResourceLocation id, MobEffectCategory category, int color)
        {
            MobEffectId = id;
            Category = category;

            var colorCode = $"{color:x}".PadLeft(6, '0');
            colorText = $"<color=#{colorCode}>{colorCode}</color>";
        }

        public string GetDescription()
        {
            return $"{MobEffectId}\nCategory: {Category}\t{colorText}";
        }

        public override string ToString() => MobEffectId.ToString();
    }
}