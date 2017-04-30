using System.Collections;
using System.Collections.Generic;
using Assets.Qbert.Scripts.LoadScene;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BackgroundsAsset))]
public class EditorAssetScene : Editor
{
    private BackgroundsAsset script;

    private SerializedProperty prefImages;
    private SerializedProperty prefAnimations;


    private void OnEnable()
    {
        script = target as BackgroundsAsset;

        prefImages = serializedObject.FindProperty("prefImages");
        prefAnimations = serializedObject.FindProperty("prefAnimations");

    }

    public override void OnInspectorGUI()
    {
        /*
        serializedObject.Update();
        EditorGUILayout.PropertyField(prefImages);
        EditorGUILayout.PropertyField(prefAnimations);
        serializedObject.ApplyModifiedProperties();
        */
        DrawDefaultInspector();

        /*
        if (tipScript != null
        )
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Clear List"))
            {
                tipScript.listShowElelentSpecification.Clear();
            }

            if (GUILayout.Button("Refresh List"))
            {
                tipScript.RefreshFromInspector();
            }

            serializedObject.Update();

            ReorderableListGUI.Title("Specification show to window");
            ReorderableListGUI.ListField(listShowElelentSpecification, ReorderableListFlags.HideAddButton);

            serializedObject.ApplyModifiedProperties();
        }
        */
    }
}
