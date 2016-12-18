using System;
using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using UnityEditor;

[CustomEditor(typeof(LevelConfigAsset)), CanEditMultipleObjects]
public class LevelConfigAssetEditor : Editor
{
    private Editor _editor;

    public override void OnInspectorGUI()
    {/*
        serializedObject.Update();

        var myAsset = serializedObject.FindProperty("rulesCustom");
        //var rules = myAsset.("rulesCustom");
        if (myAsset != null && myAsset.objectReferenceValue != null)
        {
            CreateCachedEditor(myAsset.objectReferenceValue, null, ref _editor);
            _editor.OnInspectorGUI();

            serializedObject.ApplyModifiedProperties();

        }
        */
        base.OnInspectorGUI();
    }
}
