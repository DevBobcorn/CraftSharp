using System;
using System.Collections.Generic;
using System.Linq;

namespace CraftSharp
{
    /// <summary>
    /// Palette for objects registered with a numeral id (protocol id)
    /// </summary>
    public abstract class ProtocolIdPalette<T>
    {
        protected const int UNKNOWN_NUM_ID = -1;

        protected abstract string Name { get; }
        protected abstract T UnknownObject { get; }

        protected readonly Dictionary<int, T> numIdToObject = new();
        protected readonly Dictionary<T, int> objectToNumId = new();

        protected bool EntriesFrozen { get; private set; } = false;

        /// <summary>
        /// Get all numIds in the palette
        /// </summary>
        public int[] GetAllNumIds(bool ignoreDirectional = true)
        {
            if (ignoreDirectional)
            {
                return numIdToObject.Where(x => objectToNumId.
                        ContainsKey(x.Value)).Select(x => x.Key).ToArray();
            }
            else
            {
                return numIdToObject.Keys.ToArray();
            }
        }

        protected virtual void ClearEntries()
        {
            EntriesFrozen = false;

            numIdToObject.Clear();
            objectToNumId.Clear();
        }

        /// <summary>
        /// Check if given numId is present
        /// </summary>
        public bool CheckNumId(int numId)
        {
            return numIdToObject.ContainsKey(numId);
        }

        /// <summary>
        /// Get object by numId, or default object if not found
        /// </summary>
        public T GetByNumId(int numId)
        {
            if (numIdToObject.TryGetValue(numId, out T obj))
            {
                return obj;
            }

            return UnknownObject;
        }

        /// <summary>
        /// Try get object from numId, or default object if not found
        /// </summary>
        public bool TryGetByNumId(int numId, out T obj)
        {
            if (numIdToObject.TryGetValue(numId, out obj))
            {
                return true;
            }

            obj = UnknownObject;
            return false;
        }

        public void FreezeEntries()
        {
            EntriesFrozen = true;
        }

        public void UnfreezeEntries()
        {
            EntriesFrozen = false;
        }
    }
}