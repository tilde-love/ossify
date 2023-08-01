using UnityEngine;
using UnityEngine.Events;

namespace Ossify.Variables
{
    public abstract class Subscriber<TValue, TVariable> : MonoBehaviour where TVariable : Variable<TValue>
    {
        [SerializeField] private TVariable variable;

        [SerializeField] private UnityEvent<TValue> onValueChanged = new Event();

        public TValue Value { get; private set; }

        public UnityEvent<TValue> OnValueChanged => onValueChanged;

        private void OnEnable()
        {
            variable.ValueChanged += Changed;

            Changed(variable.Value);
        }

        private void OnDisable() => variable.ValueChanged -= Changed;

        protected virtual void Changed(TValue value)
        {
            if (Value.Equals(variable.Value)) return;

            Value = variable.Value;

            onValueChanged.Invoke(Value);
        }

        private class Event : UnityEvent<TValue> { }
    }
}