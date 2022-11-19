using UnityEngine;

namespace Ossify.Variables
{
    public abstract class Subscriber<TValue, TVariable> : MonoBehaviour where TVariable : Variable<TValue>
    {
        public class Event : UnityEngine.Events.UnityEvent<TValue> { }
        
        [SerializeField] private TVariable variable;
        
        [SerializeField] private Event onValueChanged = new ();

        public TValue Value { get; private set; }

        public Event OnValueChanged => onValueChanged;

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
    }
}