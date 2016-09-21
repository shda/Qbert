using System;
using UnityEngine;
using System.Collections;
using Assets.CommonB.UI;
using UnityEngine.UI;

public class GuiSettings : MonoBehaviour
{
    public float durationMoveCameraToSettings;
    public Transform rootMainGame;
    public Transform rootSetting;

    public Transform rootRules;

    public CameraController cameraController;

    public GuiHandle guiHandle;
    public LoadScene loadScene;
    public FadeScreen fadeScreen;

    public Image[] hideImages;

    public void StartGame()
    {
        Debug.Log("StartGame");

        guiHandle.enabled = false;


        fadeScreen.OnEnd = transform1 =>
        {
            GlobalSettings.currentLevel = 0;
            GlobalSettings.currentRound = 0;

            loadScene.OnLoadScene();
        };

        fadeScreen.StartEnable(0.5f);
    }

    void Start()
    {
        fadeScreen.OnEnd = transform1 =>
        {
            
        };

        fadeScreen.StartDisable(1.0f);
    }

    public void OnMoveCameraToSettings()
    {
        HideObjects(true);
        cameraController.MoveCameraToPoint(rootSetting.position , durationMoveCameraToSettings);
    }

    public void OnPressRules()
    {
        cameraController.MoveCameraToPoint(rootRules.position, durationMoveCameraToSettings);

        rootRules.gameObject.SetActive(true);
    }

    public void OnPressCloseSetting(PressProxy pressProxy)
    {
        HideObjects(false);
        cameraController.MoveCameraToPoint(
            rootMainGame.position, durationMoveCameraToSettings , transform1 =>
        {
            
        });
    }

    public void HideObjects(bool isHide)
    {
        StopAllCoroutines();

        foreach (var hideImage in hideImages)
        {
            if (isHide)
            {
                StartCoroutine(this.ChangeColorImage(hideImage, new Color(1, 1, 1, 0), 0.5f));
            }
            else
            {
                StartCoroutine(this.ChangeColorImage(hideImage, new Color(1, 1, 1, 1), 0.5f));
            }
            
        }
    }
}
