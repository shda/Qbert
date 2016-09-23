using UnityEngine;
using System.Collections;
using Scripts.GameScene;

namespace Scripts.GameScene
{
    public class AnimationToTimeMassive : ITime, ITimeScale
    {
        //ITimeScale
        private float _timeScale = 1.0f;
        public float timeScale
        {
            get { return _timeScale; }
            set { _timeScale = value; }
        }
        //end ITimeScale

        public ITime[] anumations;

        public override void ChangeValue(float value)
        {
           // Debug.Log(value);
            if (anumations != null)
            {
                foreach (var animationToTime in anumations)
                {
                    animationToTime.time = value;
                }
            }
        }

        public IEnumerator PlayToTime(float duration, ITimeScale ITimeScale = null)
        {
            time = 0;

            ITimeScale iTimeCurrent = ITimeScale ?? this;
            float t = 0;
            while (t < 1)
            {
                t += (Time.deltaTime * iTimeCurrent.timeScale) / duration;
                time = t;
                yield return null;
            }

            t = 1;

            time = t;
        }

        void Start()
        {
            //StartCoroutine(PlayToTime(6.0f));
        }

        public bool isEnable = false;
        public float value = 0;

        void Update()
        {
            if (isEnable)
            {
                time = value;
            }
        }
    }
}


