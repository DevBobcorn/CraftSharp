namespace CraftSharp
{
    public record EntityMetaEntry
    {
        public string Name { get; }

        public EntityMetadataType DataType { get; }

        public EntityMetaEntry(string name, EntityMetadataType type)
        {
            Name = name;
            DataType = type;
        }
    }
}