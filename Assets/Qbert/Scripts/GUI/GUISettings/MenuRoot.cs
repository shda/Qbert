using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.Characters;
using Assets.Qbert.Scripts.GUI.GUISettings;

public class MenuRoot : MonoBehaviour
{
    public Qbert qbert;
    public SelectCharacter selectCharacterMenu;
    public CameraMenuController cameraMenuController;

    public void OnUdateModelQbert()
    {
        qbert.UpdateModel();
    }

    public void OnFocusCameraToThis()
    {
        OnUdateModelQbert();
        cameraMenuController.OnCamaraMoveToRootMenu();
    }

    public void OnPressButtonSelectCharacter()
    {
        selectCharacterMenu.OnFocusCameraToThis();
    }

	void Start () 
	{
        cameraMenuController.SetCameraToDefaultPosition();

    }
	
	void Update () 
	{
	
	}
}
