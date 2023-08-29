using UnityEngine;

namespace Ossify.Microtypes
{
    [CreateAssetMenu(order = Ossify.Consts.VariableOrder, menuName = "Ossify/Bool")]
    public sealed class BoolVariable : Variable<bool>
    {
        public void Toggle()
        {
            Value = !Value;
        }
    }
}