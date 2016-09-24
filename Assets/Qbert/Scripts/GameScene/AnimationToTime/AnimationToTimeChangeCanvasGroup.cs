using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.AnimationToTime
{
    public class AnimationToTimeChangeCanvasGroup : ChangeValueToTime
    {
        public CanvasGroup canvasGroup;

        public float startAlpha;
        public float endAlpha;

        public override void ChangeValue(float value)
        {
            canvasGroup.alpha = startAlpha + ((endAlpha - startAlpha)*value);
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}

