using System;
using Ossify.Variables;
using UnityEngine;

namespace Ossify
{
    [CreateAssetMenu(menuName = "Variables/Vector2 Accumulator")]
    public class Vector2Accumulator : Accumulator<Vector2, Vector2Variable, Vector2Reference> 
    {
        /// <inheritdoc />
        protected override void OnValueChanged(Vector2 value)
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
                    Output.Value = Vector2.Max(Output.Value, value);
                    break;
                case AccumulationMode.Min:
                    Output.Value = Vector2.Min(Output.Value, value);
                    break;
                case AccumulationMode.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}