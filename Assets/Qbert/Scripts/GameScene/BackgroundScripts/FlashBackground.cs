using UnityEngine;
using System.Collections;

public class FlashBackground : MonoBehaviour
{
    public FlashColorBase flashColor;

    public void Flash(float durationFlash)
    {
        flashColor.StartFlash(durationFlash, 0.3f);
    }
    public void SetNormal()
    {
        flashColor.StopFlash();
    }
}
