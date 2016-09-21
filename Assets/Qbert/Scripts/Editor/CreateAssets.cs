using System;
using UnityEngine;
using System.Collections;
using System.IO;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using UnityEditor;

public class CreateAssets : MonoBehaviour 
{
    [MenuItem("Assets/Create asset/Create map asset")]
    static void CreateMapAsset()
    {
        CreateAsset<MapAsset>("MapAsset");
    }

    [MenuItem("Assets/Create asset/Create configuration asset")]
    static void CreateConfigurationAsset()
    {
        CreateAsset<LevelConfigAsset>("Configuration");
    }

    [MenuItem("Assets/Create asset/Create gameplay objects asset")]
    static void CreateGameplayobjectsAsset()
    {
        CreateAsset<GameplayObjectsAsset>("GameplayObjects");
    }

    [MenuItem("Assets/Create asset/Create global configuration")]
    static void CreateGlobalAsset()
    {
        CreateAsset<GlobalConfigurationAsset>("GlobalConfigurationAsset");
    }

    static private void CreateAsset<T>(string name) where T : ScriptableObject
    {
        var dir = "Assets/Configuration/";
        var selected = Selection.activeObject;
        if (selected != null)
        {
            var assetDir = AssetDatabase.GetAssetPath(selected.GetInstanceID());
            if (assetDir.Length > 0 && Directory.Exists(assetDir))
                dir = assetDir + "/";
        }
        ScriptableObject asset = ScriptableObject.CreateInstance<T>();
        AssetDatabase.CreateAsset(asset, dir + name + ".asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
