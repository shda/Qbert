using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Qbert.Scripts.GameScene.AD;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using Assets.Qbert.Scripts.GameScene.GiftBox;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.GameScene.Gui.EndMenu
{
    public class SecondMenuPanel : BasePanel
    {
        public GlobalConfigurationAsset globalConfigurationAsset;

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

        public ResourceCounter coinsCounter;

        public AnimationToTimeMassive showSecondMenu;
        public AnimationToTimeMassive hideSecondMenuWithoutBack;

        public BuyCharacterByCoins giftCharacter;

        public VideoAD videoAd;

        private bool isEarnDisable;

        public IEnumerator AnimatedShowPanel()
        {
            UpdatePanels();
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

            AnimatedHidePanel(() =>
            {
                giftCharacter.OnGift(globalConfigurationAsset);
                giftCharacter.OnEnd = () =>
                {
                    isPress = false;
                    UpdatePanels();
                    StartCoroutine(AnimatedShowPanel());
                };
            });
            DisablePressButtons();
        }

        public void OnPressEarn()
        {
            if (isPress)
                return;

            DisablePressButtons();

            AnimatedHidePanel(() =>
            {
                videoAd.ShowAD(isOk =>
                {
                    if (isOk)
                    {
                        coinsCounter.SetValue(GlobalValues.AddEarnVideo());
                    }
                    isEarnDisable = true;
                    isPress = false;
                    UpdatePanels();
                    StartCoroutine(AnimatedShowPanel());
                });
            });
        }

        public void OnPressRateApp()
        {
            if (isPress)
                return;

            AnimatedHidePanel(() =>
            {
                GlobalValues.appIsRate = true;
                GlobalValues.Save();

                UpdatePanels();
                
                isPress = false;
                UpdateDownPanel();
                StartCoroutine(AnimatedShowPanel());
            });


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
                    coinsCounter.SetValueForce(GlobalValues.coins);
                    UpdatePanels();
                    giftGold.gameObject.SetActive(false);
                    StartCoroutine(hideSecondMenuWithoutBack.PlayToTime(0.3f));
                    isPress = false;
                });
            });

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
            var closeModel = globalConfigurationAsset.GetFirstCloseModel();

            if (closeModel != null)
            {
                if (GlobalValues.coins >= closeModel.priceCoins)
                {
                    needCoinsToUnlockCharacter.gameObject.SetActive(false);
                    unlockCharacterPanel.gameObject.SetActive(true);
                }
                else
                {
                    neerCountCoinsUnlockCharacter.text = closeModel.priceCoins.ToString();
                    needCoinsToUnlockCharacter.gameObject.SetActive(true);
                    unlockCharacterPanel.gameObject.SetActive(false);
                }
            }
            else
            {
                needCoinsToUnlockCharacter.gameObject.SetActive(false);
                unlockCharacterPanel.gameObject.SetActive(false);
            }
        }

        private void UpdateUpPanel()
        {
            DisableAllPanels();

            List<Action> selector = new List<Action>();

            //App rate
            if (!GlobalValues.appIsRate)
            {
                selector.Add(() =>
                {
                    EnableTObjest(rateAppPanel);
                });
            }

            //Earn video
            if (!isEarnDisable)
            {
                selector.Add(() =>
                {
                    EnableTObjest(earnPanel);
                });
            }

            //Gift
            selector.Add(() =>
            {
                EnableGiftPanel();
            });
            //selector[2].Invoke();

            if (selector.Count > 0)
            {
                selector[UnityEngine.Random.Range(0, selector.Count)].Invoke();
            }
        }

        private void EnableGiftPanel()
        {
            var timesNeedToGift = GlobalValues.timeInGameGift;
            var giftTimeIndex = GlobalValues.giftTimeIndex;

            giftTimeIndex = Mathf.Clamp(giftTimeIndex, 0 , timesNeedToGift.Length - 1);

            //EnableTObjest(giftPanel);

            float minutes = GlobalValues.timeInGameSecond/60.0f;

            if (timesNeedToGift[giftTimeIndex] < minutes)
            {
                EnableTObjest(giftPanel);
            }
            else
            {
                int minutesToGift = (int) (timesNeedToGift[giftTimeIndex] - minutes);
                minutesToGift = minutesToGift <= 0 ? 1 : minutesToGift;
                textTimeToGift.text = GlobalValues.ConvertMinutesToString(minutesToGift);
                EnableTObjest(timeToFreeGift);
            }
        }

        private void EnableTObjest(Transform tr)
        {
            tr.gameObject.SetActive(true);
        }

        private void SetScores()
        {
            float bestScore = GlobalValues.bestScore;
            float currentScore = GlobalValues.score;

            lastScoreText.text = ((int)currentScore).ToString();
            bestScoreText.text = ((int)bestScore).ToString();
        }

        void Start ()
        {
	
        }
	
        void Update ()
        {
	
        }
    }
}
