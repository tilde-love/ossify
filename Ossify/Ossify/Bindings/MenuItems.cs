#if UNITY_EDITOR
using Ossify.Bindings.Specific.TMP;
using Ossify.Bindings.Specific.Unity;
using UnityEditor;
using UnityEngine;

namespace Ossify.Bindings
{
    internal static class MenuItems
    {
        [MenuItem("CONTEXT/Button/Bind To Pulse Variable", priority = 1000)]
        public static void ButtonBindToPulseVariable(MenuCommand menuCommand)
        {
            GameObject parent = ((Component)menuCommand.context).gameObject;

            if (parent.TryGetComponent<ButtonToPulseBinder>(out ButtonToPulseBinder binder))
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
            GameObject parent = ((Component)menuCommand.context).gameObject;

            if (parent.TryGetComponent<ImageToSpriteBinder>(out ImageToSpriteBinder binder))
            {
                Debug.LogWarning("This object already has a ImageToSpriteBinder");

                return;
            }

            binder = parent.AddComponent<ImageToSpriteBinder>();

            Undo.RegisterCreatedObjectUndo(binder, "Bind To Sprite Variable");

            EditorUtility.SetDirty(parent);
        }

        [MenuItem("CONTEXT/TMP_InputField/Bind To String Variable", priority = 1000)]
        public static void InputFieldBindToStringVariable(MenuCommand menuCommand)
        {
            GameObject parent = ((Component)menuCommand.context).gameObject;

            if (parent.TryGetComponent<InputFieldToStringVariableBinder>(out InputFieldToStringVariableBinder binder))
            {
                Debug.LogWarning("This object already has a InputFieldToStringVariableBinder");

                return;
            }

            binder = parent.AddComponent<InputFieldToStringVariableBinder>();

            Undo.RegisterCreatedObjectUndo(binder, "Bind To String Variable");

            EditorUtility.SetDirty(parent);
        }

        [MenuItem("CONTEXT/TextMeshProUGUI/Bind To String Variable", priority = 1000)]
        public static void TextBindToStringVariable(MenuCommand menuCommand)
        {
            GameObject parent = ((Component)menuCommand.context).gameObject;

            if (parent.TryGetComponent<TextToStringVariableBinder>(out TextToStringVariableBinder binder))
            {
                Debug.LogWarning("This object already has a TextToStringVariableBinder");

                return;
            }

            binder = parent.AddComponent<TextToStringVariableBinder>();

            Undo.RegisterCreatedObjectUndo(binder, "Bind To String Variable");

            EditorUtility.SetDirty(parent);
        }
        
        [MenuItem("CONTEXT/TextMeshProUGUI/Bind Formatted To Float Variable", priority = 1000)]
        public static void TextBindToFloatVariable(MenuCommand menuCommand)
        {
            GameObject parent = ((Component)menuCommand.context).gameObject;

            if (parent.TryGetComponent<TextToFloatVariableBinder>(out TextToFloatVariableBinder binder))
            {
                Debug.LogWarning("This object already has a TextToFloatVariableBinder");

                return;
            }

            binder = parent.AddComponent<TextToFloatVariableBinder>();

            Undo.RegisterCreatedObjectUndo(binder, "Bind Formatted To Float Variable");

            EditorUtility.SetDirty(parent);
        }
        
        [MenuItem("CONTEXT/Image/Bind Fill To Float Variable", priority = 1000)]
        public static void FilledImageBindToFloatVariable(MenuCommand menuCommand)
        {
            GameObject parent = ((Component)menuCommand.context).gameObject;

            if (parent.TryGetComponent<FilledImageToFloatBinder>(out FilledImageToFloatBinder binder))
            {
                Debug.LogWarning("This object already has a FilledImageToFloatBinder");

                return;
            }

            binder = parent.AddComponent<FilledImageToFloatBinder>();

            Undo.RegisterCreatedObjectUndo(binder, "Bind Fill To Float Variable");

            EditorUtility.SetDirty(parent);
        }
    }
}

#endif