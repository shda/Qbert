using UnityEngine;

namespace Assets.Qbert.Scripts.Utils
{
    public interface ITouch  
    {
        void OnPress(bool isPress);
        void OnTap();
        void OnDrag(Vector2 delta);
    }
}
