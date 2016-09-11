using System;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

public class CreateMapAsset : MonoBehaviour 
{
    [MenuItem("Assets/Create map asset")]
    static void CreateSettingsAsset()
    {
        CreateAsset<MapAsset>("MapAsset");
    }

    static private void CreateAsset<T>(String name) where T : ScriptableObject
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
