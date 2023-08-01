using UnityEngine;

namespace Ossify.Collections
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Variables/Object Collection")]
    public class ObjectCollection : ScriptableCollection<GameObject> { }
}