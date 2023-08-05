using System;
using Ossify.Variables;
using UnityEngine;

namespace Ossify
{
    [CreateAssetMenu(menuName = "Variables/Vector3 Accumulator")]
    public class Vector3Accumulator : Accumulator<Vector3, Vector3Variable, Vector3Reference> 
    {
        /// <inheritdoc />
        protected override void OnValueChanged(Vector3 value)
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
                    Output.Value = Vector3.Scale(Output.Value, value);
                    break;
                case AccumulationMode.Divide: 
                    var og = Output.Value;
                    Output.Value = new Vector3(og.x / value.x, og.y / value.y, og.z / value.z);                                       
                    break;
                case AccumulationMode.Average:
                    Output.Value = (Output.Value + value) / 2;
                    break;
                case AccumulationMode.Max:
                    Output.Value = Vector3.Max(Output.Value, value);
                    break;
                case AccumulationMode.Min:
                    Output.Value = Vector3.Min(Output.Value, value);
                    break;
                case AccumulationMode.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}