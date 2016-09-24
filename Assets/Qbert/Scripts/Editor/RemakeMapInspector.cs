using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.MapLoader;
using UnityEditor;

[CustomEditor(typeof(MapFieldGenerator))]
public class RemakeMapInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapFieldGenerator myScript = (MapFieldGenerator)target;
        if (GUILayout.Button("Remake map"))
        {
            //myScript.CreateMap();
            myScript.CreateMap();
        }
    }
}
