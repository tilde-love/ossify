// <autogenerated />
using UnityEngine;
using Ossify;

#if UNITY_EDITOR
using UnityEditor;
#endif
 
namespace Ossify.Microtypes
{
    public sealed class BoolPublisher : Publisher<bool, IVariable<bool>, BoolReference> 
    { 
#if UNITY_EDITOR
        [MenuItem("GameObject/Ossify/Publishers/Bool", false, Ossify.Consts.PublisherOrder)]
        static void CreateViaMenu(MenuCommand menuCommand) => menuCommand.CreateComponent<BoolPublisher>("Bool Publisher");
#endif
    }
}
