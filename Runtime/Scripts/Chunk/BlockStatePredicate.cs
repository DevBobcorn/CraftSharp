using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CraftSharp
{
    public class BlockStatePredicate
    {
        public static readonly BlockStatePredicate EMPTY = new BlockStatePredicate(new Dictionary<string, string>());

        private readonly Dictionary<string, string> conditions = new Dictionary<string, string>();

        public BlockStatePredicate(Dictionary<string, string> conditions)
        {
            this.conditions = conditions;
        }

        public static BlockStatePredicate FromString(string source)
        {
            if (source == string.Empty || source == "normal")
                return EMPTY;
            
            var conditions = new Dictionary<string, string>();
            string[] srcs = source.Split(',');
            foreach (var src in srcs)
            {
                if (src.Contains('='))
                {
                    string[] keyVal = src.Split('=', 2);
                    conditions.Add(keyVal[0], keyVal[1]);
                }
                else
                {
                    Debug.Log($"Invalid prop condition: <{src}>");
                }
            }
            return new BlockStatePredicate(conditions);
        }

        public static BlockStatePredicate FromJson(Json.JSONData data)
        {
            if (data.Properties.Count == 0)
                return EMPTY;
            
            var conditions = new Dictionary<string, string>();
            foreach (var (key, value) in data.Properties)
            {
                conditions.Add(key, value.StringValue);
            }
            return new BlockStatePredicate(conditions);
        }

        public bool Check(BlockState state)
        {
            foreach (var condition in conditions)
            {
                // Check if the key exists...
                if (!state.Properties.ContainsKey(condition.Key))
                    return false;
                
                // Multiple allowed values are supported, separated with symbol '|'
                string[] allowedValues = condition.Value.Split('|');

                // Check if the value matches..
                if (!allowedValues.Contains(state.Properties[condition.Key]))
                    return false;

            }

            return true;
        }
    }
}
