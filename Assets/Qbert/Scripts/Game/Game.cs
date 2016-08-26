using System;
using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    public LevelController levelController;

    public void RestartLevel()
    {
        levelController.RestartLevel();
    }

    public void InitLevel()
    {
        levelController.InitLevel();
    }

    void Start()
    {
        InitLevel();
    }

    void Update()
    {

    }
}
