using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.Utils;
using UnityEngine.UI;

public class FlashSwitchBackground : FlashColorBase
{
    public Image imageFlash;

    public Material normalMaterila;
    public Material flashMaterial;

    void Awake()
    {
        imageFlash.material = normalMaterila;
       // StartCoroutine(Flash(0.3f , 0.3f));
    }

    public override void SetNormal()
    {
        imageFlash.material = normalMaterila;
    }

    public override void SetFlash()
    {
        imageFlash.material = flashMaterial;
    }

    public override void StartFlash(float durationFlash , float flashPeriod)
    {
        StopFlash();
        StartCoroutine(Flash(durationFlash , flashPeriod));
    }

    public override void StopFlash()
    {
        StopAllCoroutines();
        SetNormal();
    }

    private IEnumerator Flash(float durationFlash , float flashPeriod)
    {
        while (durationFlash > 0)
        {
            SetFlash();
            yield return new WaitForSecondsRealtime(flashPeriod);
            durationFlash -= flashPeriod;
            SetNormal();
            yield return new WaitForSecondsRealtime(flashPeriod);
            durationFlash -= flashPeriod;
        }
    }

    void Reset()
    {
        if (imageFlash == null)
        {
            imageFlash = GetComponent<Image>();
        }

        if (normalMaterila == null)
        {
            normalMaterila = imageFlash.material;
        }
    }
}
