using System;

namespace Ossify.Variables
{
    public interface IVariable<T> : IVariable
    {
        T Value { get; set; }

        event Action<T> ValueChanged;
    }

    public interface IVariable
    {
        object Value { get; set; }
        Type ValueType { get; }

        event Action<object> ValueChanged;
    }
}