using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ossify
{
    public abstract class RuntimeSet<T> : ScriptableObject, IEnumerable<T>
    {
        [SerializeField] private List<T> items = new();
        
        public event System.Action<T> Added;
        public event System.Action<T> Removed;
        
        private IReadOnlyList<T> cached;
        
        public IReadOnlyList<T> Items => cached ??= items.AsReadOnly();

        public void Add(T item)
        {
            if (items.Contains(item)) return;

            items.Add(item);
            
            Added?.Invoke(item);
        }
        
        public void Remove(T item)
        {
            if (items.Contains(item) == false) return;

            items.Remove(item);
            
            Removed?.Invoke(item);
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)items).GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}