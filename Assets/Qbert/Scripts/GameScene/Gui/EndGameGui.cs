using UnityEngine;
using System.Collections;
using Scripts.GameScene.Levels;
using Scripts.Utils;

namespace Scripts.GameScene.Gui
{
    public class EndGameGui : MonoBehaviour
    {
        public float timeShow = 1.0f;

        public AnimationToTimeMassive showFirstRailGui;
        public AnimationToTimeMassive hideFirstRailGui;
        public AnimationToTimeMassive showSecondRailGui;
        public AnimationToTimeMassive showSecondRailGuiButtonAnScore;

        public AnimationToTimeMassive showSecondRailGuiAndHideBack;
        public MapField mapField;
        public LevelController levelController;
        public Transform pauseButton;

        public void AddLiveReturnGame()
        {
            levelController.AddLiveReturnGame();
        }

        void Start()
        {

        }

        public void ShowGameOver()
        {
            pauseButton.gameObject.SetActive(false);
            StartCoroutine(showFirstRailGui.PlayToTime(1.0f));
        }

        public void ReturnToGame()
        {
            pauseButton.gameObject.SetActive(true);
            StartCoroutine(showFirstRailGui.PlayToTime(timeShow , null , true));
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
            yield return StartCoroutine(showSecondRailGuiAndHideBack.PlayToTime(timeShow));
            mapField.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(showSecondRailGuiButtonAnScore.PlayToTime(timeShow));
        }

        private IEnumerator HideFirtRailsAndShowSecond()
        {
            yield return StartCoroutine(hideFirstRailGui.PlayToTime(timeShow));
            yield return StartCoroutine(showSecondRailGui.PlayToTime(timeShow));

            mapField.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(showSecondRailGuiButtonAnScore.PlayToTime(timeShow));
        }

        void Update()
        {

        }

    }
}