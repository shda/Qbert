using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public Image frontImage;
    public Action<Transform> OnEnd;

    public void Show(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(this.ChangeColorImage(frontImage, new Color(0, 0, 0, 1), duration , OnEnd));
    }

    public void Hide(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(this.ChangeColorImage(frontImage, new Color(0, 0, 0, 0), duration , OnEnd));
    }

	void Start ()
	{
	   // Hide(3.0f);
	}
	
	void Update () 
	{
	
	}
}
