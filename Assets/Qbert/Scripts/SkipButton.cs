using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Qbert.Scripts
{
    public class SkipButton : MonoBehaviour 
    {
        [HideInInspector]
        public bool isLock = false;

        public AnimationToTimeChangeCanvasGroup showButtonSkip;
        public float timeDelayToSkip = 5.0f;

        public UnityEvent OnSkipPress; 

        public void OnPressSkip()
        {
            if (isLock)
                return;

            isLock = true;

            if (OnSkipPress != null)
            {
                OnSkipPress.Invoke();
            }
        }
    
        public void StartTimerShowButtonSkip()
        {
        

            showButtonSkip.gameObject.SetActive(false);

            UnscaleTimer.Create(timeDelayToSkip, timer =>
            {
                showButtonSkip.gameObject.SetActive(true);
                StartCoroutine(showButtonSkip.PlayToTime(0.5f));
            });
        }

        void Start ()
        {
            StartTimerShowButtonSkip();
        }
	
        void Update () 
        {
	
        }
    }
}
