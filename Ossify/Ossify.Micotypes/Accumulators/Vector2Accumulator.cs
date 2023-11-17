using System;
using Ossify.Microtypes;
using UnityEngine;

namespace Ossify
{
    [CreateAssetMenu(menuName = "Ossify/Accumulators/Vector2", order = Ossify.Consts.AccumulatorOrder)]
    public class Vector2Accumulator : Accumulator<Vector2, Variable<Vector2>, Vector2Reference> 
    {
        /// <inheritdoc />
        protected override Vector2 CalculateNewValue(Vector2 current, Vector2 value) =>
            Mode switch
            {
                AccumulationMode.Add => current + value,
                AccumulationMode.Subtract => current - value,
                AccumulationMode.Multiply => Vector2.Scale(current, value),
                AccumulationMode.Divide => new(current.x / value.x, current.y / value.y),
                AccumulationMode.Average => (current + value) / 2,
                AccumulationMode.Max => Vector2.Max(current, value),
                AccumulationMode.Min => Vector2.Min(current, value),
                AccumulationMode.None => current,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}