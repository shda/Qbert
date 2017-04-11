using UnityEngine;
using System.Collections;

public class FlashBackground : MonoBehaviour
{
    public FlashColorBase flashColor;

    public void Flash(float durationFreezingTime)
    {
        flashColor.StartFlash(durationFreezingTime , 0.3f);
    }
    public void SetNormal()
    {
        flashColor.StopFlash();
    }
}
