// <autogenerated />
using UnityEngine;
using Ossify;

#if UNITY_EDITOR
using UnityEditor;
#endif
 
namespace Ossify.Microtypes
{
    public sealed class GameObjectPublisher : Publisher<GameObject, Variable<GameObject>, GameObjectReference> 
    { 
#if UNITY_EDITOR
        [MenuItem("GameObject/Ossify/Publishers/GameObject", false, Ossify.Consts.PublisherOrder)]
        static void CreateViaMenu(MenuCommand menuCommand) => menuCommand.CreateComponent<GameObjectPublisher>("GameObject Publisher");
#endif
    }
}
