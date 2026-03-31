using System;
using System.Collections.Generic;

namespace DotnetToolbox.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default)
        {
            return dict.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> factory)
        {
            if (!dict.TryGetValue(key, out var value))
            {
                value = factory();
                dict[key] = value;
            }
            return value;
        }

        public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(
            this IDictionary<TKey, TValue> dict,
            IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            foreach (var item in items)
                dict[item.Key] = item.Value;
            return dict;
        }

        public static Dictionary<TValue, TKey> Invert<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            var inverted = new Dictionary<TValue, TKey>();
            foreach (var kvp in dict)
            {
                if (!inverted.ContainsKey(kvp.Value))
                    inverted[kvp.Value] = kvp.Key;
            }
            return inverted;
        }
    }
}
