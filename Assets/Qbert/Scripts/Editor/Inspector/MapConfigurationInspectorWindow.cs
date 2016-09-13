using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEditor.VersionControl;

[CustomEditor(typeof(MapAsset))]
public class MapConfigurationInspectorWindow : Editor
{
    private const float buttonSizeX = 20;
    private const float buttonSizeY = 20;

    private bool listVisibility = true;
    private int selectPatern = 0;
    private int selectColor = 0;


    private Color[] colors = new Color[]
    {
        Color.red , Color.green , Color.yellow, Color.magenta, Color.blue
    };

    public override void OnInspectorGUI()
    {
        DrawParams();
        DrawCubePaterns();
        DrawMap();
        DrawColor();

        DrawDefaultInspector();


        serializedObject.ApplyModifiedProperties();
    }


    public void UpdateProp()
    {
        var v = serializedObject.FindProperty("i");
        v.intValue = 10;
        //serializedObject.ApplyModifiedProperties();
        v.intValue = 11;
        //serializedObject.ApplyModifiedProperties();
    }

    private void DrawColor()
    {
        EditorGUILayout.Separator();
        GUILayout.Label("Start points id:");


        GUILayout.BeginHorizontal();
        {
                var oldColor = GUI.backgroundColor;

                for (int x = 0; x < colors.Length; x++)
                {
                    GUILayout.BeginVertical();
                    {

                        GUILayout.Label("" + x);

                        GUI.backgroundColor = colors[x];

                        bool isSelect = selectColor == x;

                        if (GUILayout.Toggle(isSelect, "", GUILayout.Width(buttonSizeX + 10),
                            GUILayout.Height(buttonSizeY + 10)))
                        {
                            selectColor = x;
                        }
                    }
                    GUILayout.EndVertical();
                }

                GUI.backgroundColor = oldColor;
        }
        GUILayout.EndHorizontal();
    }

    private void DrawParams()
    {
        var widthInt = serializedObject.FindProperty("mapWidth");
        var hightInt = serializedObject.FindProperty("mapHight");

        EditorGUILayout.PropertyField(widthInt, new GUIContent("Width map"));
        EditorGUILayout.PropertyField(hightInt, new GUIContent("Hight map"));
    }

    private void DrawCubePaterns()
    {
        var cubePaterns = serializedObject.FindProperty("cubePaterns");

        listVisibility = EditorGUILayout.Foldout(listVisibility, "Cube paterns");

        if (listVisibility)
        {
            EditorGUI.indentLevel++;

            int arraySize = cubePaterns.arraySize;
            int changeSize = EditorGUILayout.IntField("Size", arraySize);
            if (arraySize != changeSize)
            {
                cubePaterns.arraySize = changeSize;
            }

            if (selectPatern >= cubePaterns.arraySize)
                selectPatern = 0;

            for (int i = 0; i < cubePaterns.arraySize; i++)
            {
                GUILayout.BeginHorizontal();
                {
                    SerializedProperty elementProperty = cubePaterns.GetArrayElementAtIndex(i);

                    if (GUILayout.Toggle(selectPatern == i, ""))
                    {
                        selectPatern = i;
                    }

                    Rect drawZone = GUILayoutUtility.GetRect(30, 16f);
                    EditorGUI.LabelField(drawZone , "" + i);

                    drawZone = GUILayoutUtility.GetRect(400f, 16f);
                    EditorGUI.ObjectField(drawZone, elementProperty, new GUIContent(""));
                }
                
                GUILayout.EndHorizontal();
            }

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawMap()
    {
        MapAsset mapAsset = serializedObject.targetObject as MapAsset;

        var array = mapAsset.map.cubeArray;
        mapAsset.UpdateFromInspector();

        GUILayout.BeginVertical();
        for (int y = 0; y < mapAsset.mapHight; y++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space((buttonSizeX * 0.5f + 2) * ((y + 1) % 2));

            for (int x = 0; x < mapAsset.mapWidth; x++)
            {
                CubeMap.CubeInMap cube = mapAsset.map.GetCubeInMap(x, y);

                string text = "";

                var oldColor = GUI.backgroundColor;


                if (cube.isEnable)
                {
                    var patern = cube.cubePatern;

                    if (patern)
                    {
                        for (int i = 0; i < mapAsset.cubePaterns.Length; i++)
                        {
                            if (mapAsset.cubePaterns[i] == patern)
                            {
                                text = "" + i;
                            }
                        }
                    } 
                }

                if (cube.id >= 0)
                {
                    GUI.backgroundColor = colors[cube.id];
                }
                
                if (GUILayout.Button(text, GUILayout.Width(buttonSizeX), 
                    GUILayout.Height(buttonSizeY)))
                {
                    if (Event.current.button == 0)
                    {
                        cube.isEnable = !cube.isEnable;
                        cube.cubePatern = mapAsset.cubePaterns[selectPatern];
                    }
                    else if (Event.current.button == 1)
                    {
                        if (cube.id == selectColor)
                        {
                            cube.id = -1;
                        }
                        else
                        {
                            cube.id = selectColor;
                        }
                    }

                    UpdateProp();
                }

                GUI.backgroundColor = oldColor;
            }

            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }
}
