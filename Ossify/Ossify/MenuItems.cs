#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Ossify
{
    public static partial class MenuItems
    {
        public static TComponent CreateComponent<TComponent>(this MenuCommand menuCommand, string name) where TComponent : Component
        {
            // Create a custom game object
            GameObject go = new GameObject(name);

            var component = go.AddComponent<TComponent>();

            // Ensure it gets reparented if this was a context click (otherwise does nothing)
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);

            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);

            Selection.activeObject = go;

            return component; 
        }
    }
}
#endif