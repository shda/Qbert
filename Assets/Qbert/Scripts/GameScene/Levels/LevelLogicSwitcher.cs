using System.Collections;
using System.Linq;
using Assets.Qbert.Scripts.GameScene.GameAssets;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Levels
{
    public class LevelLogicSwitcher : MonoBehaviour
    {
        
        public LevelLogic[] levelBehaviours;
   
        public LevelController levelController;
        public int currentLevel = 0;

        /*
        public int countLevels
        {
            get { return globalConfiguraion.levelsAssets.Length; }
        }
        */
        public LevelLogic InitLevelLoad(int level)
        {
            var configLevel = levelController.globalConfiguraion.assetLoadLevel;
            var configCurrentLevel = GetLevelAssetByLevel(level);
            configLevel.typeLevel = configCurrentLevel.typeLevel;

            Color[] colorsLevel = GetInitColors(level);

            /*
            if (colorsLevel != null)
            {
                configLevel.globalLevelColors = colorsLevel;
            }
            */

            var levelBehaviour = levelBehaviours.First(x => x.type == configLevel.typeLevel);
            levelBehaviour.SetController(levelController);
            levelBehaviour.configurationAsset = configLevel;

            return levelBehaviour;
        }

        public Color[] GetInitColors(int level)
        {
            var configLevel = GetLevelAssetByLevel(level);
            if (configLevel.globalLevelColors != null)
                return configLevel.globalLevelColors.GetValue().colors;

            if(configLevel.rounds != null && configLevel.rounds.Length > 0)
                return configLevel.rounds[0].customColors;

            return null;
        }

        public LevelLogic GetLevelLogic(int level, int round)
        {
            currentLevel = level;

            var configLevel = GetLevelAssetByLevel(level);

            var levelBehaviour = levelBehaviours.First(x => x.type == configLevel.typeLevel);
            levelBehaviour.SetController(levelController);
            levelBehaviour.configurationAsset = configLevel;
            //levelBehaviour.SetRound(round);

            return levelBehaviour;
        }

        public LevelLogic GetBonusLogic()
        {
            var configLevel = levelController.globalConfiguraion.assetBonusLevels;

            var levelBehaviour = levelBehaviours.First(x => x.type == configLevel.typeLevel);
            levelBehaviour.SetController(levelController);
            levelBehaviour.configurationAsset = configLevel;

            return levelBehaviour;
        }

        public bool IsCanNextLevels()
        {
            return true; //currentLevel < countLevels - 1;
        }

        public LevelConfigAsset GetLevelAssetByLevel(int level)
        {
            float timeScale = 1.0f;
            CyclicLevelsAsset.StepCyclic findStepCyclic;
            LevelConfigAsset levelConfigAsset;

            levelController.globalConfiguraion.cyclicLevelsAsset.GetStepCyclicByLevel(level,
                ref timeScale, out findStepCyclic, out levelConfigAsset);

            return levelConfigAsset;
        }

        public float GetTimeScaleByLevel(int level)
        {
            float timeScale = 1.0f;
            CyclicLevelsAsset.StepCyclic findStepCyclic;
            LevelConfigAsset levelConfigAsset;

            levelController.globalConfiguraion.cyclicLevelsAsset.GetStepCyclicByLevel(level,
                ref timeScale, out findStepCyclic, out levelConfigAsset);

            return timeScale;
        }

        public MapAsset GetBonusMapByLevel(int level)
        {
            return levelController.globalConfiguraion.cyclicLevelsAsset.GetBonusMapByLevel(level);
        }

        public float GetTimeScaleBonusByLevel(int level)
        {
            return levelController.globalConfiguraion.cyclicLevelsAsset.GetTimeScaleToBonusMapByLevel(level);
        }

        void Start ()
        {
            //StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                float timeScale = 1.0f;
                CyclicLevelsAsset.StepCyclic findStepCyclic;
                LevelConfigAsset levelConfigAsset;

                levelController.globalConfiguraion.cyclicLevelsAsset.GetStepCyclicByLevel(level,
                    ref timeScale, out findStepCyclic, out levelConfigAsset);

                Debug.Log(levelConfigAsset.typeLevel + " ___ " + timeScale);

                level++;
            }
        }

        public int level;

        void Update ()
        {
            
        }
    }
}
