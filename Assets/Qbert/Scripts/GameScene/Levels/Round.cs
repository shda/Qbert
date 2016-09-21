using Assets.Qbert.Scripts.GameScene.Characters;
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

        public RuleCreateGameplayObject[] rulesCreateGamplayObjects;
    
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
            foreach (var gemeplayObjectConfig in rulesCreateGamplayObjects)
            {
                gemeplayObjectConfig.SetTimeScale(this);
                gemeplayObjectConfig.Reset();
            }

            Run();
        }

        public void Stop()
        {
            isRun = false;
        }

        public void Run()
        {
            timeToStartRound = 0;
            isRun = true;
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
            foreach (var gemeplayObjectConfig in rulesCreateGamplayObjects)
            {
                gemeplayObjectConfig.CheckCreateObject(this);
            }
        }

        public int GetCountObjectToScene(GameplayObject.Type type)
        {
            return levelController.gameplayObjects.GetCountObjectToScene(type);
        }
    }
}
