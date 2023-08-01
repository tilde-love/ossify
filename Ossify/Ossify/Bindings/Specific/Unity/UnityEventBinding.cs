using System;
using UnityEngine.Events;

namespace Ossify.Bindings.Specific.Unity
{
    public class UnityEventBinding<TArg> : EventBinding<UnityEvent<TArg>, TArg>
    {
        /// <inheritdoc />
        public UnityEventBinding(UnityEvent<TArg> bound) : base(bound) { }

        /// <inheritdoc />
        public override void Raise(TArg value)
        {
            Bound.Invoke(value);
        }

        [Serializable]
        public class UnityEvent : UnityEvent<TArg> { }
    }

    public class UnityEventBinding : EventBinding<UnityEvent>
    {
        /// <inheritdoc />
        public UnityEventBinding(UnityEvent bound) : base(bound) { }

        /// <inheritdoc />
        public override void Raise()
        {
            Bound.Invoke();
        }
    }
}