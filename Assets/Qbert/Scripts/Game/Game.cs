using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public LevelController      levelController;
    public CameraController     cameraController;
    public CubeCreateAnimator   cubeCreateAnimator;

    public GuiDispatcher    guiDispatcher;
    public GuiSettings      guiSettings;

    public void RestartLevel()
    {
        levelController.RestartLevel();
    }

    public void StartGame()
    {
        levelController.ResetScore();
        levelController.InitLevel(SelectLevel.selectLevel , 0);
        levelController.StartLevel();
    }

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
        guiSettings.OnMoveCameraToSettings();
    }

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
    }

    void Update()
    {

    }
}
