using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ossify
{
    [CreateAssetMenu(order = Consts.ContainerMenuItems, menuName = "Variables/Bin")]
    public sealed class Bin : ScriptableObject
    {
        [InfoBox(message: "\n"
                          + "Bins are simple containers.\n"
                          + "It can hold any number of ScriptableObjects.\n"
                          + "If you drop items below they will be moved into the bin.\n"
                          + "If they are already in the bin they will be saved int the folder where the bin resides.\n"
                          + "If you drop a bin on another bin it will be merged into the target bin.\n"
                          + "\n")]
        [ShowInInspector, AssetsOnly, LabelText("DROP ITEMS HERE"), ] private List<ScriptableObject> items = new ();
        
#if UNITY_EDITOR
        private async void OnValidate()
        {
            if (items.Count == 0) return;
            
            string assetPath = AssetDatabase.GetAssetPath(this);
            string assetFolder = assetPath[..^Path.GetFileName(assetPath).Length];
            
            Debug.Log("Asset Folder: " + assetFolder);
            
            foreach (var item in items)
            {
                string path = AssetDatabase.GetAssetPath(item);
                
                //Debug.Log(path);
                
                var typeString = AssetDatabase.GetMainAssetTypeAtPath(path).ToString();
                
                //Debug.Log(typeString);

                var main = AssetDatabase.LoadMainAssetAtPath(path);

                if (item is Bin && item != this)
                {
                    foreach (var sub in AssetDatabase.LoadAllAssetsAtPath(path))
                    {
                        if (sub == item) continue;
                        
                        AssetDatabase.RemoveObjectFromAsset(sub);
                        
                        AssetDatabase.AddObjectToAsset(sub, this);
                        
                        EditorUtility.SetDirty(sub);
                    }
                    
                    EditorUtility.SetDirty(item);

                    AssetDatabase.SaveAssets();
                    
                    await UniTask.NextFrame();
                    
                    AssetDatabase.DeleteAsset(path);
                }
                else if (main == this)
                {
                    string newAsset = Path.Combine(assetFolder, item.name) + ".asset"; 
                    
                    Debug.Log(newAsset);
                    
                    AssetDatabase.RemoveObjectFromAsset(item);
                    
                    AssetDatabase.CreateAsset(item, newAsset);

                    EditorUtility.SetDirty(item);
                }
                else if (main is Bin)
                {
                    Debug.Log("Bin in Bin");

                    AssetDatabase.RemoveObjectFromAsset(item);

                    AssetDatabase.AddObjectToAsset(item, this);
                    
                    EditorUtility.SetDirty(this);
                    
                    EditorUtility.SetDirty(main);
                    
                    EditorUtility.SetDirty(item);
                }
                else if (main == item)
                {
                    if (AssetDatabase.LoadAllAssetsAtPath(path).Length > 1)
                    {
                        continue;
                    }

                    AssetDatabase.RemoveObjectFromAsset(item);
                    
                    AssetDatabase.AddObjectToAsset(item, this);

                    AssetDatabase.SaveAssets();
                    
                    await UniTask.NextFrame();
                    
                    if (AssetDatabase.DeleteAsset(path) == false)
                    {
                        Debug.LogError("Could not delete asset: " + path);
                    }

                    EditorUtility.SetDirty(item);
                }
                else
                {
                    continue;
                }

                EditorUtility.SetDirty(this);
            }
            
            items.Clear();
            
            await UniTask.NextFrame();
            
            AssetDatabase.SetMainObject(this, assetPath);

            AssetDatabase.SaveAssets();
            
            await UniTask.NextFrame();
            
            AssetDatabase.ImportAsset(assetPath);
        }

        /*
        /// <summary>
        /// Finds all missing references to objects in the currently loaded scene.
        /// </summary>
        [MenuItem(MENU_ROOT + "Search in scene", false, 50)]
        public static void FindMissingReferencesInCurrentScene()
        {
            var sceneObjects = GetSceneObjects();
            FindMissingReferences(EditorSceneManager.GetActiveScene().path, sceneObjects);
        }

        /// <summary>
        /// Finds all missing references to objects in all enabled scenes in the project.
        /// This works by loading the scenes one by one and checking for missing object references.
        /// </summary>
        [MenuItem(MENU_ROOT + "Search in all scenes", false, 51)]
        public static void FindMissingReferencesInAllScenes()
        {
            foreach (var scene in EditorBuildSettings.scenes.Where(s => s.enabled))
            {
                EditorSceneManager.OpenScene(scene.path);
                FindMissingReferencesInCurrentScene();
            }
        }

        /// <summary>
        /// Finds all missing references to objects in assets (objects from the project window).
        /// </summary>
        [MenuItem(MENU_ROOT + "Search in assets", false, 52)]
        public static void FindMissingReferencesInAssets()
        {
            var allAssets = AssetDatabase.GetAllAssetPaths().Where(path => path.StartsWith("Assets/")).ToArray();
            var objs = allAssets.Select(a => AssetDatabase.LoadAssetAtPath(a, typeof(GameObject)) as GameObject).Where(a => a != null).ToArray();

            FindMissingReferences("Project", objs);
        }
        
        /// <summary>
        /// Generate a report of all missing references to objects in any scene that is in the build.
        /// </summary>
        [MenuItem(MENU_ROOT + "Generate Missing Asset Report", false, 100)]
        public static void MissingAssetsEverywhere()
        {
            StringBuilder sb = new StringBuilder();
        
            sb.AppendLine(DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
        
            var allAssets = AssetDatabase.GetAllAssetPaths();
            var objs = allAssets
                .Select(a => AssetDatabase.LoadAssetAtPath(a, typeof(GameObject)) as GameObject)
                .Where(a => a != null)
                .ToArray();
         
            FindMissingReferences("Assets", objs, sb);
        
            foreach (var scene in EditorBuildSettings.scenes.Where(s => s.enabled))
            {
                EditorSceneManager.OpenScene(scene.path);
                FindMissingReferences( scene.path, GetSceneObjects(), sb);
            }

            if (Directory.Exists("Asset Reports") == false) Directory.CreateDirectory("Asset Reports");
        
            File.WriteAllText($"Asset Reports/Missing - {DateTime.UtcNow:yyyy-MM-dd-HHmm}.txt", sb.ToString());
        }

        private static void FindMissingReferences(string context, GameObject[] gameObjects, StringBuilder sb = null)
        {
            if (gameObjects == null)
            {
                return;
            }

            foreach (var go in gameObjects)
            {
                var components = go.GetComponents<Component>();

                foreach (var component in components)
                {
                    // Missing components will be null, we can't find their type, etc.
                    if (!component)
                    {
                        if (sb == null) Debug.LogErrorFormat(go, $"Missing Component {0} in GameObject: {1}",component?.GetType().FullName ?? "Unknown", GetFullPath(go));
                        else sb.AppendLine($"\"{context}\", \"{GetFullPath(go)}\", $MISSING, \"{ component?.GetType().FullName ?? "Unknown"}\"");

                        continue;
                    }

                    SerializedObject so = new SerializedObject(component);
                    var sp = so.GetIterator();

                    var objRefValueMethod = typeof(SerializedProperty).GetProperty("objectReferenceStringValue",
                        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                    // Iterate over the components' properties.
                    do
                    {
                        if (sp.propertyType != SerializedPropertyType.ObjectReference)
                        {
                            continue;
                        }

                        string objectReferenceStringValue = string.Empty;

                        if (objRefValueMethod != null)
                        {
                            objectReferenceStringValue = (string)objRefValueMethod.GetGetMethod(true).Invoke(sp, new object[] { });
                        }

                        if (sp.objectReferenceValue == null
                            && (sp.objectReferenceInstanceIDValue != 0 || objectReferenceStringValue.StartsWith("Missing")))
                        {
                            ShowError(context, go, component.GetType().Name, ObjectNames.NicifyVariableName(sp.name), sb);
                        }
                    }
                    while (sp.NextVisible(true));
                }
            }
        }

        private static GameObject[] GetSceneObjects()
        {
            // Use this method since GameObject.FindObjectsOfType will not return disabled objects.
            return Resources.FindObjectsOfTypeAll<GameObject>()
                .Where(go => string.IsNullOrEmpty(AssetDatabase.GetAssetPath(go))
                       && go.hideFlags == HideFlags.None).ToArray();
        }

        private static void ShowError(string context, GameObject go, string componentName, string propertyName, StringBuilder sb)
        {
            if (sb == null) Debug.LogError($"Missing Ref in: [{context}]{GetFullPath(go)}. Component: {componentName}, Property: {propertyName}", go);
            else sb.AppendLine($"\"{context}\", \"{GetFullPath(go)}\", \"{componentName}\", \"{propertyName}\"");
        }

        private static string GetFullPath(GameObject go) =>
            go.transform.parent == null
                ? go.name
                : $"{GetFullPath(go.transform.parent.gameObject)}/{go.name}";
                
            */
#endif 
    }
}