using UnityEngine;

namespace Assets.Qbert.Scripts.GestureRecognizerScripts
{
    public class GestureRecognizer : MonoBehaviour
    {
        public bool singleTouchMode;

        public Vector2 startPoint;
        public Vector2 endPoint;

        void Start () 
        {
	
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                ProcessTouches();
            }
            else
            {
                ProcessMouse();
            }
        }

        private void ProcessTouches()
        {
            foreach (var touch in Input.touches)
            {
                bool pressed = touch.phase == TouchPhase.Began;
                bool unpressed = touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
                int id = touch.fingerId;
                TouchEvent(id, touch.position, pressed, unpressed);
                if (singleTouchMode) break;
            }
        }

        private void ProcessMouse()
        {
            for (int i = 0; i < 3; i++)
            {
                bool pressed = Input.GetMouseButtonDown(i);
                bool unpressed = Input.GetMouseButtonUp(i);
                int id = -i;
                if (pressed || unpressed || Input.GetMouseButton(i))
                {
                    TouchEvent(id, Input.mousePosition, pressed, unpressed);
                }
            }
        }

        private void TouchEvent(int id, Vector2 position, bool pressed, bool unpressed)
        {
            Debug.Log(position);
        }
    }
}
