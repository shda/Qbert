using System;
using UnityEngine;
using System.Collections;
using Scripts.Utils;
using UnityEngine.UI;

namespace Scripts.GameScene.Gui
{
    public class MenuPause : MonoBehaviour
    {
        public int countTimerCountdown = 3;
        public float delayShowHideBackground = 0.3f;

        public Text textCountdownTimer;
        public Transform backgroundRoot;
        public Transform rootPauseAndButtonComplite;

        public AnimationToTimeMassive animationBackground;

        public Action OnCompliteGame;
        public Action OnResumeGame;


        private bool isLock = true;

        private void ResumeGame()
        {
            BackgroundShow(() =>
            {
                if (OnResumeGame != null)
                {
                    OnResumeGame();
                }
            }, true);

            HideAll();
        }

        public void Show()
        {
            isLock = false;

            backgroundRoot.gameObject.SetActive(true);

            BackgroundShow(() =>
            {
                textCountdownTimer.gameObject.SetActive(false);
                rootPauseAndButtonComplite.gameObject.SetActive(true);
            });
        }

        public void HideAll()
        {
            textCountdownTimer.gameObject.SetActive(false);
            rootPauseAndButtonComplite.gameObject.SetActive(false);
            backgroundRoot.gameObject.SetActive(false);
        }

        private void BackgroundShow(Action OnEnd , bool isRevers = false)
        {
            StartCoroutine(this.WaitCoroutine(animationBackground.PlayToTime(delayShowHideBackground, 
                null , isRevers), transform1 =>
            {
                OnEnd();
            }));
        }

        public void OnPressScreenButton()
        {
            if(isLock)
                return;

            isLock = true;

            rootPauseAndButtonComplite.gameObject.SetActive(false);
            textCountdownTimer.gameObject.SetActive(true);

            StartCoroutine(TimerCountdown(() =>
            {
                ResumeGame();
            }));
        }

        public void OnPressCompliteButton()
        {
            if (isLock)
                return;

            isLock = true;

            BackgroundShow(() =>
            {
                if (OnCompliteGame != null)
                {
                    OnCompliteGame();
                }
            }, true);

            HideAll();
        }


        IEnumerator TimerCountdown(Action OnEndTimer)
        {
            int count = countTimerCountdown;

            while (count > 0)
            {
                textCountdownTimer.text = count.ToString();
                yield return new WaitForSeconds(1.0f);
                count--;
            }

            if (OnEndTimer != null)
            {
                OnEndTimer();
            }
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
