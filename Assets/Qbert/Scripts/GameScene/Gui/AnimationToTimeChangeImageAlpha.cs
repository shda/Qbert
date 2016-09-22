using UnityEngine;
using System.Collections;
using Scripts.GameScene;
using Scripts.Utils;
using UnityEngine.UI;

public class AnimationToTimeChangeImageAlpha : ITime
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
