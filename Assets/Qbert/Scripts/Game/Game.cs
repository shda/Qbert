using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public LevelController      levelController;
    public FadeScreen           fadeScreen;

    public InputController  inputController;


    public void RestartLevel()
    {
        levelController.RestartLevel();
    }

    public void StartGame()
    {
        levelController.ResetScore();
        levelController.InitLevel(GlobalSettings.currentLevel, GlobalSettings.currentRound);

        levelController.StartLevel();
    }

    /*
    public void OnTapScreenStartGame()
    {
        guiDispatcher.inputScreenControls.SetActive(false);
        guiDispatcher.mainMenuGui.SetActive(false);

        cubeCreateAnimator.StartAnimateShow();
        cameraController.MoveCameraToCube(new PositionCube(4, 2), 4.5f, 1.0f , transform1 =>
        {
            guiDispatcher.inputScreenControls.SetActive(true);
            guiDispatcher.scoreAndCoins.SetActive(true);
        });
    }

    public void OnPressSettings()
    {
        guiDispatcher.inputScreenControls.SetActive(false);
        guiDispatcher.mainMenuGui.SetActive(false);
       // guiSettings.OnMoveCameraToSettings();
    }
    */

    public void OnPressSelectCharacter()
    { }


    public string sceneName;
    public void LoadSelectLevelScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    void Awake()
    {
       // QualitySettings.vSyncCount = 0;
       // Application.targetFrameRate = 10;
    }

    void Start()
    {
        StartGame();
        levelController.SetPauseGamplayObjects(true);
        inputController.isEnable = false;

        fadeScreen.OnEnd = transform1 =>
        {
            levelController.SetPauseGamplayObjects(false);
            inputController.isEnable = true;
        };

        fadeScreen.StartDisable(0.5f);
    }

    void Update()
    {

    }
}
