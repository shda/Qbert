using System;
using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour
{
    public PositionCube cubePosition;
    public Transform upSide;

    public ShaderSwitchColorLerp colorLerp;

    public Action<Cube , Character> OnPressEvents; 
    public TextMesh debugText;

    public Color[] colors;

    [HideInInspector]
    public bool isSet
    {
        get { return stateColor >= colors.Length - 1; }
    }
    public int stateColor = 0;

    public void SetColors(Color[] colors)
    {
        this.colors = colors;
        SetColorOne();
    }

    private void SetColorOne()
    {
        colorLerp.SetColorSrart(colors[0]);
        colorLerp.SetColorEnd(colors[1]);
    }

    public void SetColorTwo()
    {
        colorLerp.SetColorSrart(colors[1]);
        colorLerp.SetColorEnd(colors[2]);
    }

    public void SetNextColor()
    {
        stateColor++;

        if (stateColor == 1)
        {
            SetColorOne();
            colorLerp.valueLerp = 0.0f;
            colorLerp.value = 1.0f;
        }
        else if (stateColor == 2 && colors.Length > 2)
        {
            SetColorTwo();
            colorLerp.valueLerp = 0.0f;
            colorLerp.value = 1.0f;
        }

        stateColor = Mathf.Clamp(stateColor, 0, colors.Length - 1);
    }

    public void SetLastColor()
    {
        stateColor--;

        if (stateColor == 0)
        {
            SetColorOne();
            colorLerp.value = 0.0f;
        }
        else if (stateColor == 1)
        {
            SetColorTwo();
            colorLerp.value = 0.0f;
        }

        stateColor = Mathf.Clamp(stateColor, 0, colors.Length - 1);
    }

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

    public void Reset()
    {
        stateColor = 0;
        SetColorOne();;
        colorLerp.value = 0.0f;
    }
}
