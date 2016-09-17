using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameGui : MonoBehaviour
{
    public ResourceCounter scoreText;
    public Text levelLabel;
    public Text coinsLabel;

    public void UpdateScore()
    {
        scoreText._labelCount = GlobalSettings.score;
        scoreText.UpdateText();
        SetScore(GlobalSettings.score);
    }
    public void SetLevel(int level , int round)
    {
        levelLabel.text = string.Format("{0}-{1}", level + 1, round + 1);
    }
    public void SetScore(float setScore)
    {
        GlobalSettings.score = setScore;
        scoreText.SetValue(setScore);
    }

    public void SetCoins(int setConis)
    {
        coinsLabel.text = string.Format("{0}", (int)setConis);
    }

    public void AddCoins(int addCoins)
    {
        GlobalSettings.coins += addCoins;
        SetCoins(GlobalSettings.coins);
    }

    public void AddScore(float addScore)
    {
        GlobalSettings.score += addScore;
        SetScore(GlobalSettings.score);
    }
	void Start () 
	{
	    
	}
	
	void Update () 
	{
	
	}
}
