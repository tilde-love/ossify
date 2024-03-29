// <autogenerated />
using UnityEngine;
using Ossify;

#if UNITY_EDITOR
using UnityEditor;
#endif
 
namespace Ossify.Microtypes
{
    public sealed class SpritePublisher : Publisher<Sprite, Variable<Sprite>, SpriteReference> 
    { 
#if UNITY_EDITOR
        [MenuItem("GameObject/Ossify/Publishers/Sprite", false, Ossify.Consts.PublisherOrder)]
        static void CreateViaMenu(MenuCommand menuCommand) => menuCommand.CreateComponent<SpritePublisher>("Sprite Publisher");
#endif
    }
}
