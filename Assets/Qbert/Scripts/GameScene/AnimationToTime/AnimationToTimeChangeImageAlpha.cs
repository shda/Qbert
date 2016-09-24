using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.GameScene.AnimationToTime
{
    public class AnimationToTimeChangeImageAlpha : ChangeValueToTime
    {
        public Image image;

        public float startAlpha;
        public float endAlpha;

        public override void ChangeValue(float value)
        {
            image.color = new Color(image.color.r , image.color.g , image.color.b ,
                startAlpha + ( (endAlpha - startAlpha) * value) );
        }

        void Start () 
        {
	    
        }
	
        void Update () 
        {
	
        }
    }
}
