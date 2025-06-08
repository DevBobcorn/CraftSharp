#nullable enable

namespace CraftSharp.Inventory
{
    public record Enchantment(ResourceLocation EnchantmentId, int Level)
    {
        public ResourceLocation EnchantmentId { get; } = EnchantmentId;
        public int Level { get; } = Level;
    }
}