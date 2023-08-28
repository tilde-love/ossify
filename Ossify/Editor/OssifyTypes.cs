using System;
using System.Collections.Generic;
using System.IO;
using Ossify.Editor.Editor.Generator;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Ossify.Editor
{
    [CreateAssetMenu(menuName = "Ossify/Types Definition")]
    public class OssifyTypes : ScriptableObject
    {
        [SerializeField] private string outputNamespace = "";

        [SerializeField] private string menuName = "Ossify";

        [SerializeField, TableList] private List<OssifyTypeDefinition> typeDefinitions = new();

        [Button]
        private void Generate()
        {
            // get the path to this assset file
            string path = AssetDatabase.GetAssetPath(this);
            // get the path to the folder that contains this asset file
            string folder = Path.GetDirectoryName(path);

            // create a gen folder
            string genFolder = Path.Combine(folder, "Generated");

            // ensure the folder exists 
            Directory.CreateDirectory(genFolder);

            bool changed = false;             

            foreach (OssifyTypeDefinition typeDefinition in typeDefinitions)
            {
                if (typeDefinition.IncludeVariable == true) 
                {
                    changed |= WriteTypeTemplate(genFolder, typeDefinition, new VariableTemplate());
                }    

                if (typeDefinition.IncludeReference == true) 
                {
                    changed |= WriteTypeTemplate(genFolder, typeDefinition, new VariableReferenceTemplate());
                }

                if (typeDefinition.IncludePubSub == true) 
                {
                    changed |= WriteTypeTemplate(genFolder, typeDefinition, new VariableSubscriberTemplate());
                    changed |= WriteTypeTemplate(genFolder, typeDefinition, new VariablePublisherTemplate());
                }

                if (typeDefinition.IncludeDispenser == true) 
                {
                    changed |= WriteTypeTemplate(genFolder, typeDefinition, new DispenserTemplate());
                }

                if (typeDefinition.IncludeCollection == true) 
                {
                    changed |= WriteTypeTemplate(genFolder, typeDefinition, new ScriptableCollectionTemplate());
                }
            }

            if (changed) AssetDatabase.Refresh();
        }

        [Button] 
        void SetIcons() 
        {
            // get the path to this assset file
            string path = AssetDatabase.GetAssetPath(this);
            // get the path to the folder that contains this asset file
            string folder = Path.GetDirectoryName(path);

            // create a gen folder
            string genFolder = Path.Combine(folder, "Generated");

            // ensure the folder exists 
            Directory.CreateDirectory(genFolder);

            foreach (OssifyTypeDefinition typeDefinition in typeDefinitions)
            {
                if (typeDefinition.IncludeVariable == true) 
                {
                    WriteTypeIcon(genFolder, typeDefinition, OssifyEditorResources.VariableIcon, new VariableTemplate());
                }    

                if (typeDefinition.IncludeReference == true) 
                {
                    WriteTypeIcon(genFolder, typeDefinition, OssifyEditorResources.ReferenceIcon, new VariableReferenceTemplate());                    
                }

                if (typeDefinition.IncludePubSub == true) 
                {
                    WriteTypeIcon(genFolder, typeDefinition, OssifyEditorResources.SubscriberIcon, new VariableSubscriberTemplate());
                    WriteTypeIcon(genFolder, typeDefinition, OssifyEditorResources.PublisherIcon, new VariablePublisherTemplate());                    
                }

                if (typeDefinition.IncludeDispenser == true) 
                {
                    WriteTypeIcon(genFolder, typeDefinition, OssifyEditorResources.DispenserIcon, new DispenserTemplate());
                }

                if (typeDefinition.IncludeCollection == true) 
                {
                    WriteTypeIcon(genFolder, typeDefinition, OssifyEditorResources.CollectionIcon, new ScriptableCollectionTemplate());
                }
            }

            //OssifyEditorResources
        }

        private void WriteTypeIcon(string folder, OssifyTypeDefinition typeDefinition, Texture2D icon, ITypeTemplate template)
        {
            MapToTemplate(typeDefinition, template);

            string fileName = template.FileName;
            string filePath = Path.Combine(folder, fileName);

            var monoImporter = AssetImporter.GetAtPath(filePath) as MonoImporter;

            if (monoImporter == null) return;

            monoImporter.SetIcon(icon);
            monoImporter.SaveAndReimport();
        }

        private bool WriteTypeTemplate(string folder, OssifyTypeDefinition typeDefinition, ITypeTemplate template)
        {
            MapToTemplate(typeDefinition, template);

            string fileName = template.FileName;
            string filePath = Path.Combine(folder, fileName);

            string code = template.TransformText();
        
            // check the contents are different before writing
            if (File.Exists(filePath) && File.ReadAllText(filePath) == code) return false;

            File.WriteAllText(filePath, code);         

            return true;
        }

        private void MapToTemplate(OssifyTypeDefinition typeDefinition, ITypeTemplate template)
        {
            template.Name = typeDefinition.Name;
            template.FriendlyName = typeDefinition.FriendlyName;
            template.Type = typeDefinition.Type;
            template.MenuName = menuName;
            template.Namespace = outputNamespace;
            template.MenuOrder = 0;
        }
    }

    [Serializable] 
    public class OssifyTypeDefinition
    {
        [SerializeField] private string name = "";
        [SerializeField] private string friendlyName = "";
        [SerializeField] private string type;
        [SerializeField] private bool includeVariable = true;
        [SerializeField] private bool includeReference = true;
        [SerializeField] private bool includePubSub = true;
        [SerializeField] private bool includeCollection;
        [SerializeField] private bool includeDispenser;

        public string Name => name;

        public string Type => type;

        public string FriendlyName => friendlyName;

        public bool IncludeVariable => includeVariable;

        public bool IncludeReference => includeReference;

        public bool IncludePubSub => includePubSub;

        public bool IncludeCollection => includeCollection;

        public bool IncludeDispenser => includeDispenser;
    }
}