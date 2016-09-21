using UnityEngine;

namespace Assets.Qbert.Scripts.Utils
{
    public class TouchScript : MonoBehaviour  , ITouch
    {
        public void OnPress(bool isPress)
        {
            UnityEngine.Debug.Log("OnPress + " + isPress);
        }

        public void OnTap()
        {
            UnityEngine.Debug.Log("OnTap");
        }

        public void OnDrag(Vector2 delta)
        {
            UnityEngine.Debug.Log("OnDrag");
        }
    }
}
