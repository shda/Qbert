using UnityEngine;
using UnityEngine.Events;

namespace Assets.Qbert.Scripts.GameScene.AnimationToTime
{
    public class AnimationToTimeFloat : ChangeValueToTime
    {
        [System.Serializable]
        public class FloatTweenCallback : UnityEvent<float>
        {
        }

        public float start;
        public float end;

        public FloatTweenCallback floatTweenCallback;

        public override void ChangeValue(float value)
        {
            if (floatTweenCallback != null)
            {
                float val = Mathf.Lerp(start, end, value);
                floatTweenCallback.Invoke(val);
            }
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
