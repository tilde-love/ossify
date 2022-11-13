#if UNITY_EDITOR && OSSIFY_DOOZYUI
using Doozy.Runtime.UIManager.Components;
using UnityEditor;
using UnityEngine;

namespace Ossify.Bindings.Specific.Doozy
{
    static class MenuItems
    {
        [MenuItem("CONTEXT/UISlider/Bind To Int Variable", priority = 1000)]
        public static void UISliderBindToIntVariable(MenuCommand menuCommand)
        {
            var parent = (menuCommand.context as UISlider)?.gameObject;

            if (parent.TryGetComponent<UISliderToIntVariableBinder>(out var binder))
            {
                Debug.LogWarning("This object already has a UISliderToIntVariableBinder");
                
                return;
            }

            binder = parent.AddComponent<UISliderToIntVariableBinder>();
            
            Undo.RegisterCreatedObjectUndo(binder, "Bind To String Variable");
            
            EditorUtility.SetDirty(parent);
        }
        
        [MenuItem("CONTEXT/UISlider/Bind To Float Variable", priority = 1000)]
        public static void UISliderBindToFloatVariable(MenuCommand menuCommand)
        {
            var parent = (menuCommand.context as UISlider)?.gameObject;

            if (parent.TryGetComponent<UISliderToFloatVariableBinder>(out var binder))
            {
                Debug.LogWarning("This object already has a UISliderToFloatVariableBinder");
                
                return;
            }

            binder = parent.AddComponent<UISliderToFloatVariableBinder>();
            
            Undo.RegisterCreatedObjectUndo(binder, "Bind To Float Variable");
            
            EditorUtility.SetDirty(parent);
        }
    }
}

#endif