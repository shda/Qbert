using UnityEngine;
using System.Collections;

public class LevelSwitcher : MonoBehaviour
{
    public LevelBase[] levels;
    public LevelController levelController;

    public int currentLevel = 0;

    public int countLevels
    {
        get { return levels.Length; }
    }

    public void SetLevel(int level, int round)
    {
        currentLevel = level;
        levelController.currentLevel = levels[level];
        levelController.currentLevel.SetRound(round);
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
            return;;
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
