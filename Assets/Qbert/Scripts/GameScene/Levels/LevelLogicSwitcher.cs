using System.Linq;
using Scripts.GameScene.GameAssets;
using UnityEngine;

namespace Scripts.GameScene.Levels
{
    public class LevelLogicSwitcher : MonoBehaviour
    {
        public GlobalConfigurationAsset globalConfiguraion;
        public LevelLogic[] levelBehaviours;
   
        public LevelController levelController;
        public int currentLevel = 0;
        public int countLevels
        {
            get { return globalConfiguraion.levelsAssets.Length; }
        }

        public LevelLogic InitLevelLoad(int level)
        {
            var configLevel = globalConfiguraion.assetLoadLevel;
            var configCurrentLevel = globalConfiguraion.levelsAssets[level];
            configLevel.typeLevel = configCurrentLevel.typeLevel;

            Color[] colorsLevel = GetInitColors(level);

            if (colorsLevel != null)
            {
                configLevel.globalLevelColors = colorsLevel;
            }

            var levelBehaviour = levelBehaviours.First(x => x.type == configLevel.typeLevel);
            levelBehaviour.SetController(levelController);
            levelBehaviour.configurationAsset = configLevel;

            return levelBehaviour;
        }

        public Color[] GetInitColors(int level)
        {
            var configLevel = globalConfiguraion.levelsAssets[level];
            if (configLevel.globalLevelColors != null)
                return configLevel.globalLevelColors;

            if(configLevel.rounds != null && configLevel.rounds.Length > 0)
                return configLevel.rounds[0].customColors;

            return null;
        }

        public LevelLogic GetLevelLogic(int level, int round)
        {
            currentLevel = level;

            var configLevel = globalConfiguraion.levelsAssets[level];

            var levelBehaviour = levelBehaviours.First(x => x.type == configLevel.typeLevel);
            levelBehaviour.SetController(levelController);
            levelBehaviour.configurationAsset = configLevel;
            levelBehaviour.SetRound(round);

            return levelBehaviour;
        }

        public bool IsCanNextLevels()
        {
            return currentLevel < countLevels - 1;
        }

        void Start ()
        {
	  
        }
	
        void Update () 
        {
	
        }
    }
}
