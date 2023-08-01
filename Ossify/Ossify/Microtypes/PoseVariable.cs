using UnityEngine;

namespace Ossify.Variables
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Variables/Pose")]
    public sealed class PoseVariable : Variable<Pose> { }
}