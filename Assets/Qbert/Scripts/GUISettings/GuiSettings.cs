using System;
using UnityEngine;
using System.Collections;

public class GuiSettings : MonoBehaviour
{
    public float durationMoveCameraToSettings;
    public Transform rootMainGame;
    public Transform rootSetting;
    public Transform rootRules;

    public GuiDispatcher guiDispatcher;

    public CameraController cameraController;

    public void OnMoveCameraToSettings()
    {
        cameraController.MoveCameraToPoint(rootSetting.position , durationMoveCameraToSettings);
    }

    public void OnPressRules()
    {
        cameraController.MoveCameraToPoint(rootRules.position, durationMoveCameraToSettings);
    }

    public void OnPressCloseSetting(PressProxy pressProxy)
    {
        cameraController.MoveCameraToPoint(
            rootMainGame.position, durationMoveCameraToSettings , transform1 =>
        {
            guiDispatcher.mainMenuGui.gameObject.SetActive(true);
        });
    }
}
