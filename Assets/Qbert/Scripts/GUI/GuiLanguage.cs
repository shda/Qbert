using System.Collections;
using System.Collections.Generic;
using Assets.Qbert.Scripts.GUI.GUISettings;
using UnityEngine;

public class GuiLanguage : MonoBehaviour
{
    public CameraMenuController cameraMenuController;

    public void OnFocusCameraToThis()
    {
        cameraMenuController.OnCameraToLanguage();
    }

    public void OnCloseButtonPress()
    {
        cameraMenuController.OnCameraMoveToMenuButtons();
    }
}
