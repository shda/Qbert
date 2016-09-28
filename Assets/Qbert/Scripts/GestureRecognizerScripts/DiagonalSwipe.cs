using Assets.Qbert.Scripts.GameScene;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Qbert.Scripts.GestureRecognizerScripts
{
    public class DiagonalSwipe : MonoBehaviour ,IDragHandler , IPointerDownHandler , IPointerUpHandler
    {
        [System.Serializable]
        public class SwipeEvent : UnityEvent<DirectionMove.Direction>
        {

        }

        public float minSwipeDist = 50.0f;
        public int widthScreen = 1027;
        public int hightScreen = 768;

        public SwipeEvent OnSwipeDirection;


        void Update()
        {

        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            float screenScaleWidth =    (float) widthScreen / Screen.width;
            float screenScaleHight =    (float) hightScreen / Screen.height;

            var startPos    = eventData.pressPosition;
            var endPos      = eventData.position;

            startPos = new Vector2(startPos.x * screenScaleWidth, startPos.y * screenScaleHight);
            endPos = new Vector2(endPos.x * screenScaleWidth, endPos.y * screenScaleHight);

            var aver = startPos - endPos;

            //Debug.Log(Vector2.Distance(startPos, endPos));

            if (Vector2.Distance(startPos, endPos) > minSwipeDist)
            {
                //Left
                if (aver.x > 0)
                {
                    //Up
                    if (aver.y > 0)
                    {
                        OnSwipe(DirectionMove.Direction.DownLeft);
                    }
                    //Down
                    else
                    {
                        OnSwipe(DirectionMove.Direction.UpLeft);
                    }
                }
                //Right
                else
                {
                    //Up
                    if (aver.y > 0)
                    {
                        OnSwipe(DirectionMove.Direction.DownRight);
                    }
                    //Down
                    else
                    {
                        OnSwipe(DirectionMove.Direction.UpRight);
                    }
                }
            }
        }

        public void OnSwipe(DirectionMove.Direction dir)
        {
            if (OnSwipeDirection != null)
            {
                OnSwipeDirection.Invoke(dir);
            }
           // Debug.Log(dir.ToString());
        }
    }
}
