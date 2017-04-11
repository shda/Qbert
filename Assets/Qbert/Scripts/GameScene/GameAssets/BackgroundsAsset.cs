using System;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class BackgroundsAsset : IGetValueInArray<string>
{
#if UNITY_EDITOR
    public SceneAsset[] sceneAssets;
#endif

#if UNITY_EDITOR
    void OnValidate()
    {
        if (sceneAssets != null)
        {
            values = sceneAssets.Select(x => x.name).ToArray();
        }
        Debug.Log("OnValidate");
    }
#endif
}
