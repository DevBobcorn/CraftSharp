using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CraftSharp
{
    /// <summary>
    /// Palette for objects registered with both a numeral id and an identifier
    /// </summary>
    public abstract class IdentifierPalette<T> : ProtocolIdPalette<T>
    {
        private static readonly ResourceLocation UNKNOWN_ID = ResourceLocation.INVALID;

        protected readonly Dictionary<ResourceLocation, int> idToNumId = new();
        private readonly Dictionary<int, ResourceLocation> numIdToId = new();

        protected override void ClearEntries()
        {
            base.ClearEntries();
            numIdToId.Clear();
            idToNumId.Clear();
        }

        /// <summary>
        /// Get all identifiers in the palette
        /// </summary>
        /// <returns></returns>
        public ResourceLocation[] GetAllIds()
        {
            return idToNumId.Keys.ToArray();
        }

        /// <summary>
        /// Check if given id is present
        /// </summary>
        public bool CheckId(ResourceLocation id)
        {
            return idToNumId.ContainsKey(id);
        }

        /// <summary>
        /// Get object by id, or default object if not found
        /// </summary>
        public T GetById(ResourceLocation id)
        {
            if (idToNumId.TryGetValue(id, out int numId))
            {
                if (numIdToObject.TryGetValue(numId, out T obj))
                {
                    return obj;
                }
            }

            return UnknownObject;
        }

        /// <summary>
        /// Try to get object by id, or default object if not found
        /// </summary>
        public bool TryGetById(ResourceLocation id, out T obj)
        {
            if (idToNumId.TryGetValue(id, out int numId))
            {
                if (numIdToObject.TryGetValue(numId, out obj))
                {
                    return true;
                }
            }

            obj = UnknownObject;
            return false;
        }

        /// <summary>
        /// Get id by object, or invalid id if not found
        /// </summary>
        public ResourceLocation GetIdByObject(T obj)
        {
            if (obj is not null && objectToNumId.TryGetValue(obj, out int numId))
            {
                if (numIdToId.TryGetValue(numId, out ResourceLocation id))
                {
                    return id;
                }
            }

            return UNKNOWN_ID;
        }

        public ResourceLocation GetIdByNumId(int numId)
        {
            return numIdToId.GetValueOrDefault(numId, UNKNOWN_ID);
        }

        public int GetNumIdById(ResourceLocation id)
        {
            return idToNumId.GetValueOrDefault(id, UNKNOWN_NUM_ID);
        }

        /// <summary>
        /// Update object by id
        /// </summary>
        public void UpdateById(ResourceLocation id, T obj)
        {
            if (EntriesFrozen)
            {
                throw new InvalidOperationException("Cannot update object value for frozen palette!");
            }

            if (idToNumId.TryGetValue(id, out int numId))
            {
                if (objectToNumId.TryGetValue(obj, out int registeredNumId))
                {
                    var registeredId = numIdToId[registeredNumId];
                    throw new InvalidOperationException($"The object is already registered with id [{registeredNumId}] {registeredId}");
                }
                else
                {
                    objectToNumId.Remove(numIdToObject[numId]);
                    numIdToObject[numId] = obj;
                    objectToNumId.Add(obj, numId);
                }
            }
        }

        /// <summary>
        /// Add an entry to the palette
        /// </summary>
        protected virtual void AddEntry(ResourceLocation id, int numId, T obj)
        {
            if (EntriesFrozen)
            {
                throw new InvalidOperationException("Cannot add to a frozen palette");
            }

            if (obj is null)
            {
                Debug.LogWarning($"Trying to add null into {Name} ({id}, {numId})");
                return;
            }

            if (!idToNumId.ContainsKey(id))
            {
                if (!numIdToObject.ContainsKey(numId))
                {
                    if (!objectToNumId.ContainsKey(obj))
                    {
                        numIdToObject.Add(numId, obj);
                        objectToNumId.Add(obj, numId);
                        idToNumId.Add(id, numId);
                        numIdToId.Add(numId, id);
                    }
                    else
                    {
                        Debug.LogWarning($"Object already registered in {Name}: {obj}");
                    }
                }
                else
                {
                    Debug.LogWarning($"Numeral id already registered in {Name}: {numId}");
                }
            }
            else
            {
                Debug.LogWarning($"Identifier already registered in {Name}: {id}");
            }
        }

        /// <summary>
        /// Add a directional entry to the palette, which is only accessible using numId.
        /// Used for special handling for some predifined entries.
        /// </summary>
        protected virtual void AddDirectionalEntry(ResourceLocation id, int numId, T obj)
        {
            if (EntriesFrozen)
            {
                throw new InvalidOperationException("Cannot add to a frozen palette");
            }

            if (obj is null)
            {
                Debug.LogWarning($"Trying to add null into {Name} ({id}, {numId})");
                return;
            }
            
            if (numIdToObject.TryAdd(numId, obj))
            {
                numIdToId.Add(numId, id);
            }
            else
            {
                Debug.LogWarning($"Numeral id already registered in {Name}: {numId}");
            }
        }
    }
}