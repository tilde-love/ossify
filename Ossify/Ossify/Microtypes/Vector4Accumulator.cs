﻿using System;
using Ossify.Variables;
using UnityEngine;

namespace Ossify
{
    [CreateAssetMenu(menuName = "Variables/Vector4 Accumulator")]
    public class Vector4Accumulator : Accumulator<Vector4, Vector4Variable, Vector4Reference> 
    {
        /// <inheritdoc />
        protected override Vector4 CalculateNewValue(Vector4 current, Vector4 value)
        {            
            switch (Mode)
            {
                case AccumulationMode.Add:
                    return current + value;
                case AccumulationMode.Subtract:
                    return current - value;
                case AccumulationMode.Multiply:
                    return Vector4.Scale(current, value);
                case AccumulationMode.Divide:
                    return new (current.x / value.x, current.y / value.y, current.z / value.z, current.w / value.w); 
                case AccumulationMode.Average:
                    return (current + value) / 2;
                case AccumulationMode.Max:
                    return Vector4.Max(current, value);
                case AccumulationMode.Min:
                    return Vector4.Min(current, value);
                case AccumulationMode.None:
                    return current;                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}