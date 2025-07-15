using System.Collections.Generic;
using UnityEngine;

namespace CraftSharp
{
    /// <summary>
    /// Palette for objects registered without a numeral id (protocol id)
    /// <br/>
    /// Entries are allowed to be added after entering the server, or even
    /// defined in-line within a packet from the server, thus 'dynamic'.
    /// </summary>
    public abstract class DynamicPalette<T>
    {
        protected abstract string Name { get; }
        protected abstract T UnknownObject { get; }
        
        protected readonly Dictionary<ResourceLocation, T> idToObject = new();
        
        /// <summary>
        /// Get object by id, or default object if not found
        /// </summary>
        public T GetById(ResourceLocation id)
        {
            if (idToObject.TryGetValue(id, out T obj))
            {
                return obj;
            }

            return UnknownObject;
        }
        
        /// <summary>
        /// Try get object by id, or default object if not found
        /// </summary>
        public bool TryGetById(ResourceLocation id, out T obj)
        {
            if (idToObject.TryGetValue(id, out obj))
            {
                return true;
            }

            obj = UnknownObject;
            return false;
        }
        
        /// <summary>
        /// Add an entry to the palette
        /// </summary>
        protected virtual void AddEntry(ResourceLocation id, T obj)
        {
            if (obj is null)
            {
                Debug.LogWarning($"Trying to add null into {Name} ({id})");
                return;
            }

            if (!idToObject.TryAdd(id, obj))
            {
                Debug.LogWarning($"Identifier already registered in {Name}: {id}");
            }
        }
    }
}