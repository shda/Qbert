using Assets.Qbert.Scripts.GameScene.Gui;
using UnityEngine;

namespace Assets.Qbert.Scripts.GUI.GUISettings
{
    public class GuiMainMenu : MonoBehaviour
    {
        public GameScene.Characters.Qbert qbert;
        public GuiSelectCharacter selectCharacterMenu;
        public CameraMenuController cameraMenuController;
        public GuiBuyCoins buyCoins;

        public ResourceCounter coins;

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

        public void OnPressButtonCoinsBuy()
        {
            buyCoins.OnFocusCameraToThis();
        }

        void Start () 
        {
            cameraMenuController.SetCameraToDefaultPosition();
            coins.SetValueForce(GlobalValues.coins);
        }
	
        void Update () 
        {
	
        }
    }
}
