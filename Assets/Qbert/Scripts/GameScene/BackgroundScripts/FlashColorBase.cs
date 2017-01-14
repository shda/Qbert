using UnityEngine;

public abstract class FlashColorBase : MonoBehaviour
{
    public abstract void SetFlash();
    public abstract void SetNormal();
    public abstract void StartFlash(float durationFlash, float dutationOneColor);
    public abstract void StopFlash();
}
