namespace CraftSharp
{
    public record EntityMetaEntry
    {
        public string Name { get; }

        public EntityMetaDataType DataType { get; }

        public EntityMetaEntry(string name, EntityMetaDataType type)
        {
            Name = name;
            DataType = type;
        }
    }
}