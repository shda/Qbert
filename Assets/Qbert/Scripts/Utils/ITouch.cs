using UnityEngine;
using System.Collections;

public interface ITouch  
{
    void OnPress(bool isPress);
    void OnTap();
    void OnDrag(Vector2 delta);
}
