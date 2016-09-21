using System;
using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.LoadScene;
using UnityEditor;

[CustomEditor(typeof(LoadScene))]
public class LoadSceneEditor : Editor
{
    private LoadScene script;
    private SerializedProperty sceneSelect;
    private SerializedProperty stringScenes;

    private SerializedProperty clearScensSteck;

    public int index = 0;

    private void OnEnable()
    {
        script = target as LoadScene;

        stringScenes        = serializedObject.FindProperty("scenes");
        sceneSelect         = serializedObject.FindProperty("sceneName");
        //clearScensSteck     = serializedObject.FindProperty("clearScensSteck");

        index = Array.IndexOf(script.scenes, script.sceneName);

        if (index < 0) index = 0;
    }

    public override void OnInspectorGUI()
    {
        if (script != null)
        {
            serializedObject.Update();

            script.ReadScenes();
            index = EditorGUILayout.Popup(index, script.scenes);
            sceneSelect.stringValue = script.scenes[index];

            EditorGUILayout.PropertyField(sceneSelect);
            //EditorGUILayout.PropertyField(clearScensSteck);

            serializedObject.ApplyModifiedProperties(); 
        }
    }
}
