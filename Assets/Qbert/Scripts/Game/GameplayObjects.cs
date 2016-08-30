using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameplayObjects : MonoBehaviour
{
    public Transform pointMoveTransport;
    public LevelController levelController;
    public GameplayObject[] gameplayObjectPaterns;
    public List<GameplayObject> gameplayObjectsList = new List<GameplayObject>();
    public Transform root;

    public GameplayObject GetGamplayObjectInPoint(PositionCube point)
    {
        foreach (var gameplayObject in gameplayObjectsList)
        {
            if (gameplayObject.currentPosition == point || gameplayObject.positionMove == point)
            {
                return gameplayObject;
            }
        }

        return null;
    }

    public void SetTimeScale(float scale)
    {
        foreach (var gameplayObject in gameplayObjectsList)
        {
            gameplayObject.timeScale = scale;
        }
    }

    private GameplayObject CreateGameplayObject(GameplayObject.Type type)
    {
        foreach (var gameplayObjectPatern in gameplayObjectPaterns)
        {
            if (gameplayObjectPatern.typeGameobject == type)
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

    public int GetCountObjectToScene(GameplayObject.Type type)
    {
        return gameplayObjectsList.Count(x => x.typeGameobject == type);
    }

    public T[] GetObjectsToType<T>() where T : GameplayObject
    {
        List<T> list = new List<T>();

        foreach (GameplayObject gObject in gameplayObjectsList)
        {
            if (gObject is T)
            {
                list.Add(gObject as T);
            }
        }

        return list.ToArray();
    }

    public T[] GetObjectsToTypeObject<T>(GameplayObject.Type type) where T : GameplayObject
    {
        List<T> list = new List<T>();

        foreach (GameplayObject gObject in gameplayObjectsList)
        {
            if (gObject.typeGameobject == type)
            {
                list.Add(gObject as T);
            }
        }

        return list.ToArray();
    }
}
