using UnityEngine;

namespace Ossify.Variables
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Variables/Sprite")]
    public sealed class SpriteVariable : Variable<Sprite> { }
}