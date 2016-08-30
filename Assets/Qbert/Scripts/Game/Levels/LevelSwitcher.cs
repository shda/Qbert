using UnityEngine;
using System.Collections;

public class LevelSwitcher : MonoBehaviour
{
    public LevelBase[] levels;
    public LevelController levelController;

    public int currentLevel = 0;
    public int currentRound = 0;

    public void StartLevel(int level, int round)
    {
        levelController.currentLevel = levels[level];
        levelController.RestartLevel();
    }

	void Start ()
	{
	    StartLevel(currentLevel , currentRound);
	}
	
	void Update () 
	{
	
	}
}
