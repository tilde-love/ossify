using System;
using Ossify.Microtypes;
using UnityEngine;

namespace Ossify
{
    [CreateAssetMenu(menuName = "Ossify/Accumulators/Vector3", order = Ossify.Consts.AccumulatorOrder)]
    public class Vector3Accumulator : Accumulator<Vector3, Variable<Vector3>, Vector3Reference> 
    {
        /// <inheritdoc />
        protected override Vector3 CalculateNewValue(Vector3 current, Vector3 value) =>
            Mode switch
            {
                AccumulationMode.Add => current + value,
                AccumulationMode.Subtract => current - value,
                AccumulationMode.Multiply => Vector3.Scale(current, value),
                AccumulationMode.Divide => new(current.x / value.x, current.y / value.y, current.z / value.z),
                AccumulationMode.Average => (current + value) / 2,
                AccumulationMode.Max => Vector3.Max(current, value),
                AccumulationMode.Min => Vector3.Min(current, value),
                AccumulationMode.None => current,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}