using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public LevelController      levelController;
    public CameraController     cameraController;
    public CubeCreateAnimator   cubeCreateAnimator;

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
        cameraController.MoveCameraToCube(new PositionCube(4,2) , 4.5f, 1.0f );
        cubeCreateAnimator.StartAnimateShow();
        Debug.Log("Tap");
    }


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
