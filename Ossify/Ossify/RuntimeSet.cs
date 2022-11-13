using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ossify
{
    public abstract class RuntimeSet<T> : ScriptableObject, IEnumerable<T>
    {
        [SerializeField] private List<T> items = new();

        private IReadOnlyList<T> cached;

        public IReadOnlyList<T> Items => cached ??= items.AsReadOnly();

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)items).GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public event Action<T> Added;
        public event Action<T> Removed;

        public void Add(T item)
        {
            if (items.Contains(item))
            {
                return;
            }

            items.Add(item);

            Added?.Invoke(item);
        }

        public void Remove(T item)
        {
            if (items.Contains(item) == false)
            {
                return;
            }

            items.Remove(item);

            Removed?.Invoke(item);
        }
    }
}