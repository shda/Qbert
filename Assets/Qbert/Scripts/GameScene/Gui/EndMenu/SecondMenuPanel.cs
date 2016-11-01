using System;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GameScene.GiftBox;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.GameScene.Gui.EndMenu
{
    public class SecondMenuPanel : BasePanel
    {
        //Up panels
        public Transform earnPanel;
        public Transform rateAppPanel;
        public Transform giftPanel;
        public Transform timeToFreeGift;
    
        //Down panels
        public Transform needCoinsToUnlockCharacter;
        public Transform unlockCharacterPanel;

        public Text neerCountCoinsUnlockCharacter;

        public Text textTimeToGift;
        public Text lastScoreText;
        public Text bestScoreText;

        public GiftGold giftGold;

        public AnimationToTimeMassive showSecondMenu;
        public AnimationToTimeMassive hideSecondMenuWithoutBack;

        public IEnumerator AnimatedShowPanel()
        {
            yield return StartCoroutine(showSecondMenu.PlayToTime(0.5f));
        }

        public IEnumerator AnimatedHidePanel(float timeShow , Action OnEnd = null)
        {
            yield return StartCoroutine(hideSecondMenuWithoutBack.PlayToTime(timeShow, null, true));
            if (OnEnd != null)
            {
                OnEnd();
            }
        }

        public void AnimatedHidePanel(Action OnEnd = null )
        {
            StopAllCoroutines();
            StartCoroutine(AnimatedHidePanel(0.5f , OnEnd));
        }

        public void DisableAllPanels()
        {
            var array = new Transform[]
            {
                earnPanel ,
                rateAppPanel ,
                giftPanel,
                timeToFreeGift ,
                needCoinsToUnlockCharacter,
                unlockCharacterPanel
            };

            foreach (var transform1 in array)
            {
                transform1.gameObject.SetActive(false);
            }
        }

        public void OnPressUnlockCharacter()
        {
            if(isPress)
                return;

            AnimatedHidePanel();

            DisablePressButtons();
        }

        public void OnPressEarn()
        {
            if (isPress)
                return;

            AnimatedHidePanel();

            DisablePressButtons();
        }

        public void OnPressRateApp()
        {
            if (isPress)
                return;

            AnimatedHidePanel();

            DisablePressButtons();
        }

        public void OnPressGift()
        {
            if (isPress)
                return;

            AnimatedHidePanel(() =>
            {
                giftGold.ShowGift(() =>
                {
                    giftGold.gameObject.SetActive(false);
                    StartCoroutine(hideSecondMenuWithoutBack.PlayToTime(0.3f));
                    isPress = false;
                });
            });

        

            /*
        GlobalValues.timeInGame = 0;
        GlobalValues.giftTimeIndex++;
        GlobalValues.Save();
        */


            DisablePressButtons();
        }

        public void SetTimeToGiftText(string text)
        {
            textTimeToGift.text = text;
        }

        public override void UpdatePanels()
        {
            SetScores();
            UpdateUpPanel();
            UpdateDownPanel();
            base.UpdatePanels();
        }

        private void UpdateDownPanel()
        {
            if (GlobalValues.coins > GlobalValues.countCoinsToUnlockChar)
            {
                needCoinsToUnlockCharacter.gameObject.SetActive(false);
                unlockCharacterPanel.gameObject.SetActive(true);
            }
            else
            {
                neerCountCoinsUnlockCharacter.text = GlobalValues.countCoinsToUnlockChar.ToString();
                needCoinsToUnlockCharacter.gameObject.SetActive(true);
                unlockCharacterPanel.gameObject.SetActive(false);
            }
        }

        private void UpdateUpPanel()
        {
            int rand = 0; //Random.Range(0, 3);

            DisableAllPanels();

            switch (rand)
            {
                //Gift
                case 0:
                    EnableGiftPanel();
                    break;
                //Earn
                case 1:
                    EnableTObjest(earnPanel);
                    break;
                //RateApp
                case 2:
                    EnableTObjest(rateAppPanel);
                    break;
                default:
                    EnableTObjest(rateAppPanel);
                    break;
            }
        }

        private void EnableGiftPanel()
        {
            var timesNeedToGift = GlobalValues.timeInGameGift;

            var giftTimeIndex = GlobalValues.giftTimeIndex;
            giftTimeIndex = Mathf.Clamp(giftTimeIndex, 0 , timesNeedToGift.Length);

            EnableTObjest(giftPanel);

            /*

        if (timesNeedToGift[giftTimeIndex] < GlobalValues.timeInGame)
        {
            EnableTObjest(giftPanel);
        }
        else
        {
            textTimeToGift.text = timesNeedToGift[giftTimeIndex].ToString();
            EnableTObjest(timeToFreeGift);
        }
        */
        }

        private void EnableTObjest(Transform tr)
        {
            tr.gameObject.SetActive(true);
        }

        private void SetScores()
        {
            float bestScore = GlobalValues.bestScore;
            float currentScore = GlobalValues.score;

            lastScoreText.text = currentScore.ToString();
            bestScoreText.text = bestScore.ToString();
        }

        void Start ()
        {
	
        }
	
        void Update ()
        {
	
        }
    }
}
