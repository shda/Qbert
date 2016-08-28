using System;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

public class CreateConfigurationAsset : MonoBehaviour 
{
    [MenuItem("Assets/Create configuration asser")]
    static void CreateSettingsAsset()
    {
        CreateAsset<ConfigurationAsset>("Configuration");
    }


    static private void CreateAsset<T>(String name) where T : ScriptableObject
    {
        var dir = "Assets/";
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
