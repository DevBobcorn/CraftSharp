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
        public static readonly ResourceLocation UNKNOWN_ID = ResourceLocation.INVALID;

        protected readonly Dictionary<ResourceLocation, int> idToNumId = new();
        protected readonly Dictionary<int, ResourceLocation> numIdToId = new();

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
        /// Try get object by id, or default object if not found
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

        /// <summary>
        /// Get numId by object, or invalid id if not found
        /// </summary>
        public int GetNumIdByObject(T obj)
        {
            if (obj is not null && objectToNumId.TryGetValue(obj, out int numId))
            {
                return numId;
            }

            return UNKNOWN_NUM_ID;
        }

        public ResourceLocation GetIdByNumId(int numId)
        {
            if (numIdToId.TryGetValue(numId, out ResourceLocation id))
            {
                return id;
            }
            
            return UNKNOWN_ID;
        }

        public int GetNumIdById(ResourceLocation id)
        {
            if (idToNumId.TryGetValue(id, out int numId))
            {
                return numId;
            }
            
            return UNKNOWN_NUM_ID;
        }

        /// <summary>
        /// Add an entry to the palette
        /// </summary>
        protected virtual void AddEntry(ResourceLocation id, int numId, T obj)
        {
            if (EntriesFrozen)
            {
                throw new InvalidOperationException("Cannot add to a frozon palette");
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
                throw new InvalidOperationException("Cannot add to a frozon palette");
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
                        //objectToNumId.Add(obj, numId);
                        //idToNumId.Add(id, numId);
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
    }
}