using UnityEngine;

namespace Ossify.Microtypes
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Ossify/Object Dispenser")]
    public sealed class ObjectDispenser : Dispenser<GameObject> { }
}