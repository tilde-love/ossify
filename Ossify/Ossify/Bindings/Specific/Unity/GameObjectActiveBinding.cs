using System;
using UnityEngine;

namespace Ossify.Bindings.Specific.Unity
{
    public class GameObjectActiveBinding : GetterBinding<GameObject, bool>
    {
        /// <inheritdoc />
        public GameObjectActiveBinding(GameObject bound, Func<bool> getter) : base(bound, getter) { }

        /// <inheritdoc />
        protected override void SetValue(bool value)
        {
            Bound.SetActive(value);
        }
    }
}