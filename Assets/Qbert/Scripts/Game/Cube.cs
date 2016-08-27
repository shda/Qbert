using System;
using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour
{
    public PointCube cubePosition;

    public Transform upSide;
    public ShaderSwitchColorLerp colorLerp;

    public Action<Cube , Character> OnPressEvents; 

    public bool isPress = false;

    public TextMesh debugText;

    public void OnPressMy(Character character)
    {
        if (OnPressEvents != null)
        {
            OnPressEvents(this , character);
        }
    }

	void Start () 
	{
	
	}
	
	void Update ()
	{
	    debugText.text = string.Format("{0},{1}", cubePosition.line, cubePosition.position);
	}
}
