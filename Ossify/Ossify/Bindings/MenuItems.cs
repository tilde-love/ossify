#if UNITY_EDITOR
using Ossify.Bindings.Specific.TMP;
using Ossify.Bindings.Specific.Unity;
using UnityEditor;
using UnityEngine;

namespace Ossify.Bindings
{
    static class MenuItems
    {
        [MenuItem("CONTEXT/TextMeshProUGUI/Bind To String Variable", priority = 1000)]
        public static void TextBindToStringVariable(MenuCommand menuCommand)
        {
            var parent = ((Component)menuCommand.context).gameObject;

            if (parent.TryGetComponent<TextToStringVariableBinder>(out var binder))
            {
                Debug.LogWarning("This object already has a TextToStringVariableBinder");
                
                return;
            }

            binder = parent.AddComponent<TextToStringVariableBinder>();
            
            Undo.RegisterCreatedObjectUndo(binder, "Bind To String Variable");
            
            EditorUtility.SetDirty(parent);
        }
        
        [MenuItem("CONTEXT/TMP_InputField/Bind To String Variable", priority = 1000)]
        public static void InputFieldBindToStringVariable(MenuCommand menuCommand)
        {
            var parent = ((Component)menuCommand.context).gameObject;

            if (parent.TryGetComponent<InputFieldToStringVariableBinder>(out var binder))
            {
                Debug.LogWarning("This object already has a InputFieldToStringVariableBinder");
                
                return;
            }

            binder = parent.AddComponent<InputFieldToStringVariableBinder>();
            
            Undo.RegisterCreatedObjectUndo(binder, "Bind To String Variable");
            
            EditorUtility.SetDirty(parent);
        }
        
        [MenuItem("CONTEXT/Button/Bind To Pulse Variable", priority = 1000)]
        public static void ButtonBindToPulseVariable(MenuCommand menuCommand)
        {
            var parent = ((Component)menuCommand.context).gameObject;

            if (parent.TryGetComponent<ButtonToPulseBinder>(out var binder))
            {
                Debug.LogWarning("This object already has a ButtonToPulseBinder");
                
                return;
            }

            binder = parent.AddComponent<ButtonToPulseBinder>();
            
            Undo.RegisterCreatedObjectUndo(binder, "Bind To Pulse Variable");
            
            EditorUtility.SetDirty(parent);
        }
        
        [MenuItem("CONTEXT/Image/Bind To Sprite Variable", priority = 1000)]
        public static void ImageBindToSpriteVariable(MenuCommand menuCommand)
        {
            var parent = ((Component)menuCommand.context).gameObject;

            if (parent.TryGetComponent<ImageToSpriteBinder>(out var binder))
            {
                Debug.LogWarning("This object already has a ImageToSpriteBinder");
                
                return;
            }

            binder = parent.AddComponent<ImageToSpriteBinder>();
            
            Undo.RegisterCreatedObjectUndo(binder, "Bind To Sprite Variable");
            
            EditorUtility.SetDirty(parent);
        }
    }
}

#endif