using UnityEngine;

namespace Assets.Qbert.Scripts.GUI.GUISettings
{
    public class GuiMainMenu : MonoBehaviour
    {
        public GameScene.Characters.Qbert qbert;
        public GuiSelectCharacter selectCharacterMenu;
        public CameraMenuController cameraMenuController;

        public void OnFocusCameraToThis()
        {
            OnUdateModelQbert();
            cameraMenuController.OnCamaraMoveToRootMenu();
        }

        public void OnUdateModelQbert()
        {
            qbert.UpdateModel();
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
}
