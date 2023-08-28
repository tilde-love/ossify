using UnityEngine;

namespace Ossify.Microtypes
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Variables/Byte Array")]
    public sealed class ByteArrayVariable : Variable<byte[]> { }
}