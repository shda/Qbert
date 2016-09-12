using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapAsset))]
public class ConfigurationInspectorWindow : Editor
{
    private const float buttonSizeX = 20;
    private const float buttonSizeY = 20;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapAsset mapAsset = serializedObject.targetObject as MapAsset;

        var array = mapAsset.map.cubeArray;

        mapAsset.UpdateFromInspector();

        GUILayout.BeginVertical();
        for (int x = 0; x < mapAsset.width; x++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space( (buttonSizeX * 0.5f + 2) * (x % 2));

            for (int y = 0; y < mapAsset.hight; y++)
            {
                CubeMap.CubeInMap cube = mapAsset.map.GetCubeInMap(x, y);

                string text = cube.isEnable ? "1" : "";

                if (GUILayout.Button(text, GUILayout.Width(buttonSizeX), GUILayout.Height(buttonSizeY)))
                {
                    cube.isEnable = !cube.isEnable;
                }
            }

            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

    }
}
