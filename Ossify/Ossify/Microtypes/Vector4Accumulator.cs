using System;
using Ossify.Variables;
using UnityEngine;

namespace Ossify
{
    [CreateAssetMenu(menuName = "Variables/Vector4 Accumulator")]
    public class Vector4Accumulator : Accumulator<Vector4, Vector4Variable, Vector4Reference> 
    {
        /// <inheritdoc />
        protected override void OnValueChanged(Vector4 value)
        {           
            var og = Output.Value;            
 
            switch (Mode)
            {
                case AccumulationMode.Add:
                    Output.Value = og + value; 
                    break;
                case AccumulationMode.Subtract:
                    Output.Value = og - value;
                    break;
                case AccumulationMode.Multiply:
                    Output.Value = Vector4.Scale(og, value);
                    break;
                case AccumulationMode.Divide:                        
                    Output.Value = new (og.x / value.x, og.y / value.y, og.z / value.z, og.w / value.w);                
                    break;
                case AccumulationMode.Average:
                    Output.Value = (og + value) / 2;
                    break;
                case AccumulationMode.Max:
                    Output.Value = Vector4.Max(og, value);
                    break;
                case AccumulationMode.Min:
                    Output.Value = Vector4.Min(og, value);
                    break;
                case AccumulationMode.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}