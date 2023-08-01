using UnityEngine;

namespace Ossify.Variables
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Variables/Transform")]
    public sealed class TransformVariable : Variable<Transform> { }
}