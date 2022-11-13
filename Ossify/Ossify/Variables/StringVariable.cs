using UnityEngine;

namespace Ossify.Variables
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Variables/String")] public sealed class StringVariable : Variable<string> { }
}