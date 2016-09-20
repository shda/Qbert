using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameplayObjects : MonoBehaviour , ITimeScale
{
    //ITimeScale
    private float _timeScale = 1.0f;
    public float timeScale
    {
        get { return _timeScale; }
        set { _timeScale = value; }
    }
    //end ITimeScale

    public LevelController levelController;
    public List<GameplayObject> gameplayObjectsList = new List<GameplayObject>();
    public Transform root;

    public GameplayObject GetGamplayObjectInPoint(PositionCube point )
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
        timeScale = scale;
    }

    private GameplayObject CreateGameplayObject(GameplayObject.Type type)
    {
        var obj =  PoolGameplayObjects.GetGameplayObject(type);
        var init = obj.TryInitializeObject(root, levelController);
        if (init)
        {
            return init;
        }

        PoolGameplayObjects.ReturnObject(obj);

        return null;
    }

    public GameplayObject AddGameplayObjectToGame(GameplayObject.Type type)
    {
        var gameplayObject = CreateGameplayObject(type);
        if (gameplayObject)
        {
            gameplayObject.SetTimeScaler(this);
            gameplayObject.Init();
            gameplayObject.Run();
            gameplayObjectsList.Add(gameplayObject);

            gameplayObject.OnDestroyEvents = OnDestroyEvents;
        }

        return gameplayObject;
    }

    private void OnDestroyEvents(GameplayObject gameplayObject)
    {
        gameplayObjectsList.Remove(gameplayObject);
        gameplayObject.gameObject.SetActive(false);

        PoolGameplayObjects.ReturnObject(gameplayObject);

        //Destroy(gameplayObject.gameObject);
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
        return gameplayObjectsList.Count(x => x.typeObject == type);
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
            if (gObject.typeObject == type)
            {
                list.Add(gObject as T);
            }
        }

        return list.ToArray();
    }

  
}
