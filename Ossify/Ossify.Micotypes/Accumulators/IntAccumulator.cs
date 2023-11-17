using System;
using Ossify.Microtypes;
using UnityEngine;

namespace Ossify.Microtypes
{
    [CreateAssetMenu(menuName = "Ossify/Accumulators/Int", order = Ossify.Consts.AccumulatorOrder)]
    public class IntAccumulator : Accumulator<int, Variable<int>, IntReference>
    {
        /// <inheritdoc />
        protected override int CalculateNewValue(int current, int value) =>
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