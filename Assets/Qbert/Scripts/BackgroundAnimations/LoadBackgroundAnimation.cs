using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBackgroundAnimation : MonoBehaviour
{
    public string imagesParefabsPath;
    public string animationsParefabsPath;

    public Transform rootImage;
    public Transform roomAnimations;

    public BackgroundsAsset asset;

    public FlashBackground flashBackground;

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

    private void DestroyAllChildrens(Transform root)
    {
        root.Cast<Transform>().ForAll(
            x => DestroyImmediate(x.gameObject));
    }

    private void LoadAnimation(BackgroundsAsset.SceneBackground selectBackground)
    {
        DestroyAllChildrens(roomAnimations);

        Transform animationResource = Resources.Load<Transform>(animationsParefabsPath + selectBackground.animation);

        var animation = Instantiate(animationResource);
        animation.SetParent(roomAnimations);
        animation.localScale = new Vector3(1, 1, 1);
    }


    private void LoadImage(BackgroundsAsset.SceneBackground selectBackground)
    {
        DestroyAllChildrens(rootImage);

        string pathInResoureFolder = imagesParefabsPath + selectBackground.image;

        Transform imageResource = Resources.Load<Transform>(pathInResoureFolder);

        if (imageResource != null)
        {
            var image = Instantiate(imageResource);
            RectTransform rt = image.GetComponent<RectTransform>();
            image.SetParent(rootImage);
            image.localScale = new Vector3(1, 1, 1);
            image.localPosition = new Vector3(1, 1);
            rt.sizeDelta = new Vector2(0, 0);

            flashBackground.flashColor = image.GetComponent<FlashColorBase>();
        }
        else
        {
            Debug.LogError("Error load background - " + pathInResoureFolder);
        }
    }

    private void StartLoad()
    {
        int randomIndes = Random.Range(0, asset.sceneBackgrounds.Length);

        var selectBackground = asset.sceneBackgrounds[randomIndes];
        LoadBackground(selectBackground);
    }
}
