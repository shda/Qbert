using System;
using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{
    public InputController controlController;
    public GameplayObjects gameplayObjects;
    public GameField gameField;
    public Qbert qbert;
    public GameGui gameGui;
    public LevelSwitcher levelSwitcher;

    public LevelBehaviour levelLogic;

    public void AddScore(float score)
    {
        if (gameGui)
        {
            gameGui.AddScore(score);
        }
    }

    public void AddCoins(int coint)
    {
        if (gameGui)
        {
            gameGui.AddCoins(coint);
        }
    }

    public void OnQubertDead()
    {
        RestartLevel();
    }

    public void SetPauseGamplayObjects(bool isPause)
    {
        float timeScale = isPause ? 0.0000001f : 1.0f;
        gameplayObjects.SetTimeScale(timeScale);
        levelLogic.SetTimeScaleGameplayObjects(timeScale);
    }

    public MapAsset GetMapAssetFromLevel()
    {
        return levelLogic.GetMapAssetFromCurrentRound();
    }

    private void OnPressCubeEvents(Cube cube, Character character)
    {
        levelLogic.OnCharacterPressToCube(cube , character);
    }

    private void OnCollisionCharacters(Transform owner1, Transform owner2)
    {
        var character1 = owner1.GetComponent<Character>();
        var character2 = owner2.GetComponent<Character>();

        levelLogic.OnCollisionCharacters(character1, character2);
    }

    private void OnPressControl(DirectionMove.Direction buttonType)
    {
        qbert.OnCommandMove(buttonType);
    }

    void ConnectEvents()
    {
        if (controlController != null)
        {
            controlController.OnPress = OnPressControl;
        }
           
        gameField.OnPressCubeEvents = OnPressCubeEvents;
        qbert.collisionProxy.triggerEnterEvent = OnCollisionCharacters;
    }

    public void RestartLevel()
    {
        if (gameGui != null)
        {
            gameGui.SetLevel(levelSwitcher.currentLevel, levelLogic.roundCurrent);
        }

        InitLevel(levelSwitcher.currentLevel , levelLogic.roundCurrent);

        StopAllCoroutines();
        SetPauseGamplayObjects(false);

        levelLogic.ResetLevel();
        levelLogic.StartLevel(levelLogic.roundCurrent);
        qbert.isFrize = false;
        qbert.isCheckColision = true;
        SetPauseGamplayObjects(false);
    }

    public void ResetScore()
    {
        if (gameGui != null)
        {
            gameGui.UpdateScore();
        }
    }

    public void NextLevel()
    {
        levelSwitcher.NextLevel();
        RestartLevel();
    }

    public void NextRound()
    {
        levelLogic.NextRound();
    }

    public void EndLevels()
    {
        InitLevel(0,0);
    }

    public void InitLevelLoad()
    {
        //  GlobalSettings.currentLevel = 3;

        levelLogic = levelSwitcher.InitLevelLoad(GlobalSettings.currentLevel);

        levelLogic.InitLevel();

        gameField.mapGenerator.mapAsset = GetMapAssetFromLevel();
        gameField.mapGenerator.CreateMap();

        gameField.Init();
    }

    public void InitLevel(int level , int round)
    {
        if (gameGui)
        {
            gameGui.SetLevel(level, round);
        }

        levelLogic = levelSwitcher.SetLevel(level, round);
        levelLogic.InitLevel();

        gameField.mapGenerator.mapAsset = GetMapAssetFromLevel();
        gameField.mapGenerator.CreateMap();

        gameField.Init();
    }

    public void StartLoadingLevel()
    {
        ConnectEvents();
        levelLogic.ResetLevel();
    }

    public void StartLevel()
    {
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
        qbert.isCheckColision = false;

        yield return new WaitForSeconds(time);

        SetPauseGamplayObjects(false);
        qbert.isCheckColision = true;
    }

    
}
