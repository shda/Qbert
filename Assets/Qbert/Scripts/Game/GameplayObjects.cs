using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameplayObjects : MonoBehaviour
{
    public LevelController levelController;
    public GameplayObject[] gameplayObjectPaterns;
    public List<GameplayObject> gameplayObjectsList = new List<GameplayObject>();
    public Transform root;

    private GameplayObject CreateGameplayObject(GameplayObject.Type type)
    {
        foreach (var gameplayObjectPatern in gameplayObjectPaterns)
        {
            if (gameplayObjectPatern.typeEnemy == type)
            {
                return gameplayObjectPatern.Create(root , levelController);
            }
        }

        return null;
    }

    public GameplayObject AddGameplayObjectToGame(GameplayObject.Type type)
    {
        var gameplayObject = CreateGameplayObject(type);
        if (gameplayObject)
        {
            gameplayObject.Init();
            gameplayObject.Run();;
            gameplayObjectsList.Add(gameplayObject);

            gameplayObject.OnDestroyEvents = OnDestroyEvents;
        }

        return gameplayObject;
    }

    private void OnDestroyEvents(GameplayObject gameplayObject)
    {
        gameplayObjectsList.Remove(gameplayObject);

        gameplayObject.gameObject.SetActive(false);
        Destroy(gameplayObject.gameObject);
    }

    public void DestroyAllEnemies()
    {
        if (gameplayObjectsList != null)
        {
            foreach (var enemy in gameplayObjectsList)
            {
                enemy.gameObject.SetActive(false);
                Destroy(enemy.gameObject);
            }
        }

        gameplayObjectsList = new List<GameplayObject>();
    }
}
