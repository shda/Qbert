namespace Assets.Qbert.Scripts.GameScene.AnimationToTime
{
    public class AnimationToTimeMassive : ChangeValueToTime
    {
        public ITime[] anumations;

        public override void ChangeValue(float value)
        {
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

        }
    }
}


