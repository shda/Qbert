using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.MapLoader;
using UnityEditor;

[CustomEditor(typeof(GameFieldGenerator))]
public class RemakeMapInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameFieldGenerator myScript = (GameFieldGenerator)target;
        if (GUILayout.Button("Remake map"))
        {
            //myScript.CreateMap();
            myScript.CreateMap();
        }
    }
}
