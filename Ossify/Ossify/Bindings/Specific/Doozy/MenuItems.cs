#if UNITY_EDITOR && OSSIFY_DOOZYUI
using Doozy.Runtime.UIManager.Components;
using UnityEditor;
using UnityEngine;

namespace Ossify.Bindings.Specific.Doozy
{
    internal static class MenuItems
    {
        [MenuItem("CONTEXT/UIButton/Bind To Pulse", priority = 1000)]
        public static void ButtonToPulseBinder(MenuCommand menuCommand)
        {
            GameObject parent = ((Component)menuCommand.context).gameObject;

            if (parent.TryGetComponent(out UIButtonToPulseBinder binder))
            {
                Debug.LogWarning("This object already has a UIButtonToPulseBinder");

                return;
            }

            binder = parent.AddComponent<UIButtonToPulseBinder>();

            Undo.RegisterCreatedObjectUndo(binder, "Bind To Pulse");

            EditorUtility.SetDirty(parent);
        }

        [MenuItem("CONTEXT/UISlider/Bind To Float Variable", priority = 1000)]
        public static void UISliderBindToFloatVariable(MenuCommand menuCommand)
        {
            GameObject parent = (menuCommand.context as UISlider)?.gameObject;

            if (parent.TryGetComponent(out UISliderToFloatVariableBinder binder))
            {
                Debug.LogWarning("This object already has a UISliderToFloatVariableBinder");

                return;
            }

            binder = parent.AddComponent<UISliderToFloatVariableBinder>();

            Undo.RegisterCreatedObjectUndo(binder, "Bind To Float Variable");

            EditorUtility.SetDirty(parent);
        }

        [MenuItem("CONTEXT/UISlider/Bind To Int Variable", priority = 1000)]
        public static void UISliderBindToIntVariable(MenuCommand menuCommand)
        {
            GameObject parent = (menuCommand.context as UISlider)?.gameObject;

            if (parent.TryGetComponent(out UISliderToIntVariableBinder binder))
            {
                Debug.LogWarning("This object already has a UISliderToIntVariableBinder");

                return;
            }

            binder = parent.AddComponent<UISliderToIntVariableBinder>();

            Undo.RegisterCreatedObjectUndo(binder, "Bind To String Variable");

            EditorUtility.SetDirty(parent);
        }
    }
}

#endif