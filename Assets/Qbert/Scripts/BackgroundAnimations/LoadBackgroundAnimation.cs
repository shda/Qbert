using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBackgroundAnimation : MonoBehaviour
{
    public BackgroundsAsset asset;
    public BackgroundsImages backgroundsImages;

    void Start()
    {
        StartLoad();
    }

    private void StartLoad()
    { 
        int count = backgroundsImages.images.Length;
        int index = Random.Range(0, count);

        backgroundsImages.EnableImage(index);

        string sceneName = asset.GetValue();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
