using System.Collections;
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

        public void StartOneByOne()
        {
            if(animations == null)
                return;

            StartCoroutine(PlayAnimations());
        }

        IEnumerator PlayAnimations()
        {
            yield return null;

            foreach (var animation in animations)
            {
                if(animation.delayBefore > 0)
                    yield return new WaitForSeconds(animation.delayBefore);

                yield return StartCoroutine(PlayToTime(animation.animation, animation.duration));
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
