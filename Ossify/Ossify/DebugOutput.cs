using UnityEngine;

namespace Ossify
{
    public abstract class DebugOutput : ScriptableObject
    {
        public abstract void Log(string message);
    }
}