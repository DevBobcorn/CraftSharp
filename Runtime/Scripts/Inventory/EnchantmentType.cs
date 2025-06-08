namespace CraftSharp
{
    /// <summary>
    /// Represents a Minecraft Enchantment Type
    /// </summary>
    public record EnchantmentType
    {
        public static readonly EnchantmentType DUMMY_ENCHANTMENT_TYPE = new(ResourceLocation.INVALID);
        public ResourceLocation EnchantmentTypeId { get; }

        private readonly string translationKey;

        public EnchantmentType(ResourceLocation id)
        {
            EnchantmentTypeId = id;
            
            translationKey = id.GetTranslationKey("enchantment");
        }

        public string GetDescription()
        {
            return $"{EnchantmentTypeId}\nTranslation Key:\t{translationKey}";
        }

        public override string ToString() => EnchantmentTypeId.ToString();
    }
}