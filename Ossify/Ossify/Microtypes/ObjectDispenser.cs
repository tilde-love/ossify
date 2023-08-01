using UnityEngine;

namespace Ossify
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Variables/Object Dispenser")]
    public sealed class ObjectDispenser : Dispenser<GameObject> { }
}