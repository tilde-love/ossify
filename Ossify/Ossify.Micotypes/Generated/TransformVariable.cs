using UnityEngine;

namespace Ossify.Microtypes
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Ossify/Transform")]
    public sealed class TransformVariable : Variable<Transform> { }
}