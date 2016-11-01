using System.Collections;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GameScene.Gui.EndMenu;
using Assets.Qbert.Scripts.GameScene.Levels;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Gui
{
    public class EndGameGuiAnimator : MonoBehaviour
    {
        public float timeShow = 1.0f;

        public AnimationToTimeMassive hideFirstMenuWitchoutToBackround;
        
        public Transform score;
        public Transform level;

        public MapField mapField;
        public LevelController levelController;
        public Transform inputController;
        public Transform pauseButton;

        [Header("Panels")]
        public FirstMenuPanel firstPanel;
        public SecondMenuPanel secondPanel;

        public void AddLiveReturnGame()
        {
            levelController.AddLiveReturnGame();
        }

        void Start()
        {
            UpdatePanels();
        }

        public void UpdatePanels()
        {
            firstPanel.UpdatePanels();
            secondPanel.UpdatePanels();
        }

        public void ShowGameOver()
        {
            UpdatePanels();
            inputController.gameObject.SetActive(false);

            pauseButton.gameObject.SetActive(false);
            StartCoroutine(firstPanel.AnimatedShowPanel());
        }

        public void ReturnToGame()
        {
            pauseButton.gameObject.SetActive(true);
            //showFirstMenu.gameObject.SetActive(true);
            inputController.gameObject.SetActive(true);

            firstPanel.StartCoroutine(firstPanel.AnimatedHidePanel(timeShow));
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

            //showSecondMenu.gameObject.SetActive(true);
            yield return secondPanel.StartCoroutine(secondPanel.AnimatedShowPanel());
            mapField.gameObject.SetActive(false);
        }

        private IEnumerator HideFirtRailsAndShowSecond()
        {
            hideFirstMenuWitchoutToBackround.gameObject.SetActive(true);
           // showSecondMenu.gameObject.SetActive(true);
            score.gameObject.SetActive(false);
            level.gameObject.SetActive(false);

            yield return StartCoroutine(hideFirstMenuWitchoutToBackround.PlayToTime(timeShow, null, true));
            yield return secondPanel.StartCoroutine(secondPanel.AnimatedShowPanel());

            mapField.gameObject.SetActive(false);
        }

        void Update()
        {

        }

    }
}