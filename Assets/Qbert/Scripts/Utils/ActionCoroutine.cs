using System;
using System.Collections;
using UnityEngine;

namespace Assets.Qbert.Scripts.Utils
{
    public class ActionCoroutine : MonoBehaviour
    {
        public Action<ActionCoroutine> OnEndAction;

        public IEnumerator HideTextTo(GameObject obj, float hideTo, float time)
        {
            TextMesh tm = obj.GetComponent<TextMesh>();
            if (tm == null)
            {
                UnityEngine.Debug.LogError("Error get component TextMesh!!!");
            }
            else
            {
                yield return StartCoroutine(HideTextTo(tm, hideTo, time));
            }
        }

        public IEnumerator ScaleTo(Transform scaleObj, float scaleTo, float time)
        {
            Vector3 vScaleTo = new Vector3(scaleTo, scaleTo, 1);

            float distance = Vector2.Distance(vScaleTo, scaleObj.localScale);

            float speedScale = distance / time;

            while (distance > 0.0f)
            {
                distance = Vector2.Distance(vScaleTo, scaleObj.localScale);
                Vector2 scale = Vector2.MoveTowards(scaleObj.localScale, vScaleTo, speedScale * Time.deltaTime);
                scaleObj.localScale = scale;
                yield return new WaitForEndOfFrame();
            }

            SendEvent();
        }

        public IEnumerator MoveToAndScale(Transform scaleObj, Vector3 moveTo, float scaleTo, float time, Action OnEndAction = null)
        {
            Vector3 vSceleTo = new Vector3(scaleTo, scaleTo, 1);

            float distanceScale = Vector3.Distance(vSceleTo, scaleObj.localScale);
            float distanceMove = Vector3.Distance(moveTo, scaleObj.position);
            float speedScale = distanceScale / time;
            float speedMove = distanceMove / time;

            while (distanceScale > 0.0f || distanceMove > 0.0f)
            {
                distanceScale = Vector3.Distance(vSceleTo, scaleObj.localScale);
                distanceMove = Vector3.Distance(moveTo, scaleObj.position);

                Vector3 scale = Vector3.MoveTowards(scaleObj.localScale, vSceleTo, speedScale * Time.deltaTime);
                Vector3 move = Vector3.MoveTowards(scaleObj.position, moveTo, speedMove * Time.deltaTime);

                scaleObj.localScale = scale;
                scaleObj.position = move;

                yield return new WaitForEndOfFrame();
            }

            SendEvent();
        }

        public IEnumerator HideTextTo(TextMesh obj, float hideTo, float time)
        {
            if ( time == 0 )
            {
                Color color = new Color(obj.color.r, obj.color.g, obj.color.b, hideTo);
                obj.color = color;
            }
            else
            {
                float distance = Math.Abs(obj.color.a - hideTo);
                float speedMoving = distance / time;

                if (hideTo <= obj.color.a)
                {
                    while (obj.color.a > 0.0f)
                    {
                        Color color = new Color(obj.color.r, obj.color.g, obj.color.b, obj.color.a - speedMoving * Time.deltaTime);
                        obj.color = color;
                        yield return null;
                    }
                }
                else
                {
                    while (obj.color.a < hideTo)
                    {
                        Color color = new Color(obj.color.r, obj.color.g, obj.color.b, obj.color.a + speedMoving * Time.deltaTime);
                        obj.color = color;
                        yield return null;
                    }
                }
            }

            SendEvent();
        }

        public IEnumerator MovingObjectTo(Transform obj, Vector3 movingTo, float time)
        {
            float distance = Vector3.Distance(obj.position, movingTo);
            float speedMoving = distance / time;

            while (distance > 0.0f)
            {
                Vector3 move = Vector3.MoveTowards(obj.position, movingTo, speedMoving * Time.deltaTime);
                obj.position = move;
                distance = Vector3.Distance(obj.position, movingTo);

                yield return null;
            }

            SendEvent();
        }

        void SendEvent()
        {
            if (OnEndAction != null)
                OnEndAction(this);
        }
    }
}
