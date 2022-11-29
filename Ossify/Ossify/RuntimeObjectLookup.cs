using System.Collections.Generic;
using System.Linq;

namespace Ossify
{
    public abstract class RuntimeObjectLookup<TValue> : RuntimeLookup<string, TValue> where TValue : UnityEngine.Object
    {
        /// <inheritdoc />
        protected override Dictionary<string, TValue> BuildLookup() => this.ToDictionary<TValue, string>(obj => obj.name);

        /// <inheritdoc />
        protected override string GetKey(TValue value) => value.name;
    }
}