using UnityEngine;
using System.Collections;
using Scripts.GameScene;

namespace Scripts.GameScene
{
    public class AnimationToTimeMassive : ChangeValueToTime
    {
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

        void Start()
        {
            //StartCoroutine(PlayToTime(6.0f));
        }
    }
}


