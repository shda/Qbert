using UnityEngine;

namespace Scripts.Utils
{
    public interface ITouch  
    {
        void OnPress(bool isPress);
        void OnTap();
        void OnDrag(Vector2 delta);
    }
}
