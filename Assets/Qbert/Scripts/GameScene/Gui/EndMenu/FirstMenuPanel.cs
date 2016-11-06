using System;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.AD;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GameScene.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.GameScene.Gui.EndMenu
{
    public class FirstMenuPanel : BasePanel
    {
        public Transform watchwVideoToContinue;
        public Transform watchwVideoToContinueDisable;

        public Text textCountCoinsToContinueGame;

        public VideoAD videoAd;

        public LevelController levelController;
        public Transform inputController;
        public ResourceCounter coinsCounter;

        public bool isShowBuyPanel { get; private set; }

        private bool isWatchAdPress = false;
        private int  counterInvestPress = 0;

        public AnimationToTimeMassive showFirstMenu;
        public AnimationToTimeMassive showBuyPanel;

        public IEnumerator AnimatedShowPanel()
        {
            EnablePressButtons();
            UpdateIvestLabel();
            yield return StartCoroutine(showFirstMenu.PlayToTime(0.5f));
        }

        public IEnumerator AnimatedHidePanel(float timeShow , Action OnHidePanel = null)
        {
            yield return StartCoroutine(showFirstMenu.PlayToTime(timeShow, null, true));
            if (OnHidePanel != null)
            {
                OnHidePanel();
            }
        }

        public void OnPressButtonWatchVideo()
        {
            if(isPress)
                return;

            Debug.Log("OnPressButtonWatchVideo");

            isWatchAdPress = true;
            UpdatePanels();
            DisablePressButtons();

            videoAd.ShowAD(isOk =>
            {
                if (isOk)
                {
                    StartCoroutine(AnimatedHidePanel(0.5f , () =>
                    {
                        HidePanelAndReturnToGame();
                    }));
                }
            });
        }

        private void HidePanelAndReturnToGame()
        {
            GlobalValues.ReturnQbertLives();
            inputController.gameObject.SetActive(true);
            levelController.ReturnQbertToPosution();
        }

        private void UpdateIvestLabel()
        {
            textCountCoinsToContinueGame.text = string.Format("X{0}", CountNeedInvestCoins());
        }

        private int CountNeedInvestCoins()
        {
            var cast = GlobalValues.castCoinsInvest;
            counterInvestPress = Mathf.Clamp(counterInvestPress, 0, cast.Length - 1);
            return cast[counterInvestPress];
        }

        public void OnPressButtonInvestToContinueGame()
        {
            Debug.Log("Invest");

            DisablePressButtons();

            if (GlobalValues.coins >= CountNeedInvestCoins())
            {
                GlobalValues.coins -= CountNeedInvestCoins();
                GlobalValues.Save();

                coinsCounter.SetValue(GlobalValues.coins);
                counterInvestPress++;

                StartCoroutine(AnimatedHidePanel(0.5f, () =>
                {
                    HidePanelAndReturnToGame();
                })); 
            }
            else
            {
                ShowBuyPanel();
            }
        }

        public void HideBuyPanel()
        {
            StopAllCoroutines();
            isShowBuyPanel = false;
            StartCoroutine(showBuyPanel.PlayToTime(0.4f , null , true));
        }

        public void ShowBuyPanel()
        {
            StopAllCoroutines();
            isShowBuyPanel = true;
            StartCoroutine(showBuyPanel.PlayToTime(0.4f));
        }

        public override void UpdatePanels()
        {
            ShowWatchVideoEnable(false);
            ShowWatchVideoEnable(!isWatchAdPress);
            UpdateIvestLabel();

            base.UpdatePanels();
        }

        private void ShowWatchVideoEnable(bool enable)
        {
            watchwVideoToContinue.gameObject.SetActive(enable);
            watchwVideoToContinueDisable.gameObject.SetActive(!enable);
        }

        void Start ()
        {
            
        }
	
        void Update ()
        {
	
        }
    }
}
