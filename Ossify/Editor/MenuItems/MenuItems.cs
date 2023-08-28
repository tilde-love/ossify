using Ossify;
using UnityEditor;
using UnityEngine;

namespace Ossify.Editor
{
    public static partial class MenuItems
    {
        [MenuItem("GameObject/Ossify/Async Dispensable", false, 10)]
        static void CreateAsyncDispensable(MenuCommand menuCommand) => menuCommand.CreateComponent<AsyncDispensable>("Async Dispensable");

        [MenuItem("GameObject/Ossify/Ballot Events", false, 10)]
        static void CreateBallotEvents(MenuCommand menuCommand) => menuCommand.CreateComponent<BallotEvents>("Ballot Events");

        [MenuItem("GameObject/Ossify/Ballot Listener", false, 10)]
        static void CreateBallotListener(MenuCommand menuCommand) => menuCommand.CreateComponent<BallotListener>("Ballot Listener");

        [MenuItem("GameObject/Ossify/Dispensable", false, 10)]
        static void CreateDispensable(MenuCommand menuCommand) => menuCommand.CreateComponent<Dispensable>("Dispensable");

        [MenuItem("GameObject/Ossify/Heartbeat", false, 10)]
        static void CreateHeartbeat(MenuCommand menuCommand) => menuCommand.CreateComponent<Heartbeat>("Heartbeat");

        [MenuItem("GameObject/Ossify/Impulse Listener", false, 10)]
        static void CreateImpulseListener(MenuCommand menuCommand) => menuCommand.CreateComponent<ImpulseListener>("Impulse Listener");

        [MenuItem("GameObject/Ossify/Impulser", false, 10)]
        static void CreateImpulser(MenuCommand menuCommand) => menuCommand.CreateComponent<Impulser>("Impulser");

        [MenuItem("GameObject/Ossify/Voter", false, 10)]
        static void CreateVoter(MenuCommand menuCommand) => menuCommand.CreateComponent<Voter>("Voter");

    }
}