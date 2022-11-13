using System;
using Ossify.Variables;
using Sirenix.OdinInspector;

namespace Ossify.Variables
{
    [Serializable, InlineProperty]
    public abstract class Reference<T, T1> where T1 : IVariable<T>
    {
        [HideLabel, HorizontalGroup("value", Width = 20), PropertyOrder(10)]
        public bool UseConstant = true;
        
        [ShowIf(nameof(UseConstant)), HideLabel, HorizontalGroup("value"), PropertyOrder(0)]
        public T ConstantValue;
        
        [HideIf(nameof(UseConstant)), HideLabel, HorizontalGroup("value"), PropertyOrder(0)]
        public T1 Variable;

        public T Value
        {
            get => Get();
            set => Set(value);
        }
        
        public void Set(T value)
        {
            if (UseConstant)
            {
                ConstantValue = value;
                ConstantValueChanged?.Invoke(value);
            }
            else
                Variable.Value = value;
        }
        
        public T Get() => UseConstant ? ConstantValue : Variable.Value;

        public event Action<T> ValueChanged
        {
            add
            {
                if (UseConstant) ConstantValueChanged += value; 
                else Variable.ValueChanged += value;
            }
            remove             
            {
                if (UseConstant) ConstantValueChanged -= value; 
                else Variable.ValueChanged -= value;
            }
        }

        private event Action<T> ConstantValueChanged;
        
        public static implicit operator T(Reference<T, T1> reference) => reference.Value;
    }
}