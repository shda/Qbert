using System;
using UnityEngine;

namespace Scripts.Utils
{
    public class UnscaleTimer : MonoBehaviour
    {
        private Action<UnscaleTimer> OnEndTime;
        private float time;
        private bool isRun = false;

        public static UnscaleTimer Create(float time , Action<UnscaleTimer> OnEndTime)
        {
            GameObject timer = new GameObject("UnscaleTimer_" + time);
            UnscaleTimer unscaleTimer = timer.AddComponent<UnscaleTimer>();
            unscaleTimer.StartTimer(time , OnEndTime);
            return unscaleTimer;
        }

        public void StartTimer(float time , Action<UnscaleTimer> OnEndTime)
        {
            this.time = time;
            this.OnEndTime = OnEndTime;
            isRun = true;
        }

        void Start () 
        {
	
        }
	
        void Update () 
        {
            if (isRun)
            {
                time -= Time.unscaledDeltaTime;
                if (time <= 0)
                {
                    if (OnEndTime != null)
                    {
                        OnEndTime(this);
                    }

                    isRun = false;

                    Destroy(gameObject);
                }
            }
        }
    }
}
