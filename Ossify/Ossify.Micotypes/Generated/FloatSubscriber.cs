// <autogenerated />
using UnityEngine;
using Ossify;

#if UNITY_EDITOR
using UnityEditor;
#endif
 
namespace Ossify.Microtypes
{
    public sealed class FloatSubscriber : Subscriber<float, IVariable<float>> 
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/Ossify/Subscribers/Float", false, Ossify.Consts.SubscriberOrder)]
        static void CreateViaMenu(MenuCommand menuCommand) => menuCommand.CreateComponent<FloatSubscriber>("Float Subscriber");
#endif
    }
}
