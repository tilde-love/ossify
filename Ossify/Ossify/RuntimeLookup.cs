using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ossify
{
    public abstract class RuntimeLookup<TKey, TValue> : RuntimeSet<TValue>, IReadOnlyDictionary<TKey, TValue>
    {
        [SerializeField, PropertyOrder(-1)] private TValue @default;
        
        public TValue Default => @default;
        
        private Dictionary<TKey, TValue> lookup = null;
        
        protected IDictionary<TKey, TValue> Lookup => lookup ??= BuildLookup();

        protected abstract Dictionary<TKey, TValue> BuildLookup();

        protected abstract TKey GetKey(TValue value);

        /// <inheritdoc />
        protected override bool TryAdd(TValue item)
        {
            if (Lookup.ContainsKey(GetKey(item))) return false;

            if (base.TryAdd(item) == false) return false;
            
            Lookup.Add(GetKey(item), item);
            
            return true;
        }

        /// <inheritdoc />
        protected override bool TryRemove(TValue item)
        {
            if (base.TryRemove(item) == false) return false;
            
            Lookup.Remove(GetKey(item));

            return true;
        }

#if UNITY_EDITOR
        private void OnValidate() => lookup = null;
#endif

        /// <inheritdoc />
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
            => Lookup.GetEnumerator();

        /// <inheritdoc />
        public int Count => Lookup.Count;

        /// <inheritdoc />
        public bool ContainsKey(TKey key) => Lookup.ContainsKey(key);

        /// <inheritdoc />
        public bool TryGetValue(TKey key, out TValue value) => Lookup.TryGetValue(key, out value);

        /// <inheritdoc />
        public TValue this[TKey key] => Lookup[key];

        /// <inheritdoc />
        public IEnumerable<TKey> Keys => Lookup.Keys;

        /// <inheritdoc />
        public IEnumerable<TValue> Values => Lookup.Values;
    }
}