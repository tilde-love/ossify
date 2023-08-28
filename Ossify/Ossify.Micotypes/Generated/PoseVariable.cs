using UnityEngine;

namespace Ossify.Microtypes
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Ossify/Pose")]
    public sealed class PoseVariable : Variable<Pose> { }
}