using System;
using Ossify.Variables;
using UnityEngine;

namespace Ossify
{
    [CreateAssetMenu(menuName = "Variables/Float Accumulator")]
    public class FloatAccumulator : Accumulator<float, FloatVariable, FloatReference> 
    {
        /// <inheritdoc />
        protected override void OnValueChanged(float value)
        {            
            switch (Mode)
            {
                case AccumulationMode.Add:
                    Output.Value += value;
                    break;
                case AccumulationMode.Subtract:
                    Output.Value -= value;
                    break;
                case AccumulationMode.Multiply:
                    Output.Value *= value;
                    break;
                case AccumulationMode.Divide:
                    Output.Value /= value;
                    break;
                case AccumulationMode.Average:
                    Output.Value = (Output.Value + value) / 2;
                    break;
                case AccumulationMode.Max:
                    Output.Value = Mathf.Max(Output.Value, value);
                    break;
                case AccumulationMode.Min:
                    Output.Value = Mathf.Min(Output.Value, value);
                    break;
                case AccumulationMode.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}