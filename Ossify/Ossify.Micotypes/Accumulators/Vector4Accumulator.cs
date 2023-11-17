using System;
using Ossify.Microtypes;
using UnityEngine;

namespace Ossify
{
    [CreateAssetMenu(menuName = "Ossify/Accumulators/Vector4", order = Ossify.Consts.AccumulatorOrder)]
    public class Vector4Accumulator : Accumulator<Vector4, Variable<Vector4>, Vector4Reference> 
    {
        /// <inheritdoc />
        protected override Vector4 CalculateNewValue(Vector4 current, Vector4 value) =>
            Mode switch
            {
                AccumulationMode.Add => current + value,
                AccumulationMode.Subtract => current - value,
                AccumulationMode.Multiply => Vector4.Scale(current, value),
                AccumulationMode.Divide => new(current.x / value.x, current.y / value.y, current.z / value.z, current.w / value.w),
                AccumulationMode.Average => (current + value) / 2,
                AccumulationMode.Max => Vector4.Max(current, value),
                AccumulationMode.Min => Vector4.Min(current, value),
                AccumulationMode.None => current,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}