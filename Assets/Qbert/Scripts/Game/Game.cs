using System;
using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    public InputController controlController;
    public GameField gameField;
    public Qbert qbert;

    public void RestartLevel()
    {
        qbert.SetStartPosition(0,0);
        gameField.ResetAllCube();
    }

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
    }

	void Start ()
	{
	    ConnectEvents();
	    RestartLevel();
	}

	void Update ()
    {
	
	}
}
