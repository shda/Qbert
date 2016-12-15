using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.GameScene
{
    public class BonusTimer : MonoBehaviour
    {
        public ITimeScale iTimeScaler;
        protected float timeScale
        {
            get
            {
                if (iTimeScaler == null)
                {
                    return 1.0f;
                }

                return iTimeScaler.timeScale;
            }
        }

        public Text textBonus;
        public Action OnEndTimer;
        public bool isPause { get; private set; }
        public float time { get; private set; }

        public void SetTimer(int countTime)
        {
            time = countTime;
        }

        public void StartTimer()
        {
            UpdateLabel();
            isPause = false;
        }

        public void Pause()
        {
            isPause = true;
        }

        public void Resume()
        {
            isPause = false;
        }

        private void UpdateLabel()
        {
            textBonus.text = string.Format("{0}", (int)time);
        }

        void Start ()
        {
            isPause = true;
        }
	
        void Update ()
        {
            if (!isPause)
            {
                time -= Time.deltaTime * timeScale;
            
                if (time <= 0)
                {
                    if (OnEndTimer != null)
                    {
                        OnEndTimer();
                    }

                    time = 0;
                    isPause = true;
                }

                UpdateLabel();
            }
        }
    }
}
