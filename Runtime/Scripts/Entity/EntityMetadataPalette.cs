using System;
using System.Collections.Generic;

namespace CraftSharp
{
    public abstract class EntityMetadataPalette
    {
        public abstract Dictionary<int, EntityMetadataType> GetEntityMetadataMappingsList();

        public EntityMetadataType GetDataType(int typeId)
        {
            return GetEntityMetadataMappingsList()[typeId];
        }
    }
}