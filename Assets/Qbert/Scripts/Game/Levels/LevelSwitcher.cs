using UnityEngine;
using System.Collections;

public class LevelSwitcher : MonoBehaviour
{
    public LevelBase[] levels;
    public LevelController levelController;

    public int currentLevel = 0;
    public int currentRound = 0;

    public void SetLevel(int level, int round)
    {
        levelController.currentLevel = levels[level - 1];
    }

	void Start ()
	{
	   // StartLevel(currentLevel , currentRound);
	}
	
	void Update () 
	{
	
	}
}
