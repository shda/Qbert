using Assets.Qbert.Scripts.GameScene.AD;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Gui.EndMenu
{
    public class WindowBuyCoins : BasePanel
    {
        public VideoAD videoAd;
        public Transform imageCapAdVideoFree;
        public Transform imageCapAdVideoFreeOff;

        public ResourceCounter coins;

        public const int countCapCoins = 250;
        public const int countBagCoins = 1000;
        public const int countBoxCoins = 3000;
        public const int countBathCoins = 6000;


        public void Init()
        {
            EnablePressButtons();

            bool isShowCap = !GlobalValues.isCointsByWatchAdIsBeenViewed;

            imageCapAdVideoFree.gameObject.SetActive(isShowCap);
            imageCapAdVideoFreeOff.gameObject.SetActive(!isShowCap);
        }


        private void AddCoins(int coints)
        {
            coins.SetValue(GlobalValues.AddCoins(coints));
            Init();
        }

        public void OnPressButtonCap()
        {
            bool isShowCap = !GlobalValues.isCointsByWatchAdIsBeenViewed;
            if (isShowCap)
            {
                videoAd.ShowAD(isOk =>
                {
                    GlobalValues.isCointsByWatchAdIsBeenViewed = true;
                    GlobalValues.Save();
                    AddCoins(countCapCoins);
                });
            }
        }

        public void OnPressButtonBag()
        {
            AddCoins(countBagCoins);
        }

        public void OnPressButtonBox()
        {
            AddCoins(countBoxCoins);
        }

        public void OnPressButtonBath()
        {
            AddCoins(countBathCoins);
        }

        void Start ()
        {
            Init();
        }

        void Update () 
        {
	
        }
    }
}
