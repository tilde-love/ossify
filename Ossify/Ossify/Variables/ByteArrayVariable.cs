using UnityEngine;

namespace Ossify.Variables
{
    [CreateAssetMenu(order = Consts.VariableMenuItems, menuName = "Variables/Byte Array")]  
    public sealed class ByteArrayVariable : Variable<byte[]> 
    { 
    }
}