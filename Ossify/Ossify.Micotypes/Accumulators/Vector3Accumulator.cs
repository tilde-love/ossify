using System;
using Ossify.Microtypes;
using UnityEngine;

namespace Ossify
{
    [CreateAssetMenu(menuName = "Ossify/Accumulators/Vector3", order = Ossify.Consts.AccumulatorOrder)]
    public class Vector3Accumulator : Accumulator<Vector3, IVariable<Vector3>, Vector3Reference> 
    {
        /// <inheritdoc />
        protected override Vector3 CalculateNewValue(Vector3 current, Vector3 value)
        {            
            switch (Mode)
            {
                case AccumulationMode.Add:
                    return current + value;
                case AccumulationMode.Subtract:
                    return current - value;
                case AccumulationMode.Multiply:
                    return Vector3.Scale(current, value);
                case AccumulationMode.Divide:
                    return new (current.x / value.x, current.y / value.y, current.z / value.z); 
                case AccumulationMode.Average:
                    return (current + value) / 2;
                case AccumulationMode.Max:
                    return Vector3.Max(current, value);
                case AccumulationMode.Min:
                    return Vector3.Min(current, value);
                case AccumulationMode.None:
                    return current;                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}