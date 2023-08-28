namespace Chart.Core
{
    public static partial class DictionaryExtensions
    {
        /// <summary>
        /// Set the given key in the dictionary to the given value.
        /// If the key doesn't exist, it is added.
        /// If it already exists, it is overwritten.
        /// </summary>
        /// <param name="dictionary">The dictionary to perform the operation on.</param>
        /// <param name="key">The key to set the value at.</param>
        /// <param name="value">The value to set in the dictionary.</param>
        public static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
            where TKey : notnull
        {
            if(dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// Get a value from the dictionary using the given key, or create a new value, if not found.
        /// </summary>
        /// <param name="dictionary">The dictionary to perform the operation on.</param>
        /// <param name="key">The key to get the value from.</param>
        /// <returns>
        /// A reference to the value at the given key, if it exists within the dictionary.
        /// If it doesn't exist, a new value is created and added to the dictionary, which is then returned.
        /// </returns>
        public static TValue GetValueOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
            where TKey : notnull
            where TValue : new()
        {
            if(dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            TValue value = new();
            dictionary[key] = value;

            return value;
        }


        /// <summary>
        /// Get a value from the dictionary using the given key, or create a new value, if not found.
        /// </summary>
        /// <param name="dictionary">The dictionary to perform the operation on.</param>
        /// <param name="key">The key to get the value from.</param>
        /// <param name="valueFactory">Factory for creating new values.</param>
        /// <returns>
        /// A reference to the value at the given key, if it exists within the dictionary.
        /// If it doesn't exist, a new value is created and added to the dictionary, which is then returned.
        /// </returns>
        public static TValue GetValueOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueFactory)
            where TKey : notnull
        {
            if(dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            TValue value = valueFactory(key);
            dictionary[key] = value;

            return value;
        }
    }
}