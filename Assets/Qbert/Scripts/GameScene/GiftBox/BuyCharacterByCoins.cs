using System;
using System.Linq;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GameScene.Characters;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using Assets.Qbert.Scripts.GameScene.Gui;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.GameScene.GiftBox
{
    public class BuyCharacterByCoins : MonoBehaviour
    {
        public AnimationToTimeOneByOne animationShowCnaracter;
        public AnimationToTimeMassive  animationHide;
        public Transform characterRoot;
    
        public float localScale = 1.0f;

        public Button buttonNextChar;
        public Button buttonMenu;
        public Button buttonShare;

        public Transform buttonBuy;
        public Text countBuyNext;
        public Text characterName;

        public ResourceCounter coins;

        public Action OnEnd;

        private Transform oldCharacter;
        private GlobalConfigurationAsset globalConfigurationAsset;

        void Start ()
        {
            animationShowCnaracter.StartOneByOne();
        }

        void Update () 
        {
	
        }

        public void ConnectButtons()
        {
            DisconnectButtons();

            buttonNextChar.onClick.AddListener(() =>
            {
                DisconnectButtons();

                StartCoroutine(this.WaitCoroutine(animationHide.PlayToTime(0.5f, null, true) , transform1 =>
                {
                    OnGift(globalConfigurationAsset);
                }));
            });

            buttonMenu.onClick.AddListener(() =>
            {
                DisconnectButtons();
                StartCoroutine(this.WaitCoroutine(animationHide.PlayToTime(0.5f, null, true), transform1 =>
                {
                    animationShowCnaracter.ResetAnimations();
                    gameObject.SetActive(false);
                    if (OnEnd != null)
                    {
                        OnEnd();
                    }
                }));
            });

            /*
        buttonShare.onClick.AddListener(() =>
        {

        });
        */
        }

        public void DisconnectButtons()
        {
            buttonNextChar.onClick.RemoveAllListeners();
            buttonMenu.onClick.RemoveAllListeners();
            // buttonShare.onClick.RemoveAllListeners();
        }

        public void OnGift(GlobalConfigurationAsset config)
        {
            gameObject.SetActive(true);

            globalConfigurationAsset = config;
            var closeModel = globalConfigurationAsset.GetFirstCloseModel();

            if (closeModel != null)
            {
                SetCharacter(closeModel);

                coins.SetValueForce(GlobalValues.coins);

                GlobalValues.AddBuyModel(closeModel.codeName);
                GlobalValues.RemoveCoins(closeModel.priceCoins);
                GlobalValues.Save();

                coins.SetValue(GlobalValues.coins);

                UpdateBuyButton();

                animationShowCnaracter.StartOneByOne();
                animationShowCnaracter.OnEndAnimation = () =>
                {
                    ConnectButtons();
                };
            }
        }

        public void UpdateBuyButton()
        {
            var closeModel = globalConfigurationAsset.GetFirstCloseModel();

            if (closeModel != null && closeModel.priceCoins <= GlobalValues.coins)
            {
                buttonBuy.gameObject.SetActive(true);
                countBuyNext.text = closeModel.priceCoins.ToString();
            }
            else
            {
                buttonBuy.gameObject.SetActive(false);
            }
        }

        public void SetCharacter(QbertModel qModel)
        {
            if (oldCharacter != null)
            {
                oldCharacter.gameObject.SetActive(false);
                Destroy(oldCharacter.gameObject);
            }

            oldCharacter = Instantiate(qModel.transform);
            SetLayer(oldCharacter , gameObject.layer);
            oldCharacter.SetParent(characterRoot);
            oldCharacter.localPosition = Vector3.zero;
            oldCharacter.localRotation = Quaternion.Euler(0, 0, 0);
            oldCharacter.localScale = new Vector3(localScale, localScale, localScale);

            var model = oldCharacter.GetComponent<QbertModel>();
            characterName.text = model.nameCharacter;
            model.booldeDead.gameObject.SetActive(false);
        }

        public void SetLayer(Transform tr , int layer)
        {
            tr.Cast<Transform>().ForAll(transform1 =>
            {
                transform1.gameObject.layer = layer;
                SetLayer(transform1 , layer);
            });
        }
    }
}
