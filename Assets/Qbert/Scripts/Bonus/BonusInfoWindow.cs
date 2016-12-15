using System;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.Bonus
{
    public class BonusInfoWindow : MonoBehaviour
    {
        public AnimationToTimeMassive animationToTime;
        public float duration = 0.5f;
        public Action OnClose;

        public void ShowInfo()
        {
            gameObject.SetActive(true);
            StartCoroutine(animationToTime.PlayToTime(duration));
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

        void Show()
        {
            ShowInfo();
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
