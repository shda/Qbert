using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameGui : MonoBehaviour
{
    public Text scoreText;
    public Text levelLabel;

    private float score;

    public void ResetScore()
    {
        SetScore(0);
    }
    public void SetLevel(int level , int round)
    {
        levelLabel.text = string.Format("{0}-{1}", level + 1, round + 1);
    }
    public void SetScore(float setScore)
    {
        score = setScore;
        scoreText.text = string.Format("{0}", (int)setScore);
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
