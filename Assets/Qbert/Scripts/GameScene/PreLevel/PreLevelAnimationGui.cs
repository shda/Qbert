using System;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.GameScene.PreLevel
{
    public class PreLevelAnimationGui : MonoBehaviour
    {
        public AnimationToTimeOneByOne animationToTimeOneByOne;
        public AnimationToTimeOneByOne animationShowBonusLevel;
        public AnimationToTimeOneByOne animationTimerOneByOne;

        public Text textRoundNumber;

        public void StartShowBonus(Action OnEndAnimation)
        {
            textRoundNumber.text = "BONUS LEVEL";

            animationShowBonusLevel.StartOneByOne();
            animationShowBonusLevel.OnEndAnimation = () =>
            {
                animationTimerOneByOne.OnEndAnimation = OnEndAnimation;
                animationTimerOneByOne.StartOneByOne();
            };
        }

        public void StartShowRound(int round , Action OnEndAnimation)
        {
            textRoundNumber.text = "ROUND " + round;

            animationToTimeOneByOne.StartOneByOne();
            animationToTimeOneByOne.OnEndAnimation = OnEndAnimation;
        }

        void Start () 
        {
	
        }

        void Update () 
        {
	
        }
    }
}
