using UnityEngine;
using System.Collections;

namespace Scripts.GameScene.Gui
{
    public class EndGameGui : MonoBehaviour
    {
        public float timeShow = 1.0f;

        public AnimationToTimeMassive showFirstRailGui;
        public AnimationToTimeMassive hideFirstRailGui;
        public AnimationToTimeMassive showSecondRailGui;
        public AnimationToTimeMassive showSecondRailGuiButtonAnScore;

        void Start()
        {
            StartCoroutine(TestShow());
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