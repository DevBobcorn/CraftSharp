namespace CraftSharp
{
    /// <summary>
    /// Represents a Minecraft Mob Attribute
    /// </summary>
    public record MobAttribute(ResourceLocation MobAttributeId)
    {
        public static readonly MobAttribute DUMMY_MOB_ATTRIBUTE = new(ResourceLocation.INVALID);
        public ResourceLocation MobAttributeId { get; } = MobAttributeId;

        public override string ToString()
        {
            return MobAttributeId.ToString();
        }
    }
}