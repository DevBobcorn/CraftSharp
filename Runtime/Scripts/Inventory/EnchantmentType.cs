namespace CraftSharp.Inventory
{
    /// <summary>
    /// Represents a Minecraft Enchantment Type
    /// </summary>
    public record EnchantmentType
    {
        public static readonly EnchantmentType DUMMY_ENCHANTMENT_TYPE = new(ResourceLocation.INVALID, ResourceLocation.INVALID.GetTranslationKey("enchantment"));
        public ResourceLocation EnchantmentTypeId { get; }

        public string TranslationKey { get; }

        public EnchantmentType(ResourceLocation id, string key)
        {
            EnchantmentTypeId = id;
            
            TranslationKey = key;
        }

        public string GetDescription()
        {
            return $"{EnchantmentTypeId}\nTranslation Key:\t{TranslationKey}";
        }

        public override string ToString() => EnchantmentTypeId.ToString();
    }
}