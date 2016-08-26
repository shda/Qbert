using System;
using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{
    public InputController controlController;
    public LevelActions currentLevel;
    public GameField gameField;
    public Qbert qbert;

    private void OnPressCubeEvents(Cube cube, Character character)
    {
        currentLevel.OnCharacterPressToCube(cube , character);
    }

    private void OnPressControl(ControlController.ButtonType buttonType)
    {
        Cube findCube = gameField.GetCubeDirection(buttonType, qbert.currentLevel, qbert.currentNumber);
        if (findCube)
        {
            qbert.MoveToCube(findCube);
        }
    }

    public void RestartLevel()
    {
        currentLevel.ResetLevel();
    }

    void ConnectEvents()
    {
        controlController.OnPress = OnPressControl;
        gameField.OnPressCubeEvents = OnPressCubeEvents;
    }

    public void InitLevel()
    {
        currentLevel.SetController(this);
        currentLevel.InitLevel();
        gameField.Init();
        ConnectEvents();

        RestartLevel();
    }
}
