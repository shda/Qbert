using UnityEngine;
using System.Collections;
using System.Linq;

public class LevelSwitcher : MonoBehaviour
{
    public GlobalConfigurationAsset globalConfiguraion;
    public LevelBehaviour[] levelBehaviours;
   
    public LevelController levelController;
    public int currentLevel = 0;
    public int countLevels
    {
        get { return globalConfiguraion.levelsAssets.Length; }
    }

    public LevelBehaviour InitLevelLoad(int level)
    {
        var configLevel = globalConfiguraion.assetLoadLevel;
        var levelBehaviour = levelBehaviours[level];
        levelBehaviour.SetController(levelController);
        levelBehaviour.configurationAsset = configLevel;

        return levelBehaviour;
    }

    public LevelBehaviour SetLevel(int level, int round)
    {
        currentLevel = level;

        var configLevel = globalConfiguraion.levelsAssets[level];

        var levelBehaviour = levelBehaviours.First(x => x.type == configLevel.typeLevel);
        levelBehaviour.SetController(levelController);
        levelBehaviour.configurationAsset = configLevel;
        levelBehaviour.SetRound(round);

        return levelBehaviour;
    }

    public void NextLevel()
    {
        if (currentLevel < countLevels - 1)
        {
            currentLevel++;
        }
        else
        {
            levelController.EndLevels();
            return;
        }

        levelController.InitLevel(currentLevel , 0);
    }

	void Start ()
	{
	  
	}
	
	void Update () 
	{
	
	}
}
