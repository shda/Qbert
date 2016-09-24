using Scripts.GameScene;
using Scripts.GUI.Button;
using Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GUI.GUISettings
{
    public class GuiSettings : MonoBehaviour
    {
        public float durationMoveCameraToSettings;
        public Transform rootMainGame;
        public Transform rootSetting;

        public Transform rootRules;

        public CameraController cameraController;

        public GuiHandle guiHandle;
        public LoadScene.SelectSceneLoader selectSceneLoader;
        public FadeScreen fadeScreen;

        public AnimationToTimeMassive hideIfShowSettings;


        public void StartGame()
        {
            UnityEngine.Debug.Log("StartGame");

            guiHandle.enabled = false;


            fadeScreen.OnEnd = transform1 =>
            {
                GlobalSettings.currentLevel = 0;
                GlobalSettings.currentRound = 0;

                selectSceneLoader.OnLoadScene();
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

            if (isHide)
            {
                StartCoroutine(hideIfShowSettings.PlayToTime(0.5f));
            }
            else
            {
                StartCoroutine(hideIfShowSettings.PlayToTime(0.5f , null, true));
            }
        }
    }
}
