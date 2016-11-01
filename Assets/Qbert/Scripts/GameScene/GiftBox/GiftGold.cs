using System;
using Assets.Qbert.Scripts.GameScene.Gui;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.GameScene.GiftBox
{
    public class GiftGold : MonoBehaviour
    {
        public GiftGoldAnimator giftGoldAnimator;

        public ResourceCounter currentCoinsCount;
        public ResourceCounter addGoldCoinsCount;
        public Text nextGiftTime;

        public Action OnEndGiftAction;

        public void ShowGift(Action OnEndGiftAction)
        {
            this.OnEndGiftAction = OnEndGiftAction;
            gameObject.SetActive(true);

            giftGoldAnimator.OnEndGift = OnEndGift;
            giftGoldAnimator.OnPressVideoToGift = OnPressVideoToGift;

            currentCoinsCount.SetValueForce(GlobalValues.coins);
            addGoldCoinsCount.SetValueForce(GlobalValues.countGoldToGift);

            GlobalValues.AddGiftGold();
            UpdateNextTimeToGift();

            if (!GlobalValues.isShowGiftDoubleFromVideo)
            {
                GlobalValues.isShowGiftDoubleFromVideo = true;
                GlobalValues.Save();
                giftGoldAnimator.showDoubleGift = true;
            }
            else
            {
                GlobalValues.isShowGiftDoubleFromVideo = false;
                GlobalValues.Save();
            }

            giftGoldAnimator.GiftDropToGround();
        }

        private void OnPressVideoToGift(bool isOkVideo)
        {
            if (isOkVideo)
            {
                giftGoldAnimator.showDoubleGift = false;
                currentCoinsCount.SetValueForce(GlobalValues.coins);
                addGoldCoinsCount.SetValueForce(GlobalValues.countGoldToGift);
                giftGoldAnimator.GiftDropToGround();

                GlobalValues.AddGiftDoubleByWatchVideo();
            }
            else
            {
                OnEndGiftAction();
            }
        }

        public void UpdateNextTimeToGift()
        {
            nextGiftTime.text = string.Format("{0}h",
                GlobalValues.GetNextTimeGift());
        }

        private void OnEndGift()
        {
            if (OnEndGiftAction != null)
            {
                OnEndGiftAction();
            }

            //Invoke("ShowGift", 2.0f);
        }

        void Start () 
        {
            //Invoke("ShowGift" , 3.0f);
        }

        void Update () 
        {
	
        }
    }
}
