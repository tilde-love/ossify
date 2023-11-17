using System;
using UnityEngine;
using Object = System.Object;

namespace Ossify
{
    public abstract class Accumulator<TValue, TVariable, TReference> : CustodianBackbone 
        where TValue : IEquatable<TValue>
        where TVariable : Variable<TValue> 
        where TReference : Reference<TValue, TVariable>, new () 
    {
        [SerializeField] private TReference input = new () { Value = default };

        [SerializeField] private TReference output = new () { Value = default };

        [SerializeField] private TReference defaultValue = new () { Value = default };

        [SerializeField] private AccumulationMode mode = AccumulationMode.Add;

        private CallMeOneTime.Call call;
        private TValue value; 

        public TReference Input => input;
        public TReference Output => output;
        public TReference DefaultValue => defaultValue;
        public AccumulationMode Mode => mode;

        public virtual void Reset() => output.Value = defaultValue.Value;

        /// <inheritdoc />
        protected override void Begin()
        {
            if (input.UseConstant == false && output.UseConstant == false && Equals(input.Variable, output.Variable))
            {
                Debug.LogError("Input and output cannot be the same variable.");
                return;
            }
 
            input.ValueChanged += OnPreValueChanged;

            call = CallMeOneTime.Get(FinaliseValue);
        }

        private void FinaliseValue()
        {
            output.Value = value;

            value = defaultValue.Value;

            if (output.Value.Equals(defaultValue.Value) == false) CallMeOneTime.Enqueue(call);
        }

        /// <inheritdoc />
        protected override void End() => input.ValueChanged -= OnPreValueChanged;

        private void OnPreValueChanged(TValue value)
        {
            this.value = CalculateNewValue(this.value, value);

            CallMeOneTime.Enqueue(call);
        }    

        protected abstract TValue CalculateNewValue(TValue current, TValue value); 
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