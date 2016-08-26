using System;
using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    public Action<ControlController.ButtonType> OnPress; 
    public ControlController controlController;

	void Start ()
	{
        /*
	    OnPress = type =>
	    {
            Debug.Log(type);
	    };
        */
	}
	
	void Update () 
	{
	    foreach (var value in Enum.GetValues( typeof(ControlController.ButtonType) ) )
	    {
	        var eValue = (ControlController.ButtonType) value;
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
