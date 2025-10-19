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
        
        private bool showPreview;
        private Vector2 previewScrollPosition;

        private void OnEnable()
        {
            licenseListProp = serializedObject.FindProperty("licenseList");
            libraryNameBracketStartProp = serializedObject.FindProperty("libraryNameBracketStart");
            libraryNameBracketEndProp = serializedObject.FindProperty("libraryNameBracketEnd");
            librarySeparatorProp = serializedObject.FindProperty("librarySeparator");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(licenseListProp, true);

            EditorGUILayout.LabelField("Library Name Brackets");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(libraryNameBracketStartProp, GUIContent.none, GUILayout.Width(30));
            EditorGUILayout.LabelField("LIBRARY NAME", GUILayout.Width(90));
            EditorGUILayout.PropertyField(libraryNameBracketEndProp, GUIContent.none, GUILayout.Width(30));
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.PropertyField(librarySeparatorProp);

            serializedObject.ApplyModifiedProperties();

            if(targets.Length != 1) return;
            
            EditorGUILayout.Space();
            showPreview = EditorGUILayout.Foldout(showPreview, "Licenses Preview", true);

            if(!showPreview) return;
            
            var licenseBuilder = (LicenseBuilder)target;
            var previewText = licenseBuilder.Build();
                    
            EditorGUILayout.Space(5);
                    
            // スクロールビューは通常通り操作可能
            previewScrollPosition = EditorGUILayout.BeginScrollView(
                previewScrollPosition, 
                GUILayout.Height(200)
            );
                    
            // テキストエリアのみ編集不可能
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.TextArea(
                    previewText,
                    GUILayout.ExpandHeight(true)
                );
            }
                    
            EditorGUILayout.EndScrollView();

            // コピーボタン
            EditorGUILayout.Space(5);
            if(GUILayout.Button("Copy License Text to Clipboard"))
            {
                EditorGUIUtility.systemCopyBuffer = previewText;
                Debug.Log("License text copied to clipboard!");
            }
        }
    }
}
