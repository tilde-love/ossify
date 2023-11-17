using System;
using Ossify.Microtypes;
using UnityEngine;

namespace Ossify.Microtypes
{
    [CreateAssetMenu(menuName = "Ossify/Accumulators/Float ", order = Ossify.Consts.AccumulatorOrder)]
    public class FloatAccumulator : Accumulator<float, Variable<float>, FloatReference> 
    {
        /// <inheritdoc />
        protected override float CalculateNewValue(float current, float value) =>
            Mode switch
            {
                AccumulationMode.Add => current + value,
                AccumulationMode.Subtract => current - value,
                AccumulationMode.Multiply => current * value,
                AccumulationMode.Divide => current / value,
                AccumulationMode.Average => (current + value) / 2,
                AccumulationMode.Max => Mathf.Max(current, value),
                AccumulationMode.Min => Mathf.Min(current, value),
                AccumulationMode.None => current,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}