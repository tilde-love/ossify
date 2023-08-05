using System.Threading;
using Cysharp.Threading.Tasks;
using Ossify.Variables;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ossify
{
    public abstract class Accumulator<TValue, TVariable, TReference> : CustodianBackbone 
        where TVariable : class, IVariable<TValue> 
        where TReference : Reference<TValue, TVariable>, new () 
    {
        [SerializeField] private TReference input = new () { Value = default };

        [SerializeField] private TReference output = new () { Value = default };

        [SerializeField] private TReference defaultValue = new () { Value = default };

        [SerializeField] private AccumulationMode mode = AccumulationMode.Add;

        public TReference Input => input;
        public TReference Output => output;
        public TReference DefaultValue => defaultValue;
        public AccumulationMode Mode => mode;

        public virtual void Reset() => output.Value = defaultValue.Value;

        /// <inheritdoc />
        protected override void Begin()
        {
            if (input.UseConstant == false && output.UseConstant == false && input.Variable == output.Variable)
            {
                Debug.LogError("Input and output cannot be the same variable.");
                return;
            }
 
            input.ValueChanged += OnValueChanged;
        }

        /// <inheritdoc />
        protected override void End() => input.ValueChanged -= OnValueChanged;

        protected abstract void OnValueChanged(TValue value); 
    }

    public enum AccumulationMode
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Average,
        Max,
        Min,
        None
    } 
}