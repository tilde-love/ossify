using System;

namespace Ossify.Variables
{
    public interface IVariable<T>
    {
        event Action<T> ValueChanged;

        T Value { get; set; } 
    }
}