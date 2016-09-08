using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public LevelController levelController;

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
