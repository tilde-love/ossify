using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ossify
{
    [Serializable, InlineProperty]
    public abstract class Reference<TValue, TVariable> where TVariable : IVariable<TValue>
    {
        [HideLabel, HorizontalGroup("source", Width = 20), PropertyOrder(10)]
        public bool UseConstant = true;
        
        [ShowIf(nameof(UseConstant)), HideLabel, HorizontalGroup("source"), PropertyOrder(0)]
        public TValue ConstantValue;
        
        [HideIf(nameof(UseConstant)), HideLabel, HorizontalGroup("source"), PropertyOrder(0), SerializeReference]
        public TVariable Variable;
        
        [HideIf(nameof(UseConstant)), ShowInInspector, HideLabel, HorizontalGroup("value"), PropertyOrder(20)]
        private TValue EditorValue
        {
            get => Get();
            set => Set(value);
        }
        
        public TValue Value
        {
            get => Get();
            set => Set(value);
        }
        
        public void Set(TValue value)
        {
            if (UseConstant)
            {
                ConstantValue = value;
                ConstantValueChanged?.Invoke(value);
            }
            else
                Variable.Value = value;
        }
        
        public TValue Get() => UseConstant ? ConstantValue : Variable != null ? Variable.Value : default;

        public event Action<TValue> ValueChanged
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

        private event Action<TValue> ConstantValueChanged;
        
        public static implicit operator TValue(Reference<TValue, TVariable> reference) => reference.Value;
    }
}