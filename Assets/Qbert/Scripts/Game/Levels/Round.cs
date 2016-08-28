using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[System.Serializable]
public class Round
{
    [System.Serializable]
    public class GemeplayObjectConfig
    {
        public GameplayObject.Type type;
        public int maxToRound;
        public int maxOneTime;
        public float delayToStart;
        public float delayBetween;

        public float oldTimeCreateObject { get; set; }
        public float counterCreateObjects { get; set; }

        public float oldCounterCreateObjects { get; set; }

        public void Reset()
        {
            oldTimeCreateObject = 0;
            counterCreateObjects = 0;
            oldCounterCreateObjects = 0;
        }

        public void CheckCreateObject(Round round)
        {
            if (round.timeToStart > delayToStart)
            {
                int countInScene = round.GetCountObjectToScene(type);

                if (countInScene == 0)
                {
                    if (countInScene < maxOneTime && CheckMaxToRound(round))
                    {
                        CreateObject(round);
                    }
                }
                else
                {
                    if (round.timeToStart > oldTimeCreateObject + delayBetween)
                    {
                        if (countInScene < maxOneTime && CheckMaxToRound(round))
                        {
                            CreateObject(round);
                        }
                    }
                }
            }
        }

        private bool CheckMaxToRound(Round round)
        {
            return counterCreateObjects < maxToRound || maxToRound == -1;
        }


        private void CreateObject(Round round)
        {
            counterCreateObjects++;
            round.levelController.gameplayObjects.AddGameplayObjectToGame(type);
            oldTimeCreateObject = round.timeToStart;
        }
    }

    public GemeplayObjectConfig[] gameplayConfigs;

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
            timeToStart += Time.deltaTime;
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
