﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    public PositionCube cubePosition;
    public Transform upSide;
    public Transform leftSide;
    public Transform rightSide;

    public CubeMap.CubeInMap cubeInMap;

    public ShaderSwitchColorLerp colorLerp;

    public Action<Cube , Character> OnPressEvents;

    public List<Cube> nodes = new List<Cube>(); 

    public Color[] colors;


    public TextMesh debugText;

    [HideInInspector]
    public bool isSet
    {
        get { return stateColor >= colors.Length - 1; }
    }
    public int stateColor = 0;
    public int lastState = 0;

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

    public void SetColorDrop()
    {
        colorLerp.SetColorSrart(colors[0]);
        colorLerp.SetColorEnd(colors[2]);
    }

    public void SetNextColor()
    {
        lastState = stateColor;

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
        lastState = stateColor;

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

    public void DropColor()
    {
        stateColor = 0;
        SetColorDrop();
        colorLerp.value = 0.0f;
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
        lastState = 0;
        SetColorOne();;
        colorLerp.value = 0.0f;
    }


    void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.blue;
        foreach (var node in nodes)
        {
            Gizmos.DrawLine(node.transform.position , transform.position);
        }
        */
    }
}
