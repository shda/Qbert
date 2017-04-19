using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBackgroundAnimation : MonoBehaviour
{
    public string imagesParefabsPath;
    public string animationsParefabsPath;

    public Transform rootImage;
    public Transform roomAnimations;

    public Transform image;
    public Transform animation;

    public BackgroundsAsset asset;

    public static LoadBackgroundAnimation instance { get; private set; }


    void Start()
    {
        instance = this;

        Resources.UnloadUnusedAssets();
        StartLoad();
    }

    public void LoadBackground(BackgroundsAsset.SceneBackground selectBackground)
    {
        LoadImage(selectBackground);
        LoadAnimation(selectBackground);
    }

    private void LoadAnimation(BackgroundsAsset.SceneBackground selectBackground)
    {
        if (animation != null)
        {
            DestroyImmediate(animation.gameObject);
        }

        Transform animationResource = Resources.Load<Transform>(animationsParefabsPath + selectBackground.animation);

        animation = Instantiate(animationResource);
        animation.SetParent(roomAnimations);
        animation.localScale = new Vector3(1, 1, 1);
    }


    private void LoadImage(BackgroundsAsset.SceneBackground selectBackground)
    {
        if (image != null)
        {
            DestroyImmediate(image.gameObject);
        }

        Transform imageResource = Resources.Load<Transform>(imagesParefabsPath + selectBackground.image);

        image = Instantiate(imageResource);
        RectTransform rt = image.GetComponent<RectTransform>();
        image.SetParent(rootImage);
        image.localScale = new Vector3(1, 1, 1);
        image.localPosition = new Vector3(1, 1);
        rt.sizeDelta = new Vector2(0, 0);
    }

    private void StartLoad()
    {
        var selectBackground = asset.sceneBackgrounds[0];
        LoadBackground(selectBackground);
    }
}
