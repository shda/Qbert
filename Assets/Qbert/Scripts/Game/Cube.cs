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

    public bool isPress = false;

    public void OnPressMy()
    {
        upSideRender.material.color = pressColor;
        isPress = true;
    }

    public void Reset()
    {
        upSideRender.material.color = unpressColor;
        isPress = false;
    }

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
