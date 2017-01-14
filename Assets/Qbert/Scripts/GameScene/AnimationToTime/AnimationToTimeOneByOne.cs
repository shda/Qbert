using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.AnimationToTime
{
    public class AnimationToTimeOneByOne : MonoBehaviour
    {
        [System.Serializable]
        public class TimeOneByOne
        {
            public ITime animation;
            public float duration;
            public float delayBefore;
        }

        public TimeOneByOne[] animations;

        public Action OnEndAnimation;
        public Action<TimeOneByOne> OnStartPlayAnimation;
        public Action<TimeOneByOne> OnEndPlayAnimation;

        public void StartOneByOne()
        {
            if(animations == null)
                return;

            ResetAnimations();
            StartCoroutine(PlayAnimations());
        }

        public void ResetAnimations()
        {
            foreach (var animation in animations.Reverse())
            {
                if (animation == null || animation.animation == null)
                    continue;
                animation.animation.time = 0.0f;
            }
        }

        IEnumerator PlayAnimations()
        {
            yield return null;

            foreach (var animation in animations)
            {
                if (animation == null || animation.animation == null)
                    continue;

                if (animation.delayBefore > 0)
                    yield return new WaitForSeconds(animation.delayBefore);

                if (OnStartPlayAnimation != null)
                    OnStartPlayAnimation(animation);

                yield return StartCoroutine(PlayToTime(animation.animation, animation.duration));

                if (OnEndPlayAnimation != null)
                    OnEndPlayAnimation(animation);
            }

            if (OnEndAnimation != null)
            {
                OnEndAnimation();
            }
        }

        public virtual IEnumerator PlayToTime(ITime iTime, float duration,bool isReverce = false)
        {
            float t = 0;
            while (t < 1)
            {
                t += (Time.deltaTime * iTime.timeScale) / duration;
                iTime.time = t;
                yield return null;
            }

            t = 1;

            iTime.time = t;
        }

        void Start () 
        {
	
        }

        void Update () 
        {
	
        }
    }
}
