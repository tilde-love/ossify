using System;
using System.Collections;
using System.Collections.Generic;

namespace Ossify.Activations
{
    public sealed class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> dictionary = new();
        
        public event Action<TKey, TValue> ItemAdded;
        
        public event Action<TKey, TValue> ItemSet;
        
        public event Action<TKey, TValue> ItemRemoved;
        
        /// <inheritdoc />
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dictionary.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)dictionary).GetEnumerator();

        /// <inheritdoc />
        public void Add(KeyValuePair<TKey, TValue> item) => (dictionary as IDictionary<TKey, TValue>).Add(item);

        /// <inheritdoc />
        public void Clear()
        {
            foreach (var item in dictionary)
            {
                ItemRemoved?.Invoke(item.Key, item.Value);
            }
            
            dictionary.Clear();
        }

        /// <inheritdoc />
        public bool Contains(KeyValuePair<TKey, TValue> item) => (dictionary as IDictionary<TKey, TValue>).Contains(item);

        /// <inheritdoc />
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => (dictionary as IDictionary<TKey, TValue>).CopyTo(array, arrayIndex);

        /// <inheritdoc />
        public bool Remove(KeyValuePair<TKey, TValue> item) => (dictionary as IDictionary<TKey, TValue>).Remove(item);

        /// <inheritdoc />
        public int Count => dictionary.Count;

        /// <inheritdoc />
        public bool IsReadOnly => (dictionary as IDictionary<TKey, TValue>).IsReadOnly;

        /// <inheritdoc />
        public void Add(TKey key, TValue value)
        {
            dictionary.Add(key, value);
            
            ItemAdded?.Invoke(key, value);
        }

        /// <inheritdoc />
        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        /// <inheritdoc />
        public bool Remove(TKey key)
        {
            if (dictionary.TryGetValue(key, out var value) == false)
            {
                return false;
            }

            dictionary.Remove(key);
                
            ItemRemoved?.Invoke(key, value);
                
            return true;

        }

        /// <inheritdoc />
        public bool TryGetValue(TKey key, out TValue value) => dictionary.TryGetValue(key, out value);

        /// <inheritdoc />
        public TValue this[TKey key]
        {
            get => dictionary[key];
            set 
            { 
                dictionary[key] = value;
                
                ItemSet?.Invoke(key, value);
            }
        }

        /// <inheritdoc />
        public ICollection<TKey> Keys => dictionary.Keys;

        /// <inheritdoc />
        public ICollection<TValue> Values => dictionary.Values;
    }
}