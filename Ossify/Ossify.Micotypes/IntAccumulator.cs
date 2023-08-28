using System;
using Ossify.Microtypes;
using UnityEngine;

namespace Ossify.Microtypes
{
    [CreateAssetMenu(menuName = "Ossify/Int Accumulator")]
    public class IntAccumulator : Accumulator<int, IntVariable, IntReference> 
    {
        /// <inheritdoc />
        protected override int CalculateNewValue(int current, int value)
        {            
            switch (Mode)
            {
                case AccumulationMode.Add:
                    return current + value;
                case AccumulationMode.Subtract:
                    return current - value;
                case AccumulationMode.Multiply:
                    return current * value;
                case AccumulationMode.Divide:
                    return current / value;
                case AccumulationMode.Average:
                    return (current + value) / 2;
                case AccumulationMode.Max:
                    return Mathf.Max(current, value);
                case AccumulationMode.Min:
                    return Mathf.Min(current, value);
                case AccumulationMode.None:
                    return current;                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}