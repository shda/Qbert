using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static Camera mainCamera { get; private set; }
    public static Transform root { get; private set; }

    [SerializeField]
    private Camera thisCamera;

    void OnEnable()
    {
        mainCamera = thisCamera;
        root = gameObject.transform.parent;
    }
}
