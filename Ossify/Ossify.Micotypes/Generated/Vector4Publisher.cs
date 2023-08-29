// <autogenerated />
using UnityEngine;
using Ossify;

#if UNITY_EDITOR
using UnityEditor;
#endif
 
namespace Ossify.Microtypes
{
    public sealed class Vector4Publisher : Publisher<Vector4, IVariable<Vector4>, Vector4Reference> 
    { 
#if UNITY_EDITOR
        [MenuItem("GameObject/Ossify/Publishers/Vector4", false, Ossify.Consts.PublisherOrder)]
        static void CreateViaMenu(MenuCommand menuCommand) => menuCommand.CreateComponent<Vector4Publisher>("Vector4 Publisher");
#endif
    }
}
