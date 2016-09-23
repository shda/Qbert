using UnityEngine;
using System.Collections;
using Scripts;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InfoText : MonoBehaviour
{
    public Text coinsText;
    public Text scoreText;
    public Text levelText;

    public void UpdateInfo()
    {
        if(coinsText)
            coinsText.text = "" + GlobalSettings.coins;

        if (scoreText)
            scoreText.text = "" + GlobalSettings.score;

        if (levelText)
            levelText.text = string.Format("level {0}-{1}", 
                GlobalSettings.currentLevel + 1,
                GlobalSettings.currentRound + 1);
    }

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
