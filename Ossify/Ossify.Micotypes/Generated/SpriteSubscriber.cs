// <autogenerated />
using UnityEngine;
using Ossify;

#if UNITY_EDITOR
using UnityEditor;
#endif
 
namespace Ossify.Microtypes
{
    public sealed class SpriteSubscriber : Subscriber<Sprite, Variable<Sprite>> 
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/Ossify/Subscribers/Sprite", false, Ossify.Consts.SubscriberOrder)]
        static void CreateViaMenu(MenuCommand menuCommand) => menuCommand.CreateComponent<SpriteSubscriber>("Sprite Subscriber");
#endif
    }
}
