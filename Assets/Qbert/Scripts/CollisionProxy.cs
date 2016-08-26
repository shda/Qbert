using System;
using UnityEngine;
using System.Collections;

public class CollisionProxy : MonoBehaviour
{
    public Action<Transform, Transform> triggerEnterEvent;
    public Transform proxyObject;

    void OnTriggerEnter(Collider other)
    {
        CollisionProxy collider = other.GetComponent<CollisionProxy>();

        if (collider != null && triggerEnterEvent != null)
        {
            triggerEnterEvent(collider.proxyObject, proxyObject);
        }
    }
}