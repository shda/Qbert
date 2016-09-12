using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapLoader))]
public class MapLoaderInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapLoader myScript = (MapLoader)target;
        if (GUILayout.Button("Reload map"))
        {
            myScript.CreateMap();
        }
    }
}
