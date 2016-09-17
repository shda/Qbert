using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.VersionControl;

[CustomEditor(typeof(MapAsset))]
public class MapConfigurationInspectorWindow : Editor
{
    private const float buttonSizeX = 20;
    private const float buttonSizeY = 20;

    private bool listVisibility = true;
    private int selectCubePattern;
    private int selectColor;
    private int selectGameplayObject;

    private int sizeWidth;
    private int sizeHight;

    private Transform[] oldPaterns;
    private GameplayObject[] gameplayObjects = null;


    private Color[] colors = new Color[]
    {
        Color.cyan,
        Color.red,
    };

    public override void OnInspectorGUI()
    {
        var gamePlayPaterns = serializedObject.FindProperty("gameplayObjectsAsset");
        if (gamePlayPaterns != null)
        {
            var gameplayObjectsAsset = gamePlayPaterns.objectReferenceValue as GameplayObjectsAsset;
            if (gameplayObjectsAsset)
            {
                gameplayObjects = gameplayObjectsAsset.prefabs;
            }
        }

        DrawParams();
        DrawCubePaterns();
        DrawMap();
        DrawMove();
        DrawSelectGameplayObject();

       // DrawDefaultInspector();

        serializedObject.ApplyModifiedProperties();
    }

    void DrawMove()
    {
        MapAsset mapAsset = serializedObject.targetObject as MapAsset;
        var map = mapAsset.map;

        EditorUtility.SetDirty(mapAsset);

        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Left"))
            {
                map.Move(-1, 0);
            }
            GUILayout.BeginVertical();
            {
                if (GUILayout.Button("Up"))
                {
                    map.Move(0, -1);
                }

                if (GUILayout.Button("Down"))
                {
                    map.Move(0, 1);
                }
            }
            GUILayout.EndVertical();

            if (GUILayout.Button("Right"))
            {
                map.Move(1, 0);
            }
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Clear"))
        {
            if (EditorUtility.DisplayDialog("Clear?", "Clear?", "Ok", "Cancel"))
            {
                map.Clear();
                oldPaterns = null;
            }
        }

    }

    private void DrawSelectGameplayObject()
    {
        if (gameplayObjects != null)
        {
            EditorGUILayout.Separator();
            GUILayout.Label("Select color:");

            GUILayout.BeginHorizontal();
            {
                var oldColor = GUI.backgroundColor;

                for (int x = 0; x < colors.Length; x++)
                {
                    var go = gameplayObjects[selectGameplayObject];

                    if (x == 0 && !go.editorRules.isUsingStartPosition)
                        continue;

                    if (x == 1 && !go.editorRules.isUsingEndPosition)
                        continue;

                    GUILayout.BeginVertical();
                    {
                        if (x == 0)
                        {
                            if (go.editorRules.isNecessaryStartPoint)
                            {
                                GUILayout.Label("Start point(necessary)");
                            }
                            else
                            {
                                GUILayout.Label("Start point(not necessary)");
                            }
                            
                        }
                        else if (x == 1)
                        {
                            if (go.editorRules.isNecessaryEndPoint)
                            {
                                GUILayout.Label("End point(necessary)");
                            }
                            else
                            {
                                GUILayout.Label("End point(not necessary)");
                            }
                        }
                            

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



            EditorGUILayout.Separator();
            GUILayout.Label("Select object:");
            GUILayout.BeginVertical();
            {
                for (int x = 0; x < gameplayObjects.Length; x++)
                {
                    bool isSelect = selectGameplayObject == x;

                    if (GUILayout.Toggle(isSelect, gameplayObjects[x].typeObject.ToString()))
                    {
                        selectGameplayObject = x;
                    }
                }
            }
            GUILayout.EndVertical();
        }
    }

    private void DrawParams()
    {
        var widthInt = serializedObject.FindProperty("mapWidth");
        var hightInt = serializedObject.FindProperty("mapHight");

        sizeWidth = widthInt.intValue;
        sizeHight = hightInt.intValue;

        EditorGUILayout.PropertyField(widthInt, new GUIContent("Width map"));
        EditorGUILayout.PropertyField(hightInt, new GUIContent("Hight map"));

        MapAsset mapAsset = (MapAsset) serializedObject.targetObject ;

        if (GUILayout.Button("Update size"))
        {
            mapAsset.UpdateFromInspector();
        }

        var gamePlayPaterns = serializedObject.FindProperty("gameplayObjectsAsset");
        EditorGUILayout.PropertyField(gamePlayPaterns, new GUIContent("Gameplay objects asset"));

        /*
        if (oldPaterns == null)
        {
            oldPaterns = mapAsset.cubePaterns.ToArray();
        }
        else if(oldPaterns.Length != mapAsset.cubePaterns.Length)
        {
            oldPaterns = mapAsset.cubePaterns.ToArray();
        }
        else
        {
            for (int i = 0; i < oldPaterns.Length; i++)
            {
                if (mapAsset.cubePaterns[i] != oldPaterns[i])
                {
                    Debug.Log("No");

                    foreach (var cube in mapAsset.map.cubeArray)
                    {
                        if (cube.cubePattern == oldPaterns[i])
                        {
                            cube.cubePattern = mapAsset.cubePaterns[i];
                        }
                    }
                }

                oldPaterns[i] = mapAsset.cubePaterns[i];
            }
        }
        */
      //  oldPaterns = mapAsset.cubePaterns;
    }
    private void DrawCubePaterns()
    {
        var cubePaterns = serializedObject.FindProperty("cubePaterns");

        listVisibility = EditorGUILayout.Foldout(listVisibility, "Cube patterns");


        if (listVisibility)
        {
            EditorGUI.indentLevel++;

            int arraySize = cubePaterns.arraySize;
            int changeSize = EditorGUILayout.IntField("Size", arraySize);
            if (arraySize != changeSize)
            {
                cubePaterns.arraySize = changeSize;
            }

            if (selectCubePattern >= cubePaterns.arraySize)
                selectCubePattern = 0;

            for (int i = 0; i < cubePaterns.arraySize; i++)
            {
                GUILayout.BeginHorizontal();
                {
                    SerializedProperty elementProperty = cubePaterns.GetArrayElementAtIndex(i);

                    if (GUILayout.Toggle(selectCubePattern == i, ""))
                    {
                        selectCubePattern = i;
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

        GUILayout.BeginVertical();

        if (mapAsset.map.width == sizeWidth && mapAsset.map.hight == sizeHight)
        {
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
                        var patern = cube.cubePattern;

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

                    GameplayObject go = gameplayObjects[selectGameplayObject];

                    var type = go.typeObject;

                    if (cube.listTypeObjectsStartPoint != null)
                    {
                        if ( cube.listTypeObjectsStartPoint.Any(ob => ob == type) )
                        {
                            GUI.backgroundColor = colors[0];
                        }
                    }

                    if (cube.listTypeObjectsEndPoint != null)
                    {
                        if (cube.listTypeObjectsEndPoint.Any(ob => ob == type))
                        {
                            GUI.backgroundColor = colors[1];
                        }
                    }

                    if (GUILayout.Button(text, GUILayout.Width(buttonSizeX),
                        GUILayout.Height(buttonSizeY)))
                    {
                        if (Event.current.button == 0)
                        {
                            cube.isEnable = !cube.isEnable;
                            cube.cubePattern = mapAsset.cubePaterns[selectCubePattern];
                        }
                        else if (Event.current.button == 1)
                        {
                            if (selectColor == 0)
                            {
                                if (cube.listTypeObjectsStartPoint == null)
                                {
                                    cube.listTypeObjectsStartPoint = new List<GameplayObject.Type>();
                                }

                                var index = cube.listTypeObjectsStartPoint.FindIndex(ob => ob == type);
                                if (index != -1)
                                {
                                    cube.listTypeObjectsStartPoint.RemoveAt(index);
                                }
                                else
                                {
                                    cube.listTypeObjectsStartPoint.Add(type);
                                }

                                if (cube.listTypeObjectsEndPoint == null)
                                {
                                    cube.listTypeObjectsEndPoint = new List<GameplayObject.Type>();
                                }
                                index = cube.listTypeObjectsEndPoint.FindIndex(ob => ob == type);
                                if (index != -1)
                                {
                                    cube.listTypeObjectsEndPoint.RemoveAt(index);
                                }
                            }
                            else 
                            if (selectColor == 1)
                            {
                                if (cube.listTypeObjectsEndPoint == null)
                                {
                                    cube.listTypeObjectsEndPoint = new List<GameplayObject.Type>();
                                }

                                var index = cube.listTypeObjectsEndPoint.FindIndex(ob => ob == type);
                                if (index != -1)
                                {
                                    cube.listTypeObjectsEndPoint.RemoveAt(index);
                                }
                                else
                                {
                                    cube.listTypeObjectsEndPoint.Add(type);
                                }

                                if (cube.listTypeObjectsStartPoint == null)
                                {
                                    cube.listTypeObjectsStartPoint = new List<GameplayObject.Type>();
                                }
                                index = cube.listTypeObjectsStartPoint.FindIndex(ob => ob == type);
                                if (index != -1)
                                {
                                    cube.listTypeObjectsStartPoint.RemoveAt(index);
                                }
                            }
                        }
                    }

                    GUI.backgroundColor = oldColor;
                }

                GUILayout.EndHorizontal();
            }
        }
        
        GUILayout.EndVertical();
    }
}
