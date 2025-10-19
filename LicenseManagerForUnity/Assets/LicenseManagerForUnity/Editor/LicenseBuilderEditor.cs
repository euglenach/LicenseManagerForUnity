using UnityEditor;
using UnityEngine;

namespace LicenseManagerForUnity.Editor
{

    [CustomEditor(typeof(LicenseBuilder))]
    public class LicenseBuilderEditor : UnityEditor.Editor
    {
        private SerializedProperty licenseListProp;
        private SerializedProperty libraryNameBracketStartProp;
        private SerializedProperty libraryNameBracketEndProp;
        private SerializedProperty librarySeparatorProp;
        private SerializedProperty outputFileProp;
        
        private bool showPreview;
        private Vector2 previewScrollPosition;

        private void OnEnable()
        {
            licenseListProp = serializedObject.FindProperty("licenseList");
            libraryNameBracketStartProp = serializedObject.FindProperty("libraryNameBracketStart");
            libraryNameBracketEndProp = serializedObject.FindProperty("libraryNameBracketEnd");
            librarySeparatorProp = serializedObject.FindProperty("librarySeparator");
            outputFileProp = serializedObject.FindProperty("outputFile");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(licenseListProp, true);

            EditorGUILayout.LabelField("Library Name Brackets");
            using(new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(libraryNameBracketStartProp, GUIContent.none, GUILayout.Width(30));
                EditorGUILayout.LabelField("LIBRARY NAME", GUILayout.Width(90));
                EditorGUILayout.PropertyField(libraryNameBracketEndProp, GUIContent.none, GUILayout.Width(30));
            }
            
            EditorGUILayout.PropertyField(librarySeparatorProp);
            EditorGUILayout.PropertyField(outputFileProp);

            serializedObject.ApplyModifiedProperties();
            
            var licenseBuilder = (LicenseBuilder)target;

            // Export to file button
            if(outputFileProp.objectReferenceValue != null && GUILayout.Button("Export to file"))
            {
                licenseBuilder.ExportToFile((TextAsset)outputFileProp.objectReferenceValue);
            }

            if(targets.Length != 1) return;
            
            // Preview
            EditorGUILayout.Space();
            showPreview = EditorGUILayout.Foldout(showPreview, "Licenses Preview", true);

            if(!showPreview) return;
            
            var previewText = licenseBuilder.Build();
                    
            EditorGUILayout.Space(5);
            
            previewScrollPosition = EditorGUILayout.BeginScrollView(previewScrollPosition, GUILayout.Height(200));
            
            using(new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.TextArea(previewText, GUILayout.ExpandHeight(true));
            }
                    
            EditorGUILayout.EndScrollView();

            // Copy to clipboard button
            EditorGUILayout.Space(5);
            if(GUILayout.Button("Copy License Text to Clipboard"))
            {
                EditorGUIUtility.systemCopyBuffer = previewText;
                Debug.Log("License text copied to clipboard!");
            }
        }
    }
}
