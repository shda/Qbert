using UnityEngine;
using System.Collections;
using Scripts.GameScene;
using UnityEngine.UI;

public class AnimationToTimeChangeCanvasGroup : ITime
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

