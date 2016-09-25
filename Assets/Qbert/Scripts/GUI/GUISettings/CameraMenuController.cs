using Assets.Qbert.Scripts.GameScene;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using UnityEngine;

namespace Assets.Qbert.Scripts.GUI.GUISettings
{
    public class CameraMenuController : MonoBehaviour 
    {
        public float durationMoveCameraToSettings;

        public Transform rootMainGame;
        public Transform rootSetting;
        public Transform rootRules;
        public Transform rootSelectCharecters;

        public CameraController cameraController;

        public AnimationToTimeMassive hideIfShowSettings;

        public void OnCameraMoveSelectCharacter()
        {
            HideObjects(true);
            cameraController.MoveCameraToPoint(rootSelectCharecters.position, durationMoveCameraToSettings);
        }

        public void OnCameraMoveToMenuButtons()
        {
            HideObjects(true);
            cameraController.MoveCameraToPoint(rootSetting.position, durationMoveCameraToSettings);
        }

        public void OnCameraMoveToRules()
        {
            cameraController.MoveCameraToPoint(rootRules.position, durationMoveCameraToSettings);

            rootRules.gameObject.SetActive(true);
        }

        public void OnCamaraMoveToRootMenu()
        {
            HideObjects(false);
            cameraController.MoveCameraToPoint(
                rootMainGame.position, durationMoveCameraToSettings, transform1 =>
                {

                });
        }

        public void HideObjects(bool isHide)
        {
            StopAllCoroutines();

            if (isHide)
            {
                StartCoroutine(hideIfShowSettings.PlayToTime(0.5f));
            }
            else
            {
                StartCoroutine(hideIfShowSettings.PlayToTime(0.5f, null, true));
            }
        }

        void Start () 
        {
	
        }
	
        void Update () 
        {
	
        }
    }
}
