using System.Collections;
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
        public Transform rootCoinsBuy;

        public CameraController cameraController;

        public AnimationToTimeMassive hideAll;
        public AnimationToTimeMassive hideAllWitchoutCoins;

        public AnimationToTimeChangeImageAlpha animationToTime;

        public void OnCameraMoveCoinsBuy()
        {
            HideAllWitchoutCoins(true);
            cameraController.MoveCameraToPoint(rootCoinsBuy.position, durationMoveCameraToSettings);
        }

        public void OnCameraMoveSelectCharacter()
        {
            HideAllObjects(true);
            cameraController.MoveCameraToPoint(rootSelectCharecters.position, durationMoveCameraToSettings);
        }

        public void OnCameraMoveToMenuButtons()
        {
            HideAllObjects(true);
            cameraController.MoveCameraToPoint(rootSetting.position, durationMoveCameraToSettings);
        }

        public void OnCameraMoveToRules()
        {
            cameraController.MoveCameraToPoint(rootRules.position, durationMoveCameraToSettings);
            rootRules.gameObject.SetActive(true);
        }

        public void OnCamaraMoveToRootMenu()
        {
            HideAllObjects(false);
            cameraController.MoveCameraToPoint(rootMainGame.position, durationMoveCameraToSettings);
        }

        public void SetCameraToDefaultPosition()
        {
            cameraController.MoveCameraToPoint(rootMainGame.position, 0);
        }

        public void HideAllWitchoutCoins(bool isHide)
        {
            StopAllCoroutines();

            StartCoroutine(ShowLogo(isHide));

            if (isHide)
            {
                StartCoroutine(hideAllWitchoutCoins.PlayToTime(0.5f));
            }
            else
            {
                StartCoroutine(hideAllWitchoutCoins.PlayToTime(0.5f, null, true));
            }
        }

        public void HideAllObjects(bool isHide)
        {
            StopAllCoroutines();

            StartCoroutine(ShowLogo(isHide));

            if (isHide)
            {
                StartCoroutine(hideAll.PlayToTime(0.5f));
            }
            else
            {
                StartCoroutine(hideAll.PlayToTime(0.5f, null, true));
            }
        }

        public IEnumerator ShowLogo(bool isShow)
        {
            if (!isShow)
            {
                yield return new WaitForSeconds(0.3f);
                StartCoroutine(animationToTime.PlayToTime(0.6f , null , true));
            }
            else
            {
                animationToTime.isReverce = false;
                animationToTime.time = 1.0f;
            }

            yield return null;
        }

        void Start () 
        {
	
        }
	
        void Update () 
        {
	
        }
    }
}
