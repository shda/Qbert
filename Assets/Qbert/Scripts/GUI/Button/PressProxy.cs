using Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.GUI.Button
{
    public class PressProxy : MonoBehaviour , ITouch
    {
        [System.Serializable]
        public class ButtonPressEvent : UnityEvent<PressProxy>
        {
        }

        public ButtonPressEvent OnPressButton;

        public void OnPress(bool isPress)
        {
        
        }

        public void OnTap()
        {
            if (OnPressButton != null)
            {
                OnPressButton.Invoke(this);
            }
        }

        public void OnDrag(Vector2 delta)
        {
        
        }
    }
}
