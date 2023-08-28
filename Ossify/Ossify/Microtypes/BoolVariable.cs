using UnityEngine;

namespace Ossify.Microtypes
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Ossify/Bool")]
    public sealed class BoolVariable : Variable<bool>
    {
        public void Toggle()
        {
            Value = !Value;
        }
    }
}