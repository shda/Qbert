using System.Collections;
using System.Collections.Generic;
using Assets.Qbert.Scripts.GUI.GUISettings;
using UnityEngine;

public class GuiAboutUs : MonoBehaviour
{
    public CameraMenuController cameraMenuController;

    public void OnFocusCameraToThis()
    {
        cameraMenuController.OnCameraMoveToAboutUs();
    }

    public void OnCloseButtonPress()
    {
        cameraMenuController.OnCameraMoveToMenuButtons();
    }
}
