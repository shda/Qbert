using System.Collections;
using UnityEngine;

namespace Scripts.GameScene
{
    public class AnimationToTime : ChangeValueToTime
    {
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

        void Start () 
        {
        
        }
    }
}
