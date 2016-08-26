using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour
{
    public int lineNumber;
    public int numberInLine;

    public Transform upSide;

    public Renderer upSideRender;

    public Color pressColor;
    public Color unpressColor;

    public void OnPressMy()
    {
        upSideRender.material.color = pressColor;
    }

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
