// <autogenerated />
using UnityEngine;
using Ossify;

#if UNITY_EDITOR
using UnityEditor;
#endif
 
namespace Ossify.Microtypes
{
    public sealed class TransformSubscriber : Subscriber<Transform, Variable<Transform>> 
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/Ossify/Subscribers/Transform", false, Ossify.Consts.SubscriberOrder)]
        static void CreateViaMenu(MenuCommand menuCommand) => menuCommand.CreateComponent<TransformSubscriber>("Transform Subscriber");
#endif
    }
}
