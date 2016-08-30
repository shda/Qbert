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

    public void SetPauseGamplayObjects(bool isPause)
    {
        float timeScale = isPause ? 0.0000001f : 1.0f;
        gameplayObjects.SetTimeScale(timeScale);
        currentLevel.SetTimeScaleGameplayObjects(timeScale);
    }
    private void OnPressCubeEvents(Cube cube, Character character)
    {
        currentLevel.OnCharacterPressToCube(cube , character);
    }

    private void OnCollisionCharacters(Transform owner1, Transform owner2)
    {
        var character1 = owner1.GetComponent<Character>();
        var character2 = owner2.GetComponent<Character>();

        currentLevel.OnCollisionCharacters(character1, character2);
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


    public void RestartLevel()
    {
        currentLevel.ResetLevel();
        currentLevel.StartLevel();
        qbert.isFrize = false;
        SetPauseGamplayObjects(false);
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


    public void StartPauseGameObjectsToSecond(float time)
    {
        StopAllCoroutines();
        StartCoroutine(TimerPauseGameObjectsToSecond(time));
    }

    private IEnumerator TimerPauseGameObjectsToSecond(float time)
    {
        SetPauseGamplayObjects(true);
        yield return new WaitForSeconds(time);
        SetPauseGamplayObjects(false);
    }

    public void SetPauseDebug()
    {
        StartPauseGameObjectsToSecond(3.0f);
    }
}
