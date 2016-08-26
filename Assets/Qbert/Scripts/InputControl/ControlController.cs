using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlController : SingletonT<ControlController>
{
    public enum ButtonType
    {
        Left,
        Right,
        Up,
        Down
    }

    [Serializable]
    public class ButtonControl
    {
        public ButtonTouchPanel button;
        public ButtonType buttonType;
    }

    public ButtonControl[] controls;
    public InputArrea inputArrea;
    public Action<Vector2> OnPressToScreen;

    public override void AwakeFirst()
    {
        
    }

    void Start () 
	{
        inputArrea.OpDownPressToScreen = OpDownPressToScreen;
	    ConnectButtons();
	}

    void ConnectButtons()
    {
        foreach (var buttonControl in controls)
        {
            buttonControl.button.buttonType = (int) buttonControl.buttonType;
        }
    }

    public bool GetInDown(ButtonType buttonType)
    {
        return GetButtonByType(buttonType).button.GetInDown();
    }

    public float GetButtonValue(ButtonType buttonType)
    {

        return GetButtonByType(buttonType).button.GetValue();
    }

    public bool GetButtonIsPressed(ButtonType buttonType)
    {
        return GetButtonByType(buttonType).button.isPressed;
    }

    public ButtonControl GetButtonByType(ButtonType buttonType)
    {
        foreach (var buttonControl in controls)
        {
            if (buttonControl.button.buttonType == (int)buttonType)
            {
                return buttonControl;
            }
        }
        return null;
    }

    private void OpDownPressToScreen(Vector2 vector2)
    {
        if (OnPressToScreen != null)
        {
            OnPressToScreen(vector2);
        }
    }


    void Update () 
	{
	
	}
}
