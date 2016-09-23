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

        public LevelController levelController;

        public void AddLiveReturnGame()
        {
            levelController.AddLiveReturnGame();
        }

        void Start()
        {
            //StartCoroutine(TestShow());
        }

        public void ReturnToGame()
        {
            showFirstRailGui.isReverce = true;
            StartCoroutine(showFirstRailGui.PlayToTime(timeShow));

            AddLiveReturnGame();
        }


        public void OnPressEndGameFirstRails()
        {
            StartCoroutine(HideFirtRailsAndShowSecond());
        }

        private IEnumerator HideFirtRailsAndShowSecond()
        {
            showFirstRailGui.isReverce = false;
            yield return StartCoroutine(hideFirstRailGui.PlayToTime(timeShow));
            yield return StartCoroutine(showSecondRailGui.PlayToTime(timeShow));
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(showSecondRailGuiButtonAnScore.PlayToTime(timeShow));
        }

        IEnumerator TestShow()
        {
            yield return new WaitForSeconds(1.0f);
            yield return StartCoroutine(showFirstRailGui.PlayToTime(timeShow));
            yield return new WaitForSeconds(5.0f);
            yield return StartCoroutine(hideFirstRailGui.PlayToTime(timeShow));
            //yield return new WaitForSeconds(2.0f);
            yield return StartCoroutine(showSecondRailGui.PlayToTime(timeShow));
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(showSecondRailGuiButtonAnScore.PlayToTime(timeShow));
        }

        void Update()
        {

        }
    }
}