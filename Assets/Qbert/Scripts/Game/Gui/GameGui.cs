using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameGui : MonoBehaviour
{
    public ResourceCounter scoreText;
    public Text levelLabel;
    public Text coinsLabel;

    private float score;
    private int coins;

    public void ResetScore()
    {
        scoreText._labelCount = 0;
        scoreText.UpdateText();
        SetScore(0);
    }
    public void SetLevel(int level , int round)
    {
        levelLabel.text = string.Format("{0}-{1}", level + 1, round + 1);
    }
    public void SetScore(float setScore)
    {
        score = setScore;
        scoreText.SetValue(setScore);
    }

    public void SetCoins(int setConis)
    {
        coinsLabel.text = string.Format("{0}", (int)setConis);
    }

    public void AddCoins(int addScore)
    {
        coins += addScore;
        SetCoins(coins);
    }

    public void AddScore(float addScore)
    {
        score += addScore;
        SetScore(score);
    }
	void Start () 
	{
	    
	}
	
	void Update () 
	{
	
	}
}
