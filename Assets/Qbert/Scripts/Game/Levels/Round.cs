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
                        isFirstStart = false;

                        if (countInScene < maxOneTime && CheckMaxToRound(round))
                        {
                            CreateObject(round);
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
    private bool isPause = false;

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
        if (isRun && !isPause)
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

    public void SetPause(bool isPause)
    {
        this.isPause = isPause;
    }
}
