using UnityEngine;
using System.Collections;

public class PressScale : MonoBehaviour  , ITouch
{
    public enum ScaleDirection
    {
        X,Y,Z,
    }

    public ScaleDirection scaleDirection = ScaleDirection.Y;

    public float scale;
    public Transform rootScale;

    private Vector3 oldLocalScale;

    public void OnPress(bool isPress)
    {
        if (isPress)
        {
            oldLocalScale = rootScale.localScale;

            float x = scaleDirection == ScaleDirection.X ? scale : oldLocalScale.x;
            float y = scaleDirection == ScaleDirection.Y ? scale : oldLocalScale.y;
            float z = scaleDirection == ScaleDirection.Z ? scale : oldLocalScale.z;

            rootScale.localScale = new Vector3(x,y,z);
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
