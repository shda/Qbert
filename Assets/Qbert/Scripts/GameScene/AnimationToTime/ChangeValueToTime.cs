using UnityEngine;
using System.Collections;
using Scripts.GameScene;

public abstract class ChangeValueToTime : ITime
{
    public virtual IEnumerator PlayToTime(float duration, ITimeScale ITimeScale = null , bool isReverce = false)
    {
        ITimeScale iTimeCurrent = ITimeScale ?? this;

        float t = 0;
        this.isReverce = isReverce;

        while (t < 1)
        {
            t += (Time.deltaTime * iTimeCurrent.timeScale) / duration;
            time = t;
            yield return null;
        }

        t = 1;

        time = t;
    }

   // public bool isEnable = false;
   // public float value = 0;


    void Update()
    {
       // if (isEnable)
        {
          //  time = value;
        }
    }
}
