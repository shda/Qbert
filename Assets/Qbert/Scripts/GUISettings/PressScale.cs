using UnityEngine;
using System.Collections;

public class PressScale : MonoBehaviour  , ITouch
{
    public float scale;
    public Transform rootScale;

    private Vector3 oldLocalScale;

    public void OnPress(bool isPress)
    {
        if (isPress)
        {
            oldLocalScale = rootScale.localScale;

            rootScale.localScale = 
                new Vector3(oldLocalScale.x, scale, oldLocalScale.z);
        }
        else
        {
            rootScale.localScale =
                new Vector3(oldLocalScale.x, oldLocalScale.y, oldLocalScale.z);
        }
    }

    public void OnTap()
    {
       
    }

    public void OnDrag(Vector2 delta)
    {
        
    }
}
