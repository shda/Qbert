using System;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GameScene.Gui;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.Bonus
{
    public class WindowShowCollectedCoins : MonoBehaviour
    {
        public AnimationToTimeMassive animationToTime;
        public float duration = 0.5f;
        public ResourceCounter resourceCounter;
        public Action OnClose;


        public void ShowInfo( int countGold )
        {
            resourceCounter.SetValueForce(0);
            
            gameObject.SetActive(true);

            StartCoroutine(this.WaitCoroutine(animationToTime.PlayToTime(duration),
                transform1 =>
                {
                    resourceCounter.SetValue(countGold);
                }));
        }

        public void Hide()
        {
            Debug.Log("Hide");

            StartCoroutine(this.WaitCoroutine(animationToTime.PlayToTime(duration, null, true),
                transform1 =>
                {
                    if (OnClose != null)
                    {
                        OnClose();
                    }
                    gameObject.SetActive(false);
                }));
        }

        void ShowDebug()
        {
            ShowInfo(100);
        }

        void OnEnable()
        {
        
        }

        void Start ()
        {
            gameObject.SetActive(false);
            //Invoke("Show" , 1.0f);
        }
	
        void Update ()
        {
		
        }
    }
}
