namespace CraftSharp
{
    public record ItemParticleExtraData : ParticleExtraData
    {
        public ItemStack ItemStack;

        public ItemParticleExtraData(ItemStack itemStack)
        {
            ItemStack = itemStack;
        }
    }
}