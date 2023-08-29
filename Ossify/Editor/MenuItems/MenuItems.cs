using Ossify;
using UnityEditor;
using UnityEngine;

namespace Ossify.Editor
{
    public static partial class MenuItems
    {
        [MenuItem("GameObject/Ossify/Async Dispensable", false, Ossify.Consts.DispenserOrder)]
        static void CreateAsyncDispensable(MenuCommand menuCommand) => menuCommand.CreateComponent<AsyncDispensable>("Async Dispensable");

        [MenuItem("GameObject/Ossify/Ballot Events", false, Ossify.Consts.BallotOrder)]
        static void CreateBallotEvents(MenuCommand menuCommand) => menuCommand.CreateComponent<BallotEvents>("Ballot Events");

        [MenuItem("GameObject/Ossify/Ballot Listener", false, Ossify.Consts.BallotOrder)]
        static void CreateBallotListener(MenuCommand menuCommand) => menuCommand.CreateComponent<BallotListener>("Ballot Listener");

        [MenuItem("GameObject/Ossify/Dispensable", false, Ossify.Consts.DispenserOrder)]
        static void CreateDispensable(MenuCommand menuCommand) => menuCommand.CreateComponent<Dispensable>("Dispensable");

        [MenuItem("GameObject/Ossify/Heartbeat", false, Ossify.Consts.ToolingOrder)]
        static void CreateHeartbeat(MenuCommand menuCommand) => menuCommand.CreateComponent<Heartbeat>("Heartbeat");

        [MenuItem("GameObject/Ossify/Impulse Listener", false, Ossify.Consts.ImpulseOrder)]
        static void CreateImpulseListener(MenuCommand menuCommand) => menuCommand.CreateComponent<ImpulseListener>("Impulse Listener");

        [MenuItem("GameObject/Ossify/Impulser", false, Ossify.Consts.ImpulseOrder)]
        static void CreateImpulser(MenuCommand menuCommand) => menuCommand.CreateComponent<Impulser>("Impulser");

        [MenuItem("GameObject/Ossify/Voter", false, Ossify.Consts.BallotOrder)]
        static void CreateVoter(MenuCommand menuCommand) => menuCommand.CreateComponent<Voter>("Voter");

    }
}