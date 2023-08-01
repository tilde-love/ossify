using UnityEngine;

namespace Ossify.Variables
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Variables/Bool")]
    public sealed class BoolVariable : Variable<bool>
    {
        public void Toggle() => Value = !Value;
    }
}