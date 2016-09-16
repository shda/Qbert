﻿using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEditor.VersionControl;

[CustomEditor(typeof(GameplayObjectsAsset))]
public class GameplayObjectInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DrawParams();
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawParams()
    {
        var gamePlayPaterns = serializedObject.FindProperty("prefabs");
        
        var asset = gamePlayPaterns.serializedObject.targetObject as GameplayObjectsAsset;

        if (asset != null)
        {
            foreach (var prefab in asset.prefabs)
            {
                EditorUtility.SetDirty(prefab);

                GUILayout.BeginHorizontal();
                {
                    Rect drawZone = GUILayoutUtility.GetRect(0, 16f);
                    EditorGUI.LabelField(drawZone, prefab.typeGameobject.ToString());
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    GUILayout.Space(10);
                    Rect drawZone = GUILayoutUtility.GetRect(100, 16f);
                    prefab.startPositionId = EditorGUI.IntField(drawZone, "Start position id", prefab.startPositionId);

                    drawZone = GUILayoutUtility.GetRect(100, 16f);
                    prefab.endPositionId = EditorGUI.IntField(drawZone, "End position id", prefab.endPositionId);

                    
                }
                GUILayout.EndHorizontal();
            }
            
        }

        gamePlayPaterns.serializedObject.UpdateIfDirtyOrScript();

        gamePlayPaterns.serializedObject.ApplyModifiedProperties();
    }
}
