// <autogenerated />
using UnityEngine;
using Ossify;

#if UNITY_EDITOR
using UnityEditor;
#endif
 
namespace Ossify.Microtypes
{
    public sealed class PoseSubscriber : Subscriber<Pose, IVariable<Pose>> 
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/Ossify/Subscribers/Pose", false, Ossify.Consts.SubscriberOrder)]
        static void CreateViaMenu(MenuCommand menuCommand) => menuCommand.CreateComponent<PoseSubscriber>("Pose Subscriber");
#endif
    }
}
