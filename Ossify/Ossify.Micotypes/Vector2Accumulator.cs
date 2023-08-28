using System;
using Ossify.Microtypes;
using UnityEngine;

namespace Ossify
{
    [CreateAssetMenu(menuName = "Variables/Vector2 Accumulator")]
    public class Vector2Accumulator : Accumulator<Vector2, Vector2Variable, Vector2Reference> 
    {
        /// <inheritdoc />
        protected override Vector2 CalculateNewValue(Vector2 current, Vector2 value)
        {            
            switch (Mode)
            {
                case AccumulationMode.Add:
                    return current + value;
                case AccumulationMode.Subtract:
                    return current - value;
                case AccumulationMode.Multiply:
                    return Vector2.Scale(current, value);
                case AccumulationMode.Divide:
                    return new (current.x / value.x, current.y / value.y); 
                case AccumulationMode.Average:
                    return (current + value) / 2;
                case AccumulationMode.Max:
                    return Vector2.Max(current, value);
                case AccumulationMode.Min:
                    return Vector2.Min(current, value);
                case AccumulationMode.None:
                    return current;                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}