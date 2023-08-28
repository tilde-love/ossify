using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Serialization;
using FilePathAttribute = UnityEditor.FilePathAttribute;

namespace Ossify.Editor
{    
    //[FilePath("Assets/OssifyEditorResources.asset", FilePathAttribute.Location.ProjectFolder)]
    public class OssifyEditorResources : ScriptableSingleton<OssifyEditorResources>
    {
        [Button("Save")]
        public void Save() => Save(true);

        [SerializeField] private Texture2D variableIcon;
        [SerializeField] private Texture2D referenceIcon;
        [SerializeField] private Texture2D publisherIcon; 
        [SerializeField] private Texture2D subscriberIcon;
        [SerializeField] private Texture2D dispenserIcon;
        [SerializeField] private Texture2D collectionIcon;
         
        public static Texture2D VariableIcon => instance.variableIcon;
        public static Texture2D ReferenceIcon => instance.referenceIcon;
        public static Texture2D PublisherIcon => instance.publisherIcon;
        public static Texture2D SubscriberIcon => instance.subscriberIcon;         
        public static Texture2D DispenserIcon => instance.dispenserIcon;
        public static Texture2D CollectionIcon => instance.collectionIcon; 
        
        // void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
        
        // [MenuItem("Ossify/Open Editor Resources")]
        // public static void OpenSpecificObjectInspector()
        // {
        //     // Select the object
        //     Selection.activeObject = instance;
        //
        //     instance.name = nameof(OssifyEditorResources);
        //
        //     // Request the Inspector to focus on the selected object
        //     EditorApplication.ExecuteMenuItem("Window/General/Inspector");
        // }
        //         
        // [InitializeOnLoadMethod]
        // private static void EnsureInstanceExists()
        // {
        //     // Access the instance to ensure it is created
        //     var instance = OssifyEditorResources.instance;
        //
        //     instance.name = nameof(OssifyEditorResources);
        //
        //     instance.Save(true);
        // }
    }
}