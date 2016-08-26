using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class RemakeMap : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapGenerator myScript = (MapGenerator)target;
        if (GUILayout.Button("Remake map"))
        {
            myScript.CreateMap();
        }
    }
}
