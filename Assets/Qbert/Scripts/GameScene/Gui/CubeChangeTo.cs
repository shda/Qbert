using UnityEngine;
using System.Collections;

public class CubeChangeTo : MonoBehaviour
{
    public Renderer render;

    public void SetColor(Color color)
    {
        render.material.color = color;
    }

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
