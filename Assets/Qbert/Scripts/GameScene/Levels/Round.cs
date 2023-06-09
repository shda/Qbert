﻿using Assets.Qbert.Scripts.GameScene.Characters;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Levels
{
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

        [Header("Цвета раунда(можно не ставить)")]
        public Color[] customColors;

        [Header("Карта раунда(можно не ставить)")]
        public MapAsset customMap;

        [Header("Правила поведения (можно не ставить)")]
        public RuleCreateObjectsAsset rulesCustom;

        public RuleCreateGameplayObject[] rulesCreateGamplayObjects;

        public RuleCreateGameplayObject[] RulesCreateGamplayObjects
        {
            get
            {
                if (rulesCustom != null)
                {
                    return rulesCustom.rules;
                }

                return rulesCreateGamplayObjects;
            }
        }

        [HideInInspector]
        public float timeToStartRound;
        [HideInInspector]
        public LevelController levelController;
        private bool isRun = false;
    
        public void Init(LevelController levelController)
        {
            this.levelController = levelController;
            ResetRound();
        }

        public void ResetRound()
        {
            foreach (var gemeplayObjectConfig in RulesCreateGamplayObjects)
            {
                gemeplayObjectConfig.SetTimeScale(this);
                gemeplayObjectConfig.Reset();
            }
        }

        public void Run()
        {
            if (!isRun)
            {
                ResetTimerRun();
            }

            isRun = true;
        }

        public void ResetTimerRun()
        {
            timeToStartRound = 0;
        }
        public void Update()
        {
            if (isRun)
            {
                timeToStartRound += Time.deltaTime * timeScale;
                UpdateGameObjects();
            }
        }
        public void UpdateGameObjects()
        {
            foreach (var gemeplayObjectConfig in RulesCreateGamplayObjects)
            {
                gemeplayObjectConfig.CheckCreateObject(this);
            }
        }

        public int GetCountObjectToScene(GameplayObject.Type type)
        {
            if (levelController == null)
            {
                return int.MaxValue;
            }

            return levelController.gameplayObjects.GetCountObjectToScene(type);
        }
    }
}
