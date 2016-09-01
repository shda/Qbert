using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[System.Serializable]
public class Round : ITimeScale
{
    //ITimeScale
    private float _timeScale = 1.0f;
    public float timeScale
    {
        get { return _timeScale; }
        set { _timeScale = value; }
    }
    //end ITimeScale

    [System.Serializable]
    public class GemeplayObjectConfig
    {
        [Header("Тип объекта")]
        public GameplayObject.Type type;
        [Header("Максимум за весь раунд")]
        public int maxToRound;
        [Header("Максимум одновременно")]
        public int maxOneTime;
        [Header("Задержка появления при старте")]
        public float delayToStart;
        [Header("Задержка между появлениями")]
        public float delayBetween;

        public float oldTimeCreateObject { get; set; }
        public float counterCreateObjects { get; set; }

        private bool isFirstStart = true;

        public void Reset()
        {
            oldTimeCreateObject = 0;
            counterCreateObjects = 0;
            isFirstStart = true;
        }

        public void CheckCreateObject(Round round)
        {
            int countInScene = round.GetCountObjectToScene(type);

            if (countInScene < maxOneTime)
            {
                if (round.timeToStart > delayToStart)
                {
                    if (round.timeToStart > oldTimeCreateObject + delayBetween || isFirstStart)
                    {
                        if (countInScene < maxOneTime && CheckMaxToRound(round))
                        {
                            if (CreateObject(round))
                            {
                                isFirstStart = false;
                            }
                        }
                    }
                }
            }
            else
            {
                oldTimeCreateObject = round.timeToStart;
            }
        }

        private bool CheckMaxToRound(Round round)
        {
            return counterCreateObjects < maxToRound || maxToRound == -1;
        }

        private bool CreateObject(Round round)
        {
            var addObj = round.levelController.gameplayObjects.AddGameplayObjectToGame(type);

            if (addObj != null)
            {
                counterCreateObjects++;
                oldTimeCreateObject = round.timeToStart;

                return true;
            }

            return false;
        }
    }

    
    public GemeplayObjectConfig[] gameplayConfigs;
    [Header("Цвета раунда")]
    public Color[] colors;

    private float timeToStart;
    private bool isRun = false;
    private LevelController levelController;

    public void Init(LevelController levelController)
    {
        this.levelController = levelController;
        ResetRound();
    }

    public void ResetRound()
    {
        foreach (var gemeplayObjectConfig in gameplayConfigs)
        {
            gemeplayObjectConfig.Reset();
        }

        Run();
    }

    public void Run()
    {
        timeToStart = 0;
        isRun = true;
    }
    public void Update()
    {
        if (isRun)
        {
            timeToStart += Time.deltaTime * timeScale;
            UpdateGameObjects();
        }
    }
    public void UpdateGameObjects()
    {
        foreach (var gemeplayObjectConfig in gameplayConfigs)
        {
            gemeplayObjectConfig.CheckCreateObject(this);
        }
    }

    public int GetCountObjectToScene(GameplayObject.Type type)
    {
        return levelController.gameplayObjects.GetCountObjectToScene(type);
    }
}
