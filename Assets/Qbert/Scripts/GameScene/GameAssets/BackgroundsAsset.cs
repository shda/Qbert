using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class BackgroundsAsset : ScriptableObject
{
    [Serializable]
    public class SceneBackground 
    {
        public string image;
        public string animation;
    }

    public SceneBackground[] sceneBackgrounds;

    public string[] imageNames;
    public string[] animationsNames;

#if UNITY_EDITOR
    public Transform[] prefImages;
    public Transform[] prefAnimations;
#endif

#if UNITY_EDITOR
    void OnValidate()
    {
        imageNames = prefImages.Select(x => x.name).ToArray();
        animationsNames = prefAnimations.Select(x => x.name).ToArray();
    }

    public void RemoveOrderBy(int index)
    {
        List<SceneBackground> sb = new List<SceneBackground>(sceneBackgrounds);
        sb.RemoveAt(index);

        sceneBackgrounds = sb.ToArray();
    }
#endif


}
