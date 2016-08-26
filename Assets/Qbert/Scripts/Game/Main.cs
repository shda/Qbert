using System;
using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
    public InputController controlController;

    public GameField gameField;
    public Qbert qbert;
    
    void ConnectEvents()
    {
        controlController.OnPress += OnPressControl;
    }

    void DisconnectEvents()
    {
        controlController.OnPress -= OnPressControl;
    }

    private void OnPressControl(ControlController.ButtonType buttonType)
    {
        Cube findCube = gameField.GetCubeDirection(buttonType , qbert.currentLevel , qbert.currentNumber);
        if (findCube)
        {
            qbert.MoveToCube(findCube);
        }
        
        Debug.Log(buttonType);
    }

	void Start ()
	{
	    ConnectEvents();
	}



	void Update ()
    {
	
	}
}
