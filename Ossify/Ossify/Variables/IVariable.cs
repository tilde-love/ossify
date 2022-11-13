using System;

namespace Ossify.Variables
{
    public interface IVariable<T>
    {
        T Value { get; set; }
        event Action<T> ValueChanged;
    }
}