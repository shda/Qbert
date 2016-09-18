using System;
using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    public Action<DirectionMove.Direction> OnPress; 
    public ControlController controlController;

    public bool isEnable;

	void Start ()
	{
	}
	
	void Update () 
	{
	    if (isEnable)
	    {
            foreach (var value in Enum.GetValues(typeof(DirectionMove.Direction)))
            {
                var eValue = (DirectionMove.Direction)value;
                if (controlController.GetInDown(eValue))
                {
                    if (OnPress != null)
                    {
                        OnPress(eValue);
                    }
                }
            }
        }
	}
}
