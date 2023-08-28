using UnityEngine;

namespace Ossify.Microtypes
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Ossify/Object Collection")]
    public class ObjectCollection : ScriptableCollection<GameObject> { }
}