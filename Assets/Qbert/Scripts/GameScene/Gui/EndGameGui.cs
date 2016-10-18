using System.Collections;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GameScene.Levels;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Gui
{
    public class EndGameGui : MonoBehaviour
    {
        public float timeShow = 1.0f;

        public AnimationToTimeMassive showFirstMenu;
        public AnimationToTimeMassive hideFirstMenuWitchoutToBackround;
        public AnimationToTimeMassive showSecondMenu;


        public Transform score;
        public Transform level;

        public MapField mapField;
        public LevelController levelController;
        public Transform pauseButton;

        public EndPanelsLogic endPanelsLogic;

        public void AddLiveReturnGame()
        {
            levelController.AddLiveReturnGame();
        }

        void Start()
        {
            // firstMenuRoot.gameObject.SetActive(false);
            // secondMenuRoot.gameObject.SetActive(false);

            endPanelsLogic.UpdatePanels();
        }

        public void ShowGameOver()
        {
            pauseButton.gameObject.SetActive(false);
            showFirstMenu.gameObject.SetActive(true);
            StartCoroutine(showFirstMenu.PlayToTime(0.5f));
        }

        public void ReturnToGame()
        {
            pauseButton.gameObject.SetActive(true);
            showFirstMenu.gameObject.SetActive(true);
            StartCoroutine(showFirstMenu.PlayToTime(timeShow , null , true));
            AddLiveReturnGame();
        }


        public void OnPressEndGameFirstRails()
        {
            pauseButton.gameObject.SetActive(true);
            StartCoroutine(HideFirtRailsAndShowSecond());
        }

        public void OnShowSecondRails()
        {
            pauseButton.gameObject.SetActive(false);
            StartCoroutine(ShowSecondRails());
        }

        public IEnumerator ShowSecondRails()
        {
            pauseButton.gameObject.SetActive(false);
            score.gameObject.SetActive(false);
            level.gameObject.SetActive(false);

            showSecondMenu.gameObject.SetActive(true);
            yield return StartCoroutine(showSecondMenu.PlayToTime(timeShow ));
            mapField.gameObject.SetActive(false);
        }

        private IEnumerator HideFirtRailsAndShowSecond()
        {
            hideFirstMenuWitchoutToBackround.gameObject.SetActive(true);
            showSecondMenu.gameObject.SetActive(true);
            score.gameObject.SetActive(false);
            level.gameObject.SetActive(false);

            yield return StartCoroutine(hideFirstMenuWitchoutToBackround.PlayToTime(timeShow, null, true));
            yield return StartCoroutine(showSecondMenu.PlayToTime(timeShow));

            mapField.gameObject.SetActive(false);
        }

        void Update()
        {

        }

    }
}