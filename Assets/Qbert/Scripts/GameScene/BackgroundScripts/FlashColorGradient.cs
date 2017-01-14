using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.Utils;
using UnityEngine.UI;

public class FlashColorGradient : FlashColorBase
{
    public Color[] flashColors;

    public Image imageFlash;

    private Material materialNormal;

    private const string colorInName = "_ColorIn";
    private const string colorOutName = "_ColorOut";

    private Color[] normalColors;

    void Awake()
    {
        materialNormal = new Material(imageFlash.material);
        normalColors = new []
        {
            materialNormal.GetColor(colorInName),
            materialNormal.GetColor(colorOutName)
        } ;

        imageFlash.material = materialNormal;

        //StartCoroutine(Flash(10 , 0.3f));
    }

    public override void SetFlash()
    {
       // imageFlash.material = materialFlash;
    }

    public override void SetNormal()
    {
       // imageFlash.material = materialNormal;
    }

    public override void StartFlash(float durationFlash , float dutationOneColor)
    {
        StopFlash();
        StartCoroutine(Flash(durationFlash , dutationOneColor));
    }

    public override void StopFlash()
    {
        StopAllCoroutines();
        SetNormal();
    }

    private IEnumerator Flash(float durationFlash , float dutationOneColor)
    {
        while (durationFlash > 0)
        {
            float saveTime = Time.fixedTime;

            Color currentFirst = normalColors[0];
            Color currentSecond = normalColors[1];

            //Set flash color
            yield return new ChangeFloatToTime(dutationOneColor, false , f =>
            {
                currentFirst = LerpColor(currentFirst, flashColors[0], f);
                currentSecond = LerpColor(currentSecond, flashColors[1], f);
                SetColor(currentFirst, currentSecond);
            });

            //Set normal color
            yield return new ChangeFloatToTime(dutationOneColor, false, f =>
            {
                currentFirst = LerpColor(currentFirst, normalColors[0], f);
                currentSecond = LerpColor(currentSecond, normalColors[1], f);
                SetColor(currentFirst, currentSecond);
            });


            durationFlash -= Time.fixedTime - saveTime;
        }

        SetColor(normalColors[0], normalColors[1]);

    }

    private void SetColor(Color colorIn , Color colorOut)
    {
        materialNormal.SetColor(colorInName, colorIn);
        materialNormal.SetColor(colorOutName, colorOut);
    }


    private Color LerpColor(Color current, Color colorTo , float value)
    {
        return Color.Lerp(current, colorTo, value);
    }
}
