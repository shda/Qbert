using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour  , ITouch
{
    public void OnPress(bool isPress)
    {
        Debug.Log("OnPress + " + isPress);
    }

    public void OnTap()
    {
        Debug.Log("OnTap");
    }

    public void OnDrag(Vector2 delta)
    {
        Debug.Log("OnDrag");
    }
}
