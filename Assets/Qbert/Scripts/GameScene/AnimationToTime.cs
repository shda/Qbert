using System.Collections;
using UnityEngine;

namespace Scripts.GameScene
{
    public class AnimationToTime : ITime, ITimeScale
    {
        //ITimeScale
        private float _timeScale = 1.0f;
        public float timeScale
        {
            get { return _timeScale; }
            set { _timeScale = value; }
        }
        //end ITimeScale

        public string animationName;
        public Animator animator;

        private int heshAnimationName;

        public override void ChangeValue(float value)
        {
            if (animator != null && animator.gameObject.activeSelf)
            {
                animator.Play(heshAnimationName, 0, value);
                animator.Update(0);
            }
        }

        void Awake()
        {
            heshAnimationName = Animator.StringToHash(animationName);
            animator.speed = 0;
        }

        public IEnumerator PlayToTime(float duration , ITimeScale ITimeScale = null)
        {
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

        void Start () 
        {
        
        }

        public bool isEnable = false;
        public float value = 0;

        void Update () 
        {
            if (isEnable)
            {
                time = value;
            }
        }
    }
}
