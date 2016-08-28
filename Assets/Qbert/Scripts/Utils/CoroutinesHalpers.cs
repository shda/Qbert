﻿using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class CoroutinesHalpers 
{
    public static void LanchAction(Action<Transform> OnEnd , Transform tr)
    {
        if (OnEnd != null)
        {
            OnEnd(tr);
        }
    }

    public static IEnumerator ScaleTranformTo(this MonoBehaviour mono, Transform tr, float scaleTo, float duration, Action<Transform> OnEnd = null)
    {
        Vector3 vSceleTo = new Vector3(scaleTo, scaleTo, 1);
        float distance = Vector3.Distance(vSceleTo, tr.localScale);
        float speedScale = distance / duration;

        while (distance > 0.001f)
        {
            distance = Vector3.Distance(vSceleTo, tr.localScale);
            Vector3 scale = Vector3.MoveTowards(tr.localScale, vSceleTo, speedScale * Time.deltaTime);
            tr.localScale = scale;
            yield return new WaitForEndOfFrame();
        }

        LanchAction(OnEnd , tr);
    }

    public static IEnumerator ChangeColorImage(this MonoBehaviour mono, Image obj, Color colorTo,
        float duration, Action<Transform> OnEnd = null)
    {
        float t = 0;
        Color fr = obj.color;
        Color to = colorTo;
        while (t < 1)
        {
            t += Time.smoothDeltaTime / duration;
            obj.color = Color.Lerp(fr, to, t);
            yield return null;
        }

        LanchAction(OnEnd, obj.transform);
    }


    public static IEnumerator ChangeColorSpriteRenderer(this MonoBehaviour mono,  SpriteRenderer obj, Color colorTo, float duration , Action<Transform> OnEnd = null )
    {
        float t = 0;
        Color fr = obj.color;
        Color to = colorTo;
        while (t < 1)
        {
            t += Time.smoothDeltaTime / duration;
            obj.color = Color.Lerp(fr, to, t);
            yield return null;
        }

        LanchAction(OnEnd, obj.transform);
    }

    public static IEnumerator WaitForSecondITime(this MonoBehaviour mono, float duration, ITime iTime = null)
    {
        float t = 0;
        while (t < 1)
        {
            t += GetTimeDeltatimeScale(iTime) / duration;
            yield return null;
        }
    }

    public static IEnumerator MovingTransformTo(this MonoBehaviour mono, Transform tr, Vector3 movingTo, float time, ITime iTime = null , Action<Transform> OnEnd = null)
    {
        float distance = Vector3.Distance(tr.position, movingTo);
        float speedMoving = distance / time;

        while (distance > 0.001f)
        {
            Vector3 move = Vector3.MoveTowards(tr.position, movingTo, speedMoving * GetTimeDeltatimeScale(iTime));
            tr.position = move;
            distance = Vector3.Distance(tr.position, movingTo);
            yield return null;
        }

        LanchAction(OnEnd, tr);
    }

    public static IEnumerator MovingSpeedTransformTo(this MonoBehaviour mono, Transform tr, Vector3 movingTo, float speed,  ITime iTime = null , Action<Transform> OnEnd = null)
    {
        float distance;
        do
        {
            tr.localPosition = Vector3.MoveTowards(tr.localPosition, movingTo, speed * GetTimeDeltatimeScale(iTime));
            distance = Vector3.Distance(tr.localPosition, movingTo);
            yield return null;
        } while (distance > 0);

        LanchAction(OnEnd, tr);
    }

    public static float GetTimeDeltatimeScale(ITime iTime = null)
    {
        float timeDeltaTime = Time.deltaTime;
        if (iTime != null)
        {
            timeDeltaTime = timeDeltaTime * iTime.timeScale;
        }

        return timeDeltaTime;
    }
}
