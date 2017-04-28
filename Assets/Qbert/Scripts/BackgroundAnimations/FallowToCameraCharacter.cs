using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallowToCameraCharacter : MonoBehaviour
{
    public Transform point;
    public Transform rootCamera;

    public Vector3 startPoint;
    public Vector3 startCamera;

    public Vector3 current;

    public bool isFallowToCharacter = true;

	void Start () 
	{
	    if (point != null)
	    {
            startPoint = point.position;
            startCamera = rootCamera.position;
        }
    }
	
	void Update ()
	{
	    if (isFallowToCharacter && point != null)
	    {
            current = startCamera + startPoint + point.position;
            rootCamera.position = current;
        }
	}
}
