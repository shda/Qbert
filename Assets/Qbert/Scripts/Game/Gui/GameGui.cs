using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameGui : MonoBehaviour
{
    public Text scoreText;
    public Text levelLabel;

    private int score;

    public void ResetScore()
    {
        SetScore(0);
    }
    public void SetLevel(int level , int round)
    {
        levelLabel.text = string.Format("{0}-{1}", level, round);
    }
    public void SetScore(int setScore)
    {
        score = setScore;
        scoreText.text = setScore.ToString();
    }

    public void AddScore(int addScore)
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
