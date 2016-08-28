using System;
using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{
    public InputController controlController;
    public GameplayObjects gameplayObjects;
    public LevelBase currentLevel;
    public GameField gameField;
    public Qbert qbert;

    private void OnPressCubeEvents(Cube cube, Character character)
    {
        currentLevel.OnCharacterPressToCube(cube , character);
    }

    private void OnPressControl(DirectionMove.Direction buttonType)
    {
        if (!qbert.isFrize)
        {
            Cube findCube = gameField.GetCubeDirection(buttonType, qbert.currentPosition);
            if (findCube)
            {
                qbert.MoveToCube(findCube);
            }
            else
            {
                Vector3 newPos = qbert.root.position + gameField.GetOffsetDirection(buttonType);
                qbert.MoveToPointAndDropDown(newPos, character =>
                {
                    RestartLevel();
                    Debug.Log("OnDead");
                });
            }
        }
    }

    private void OnCollisionCharacters(Transform owner1, Transform owner2)
    {
        var character1 = owner1.GetComponent<Character>();
        var character2 = owner2.GetComponent<Character>();

        currentLevel.OnCollisionCharacters(character1 , character2);
    }

    public void RestartLevel()
    {
        currentLevel.ResetLevel();
        currentLevel.StartLevel();
        qbert.isFrize = false;
    }

    void ConnectEvents()
    {
        controlController.OnPress = OnPressControl;
        gameField.OnPressCubeEvents = OnPressCubeEvents;
        qbert.collisionProxy.triggerEnterEvent = OnCollisionCharacters;
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
