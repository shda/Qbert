using System;
using UnityEngine;
using System.Collections;

public class ChangeFloatToTime : CustomYieldInstruction
{
    private Action<float> OnChangeValue;
    private bool isReverce;
    private float duration;
    private float timeValue;

    public ChangeFloatToTime(float duration , bool isReverce , Action<float> OnChangeValue)
    {
        this.OnChangeValue = OnChangeValue;
        this.duration = duration;
        this.isReverce = isReverce;
    }

    public override bool keepWaiting
    {
        get
        {
            return ChangeValue(); ;
        }
    }

    private bool ChangeValue()
    {
        timeValue += Time.deltaTime / duration;
        if (OnChangeValue != null)
        {
            var value = Mathf.Clamp01(isReverce ? 1.0f - timeValue : timeValue);
            OnChangeValue(value);
        }

        if (timeValue >= 1)
        {
            return false;
        }

        return true;
    }
}
