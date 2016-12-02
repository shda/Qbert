using System;
using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using UnityEngine.UI;

public class PreLevelAnimationGui : MonoBehaviour
{
    public AnimationToTimeOneByOne animationToTimeOneByOne;
    public Text textRoundNumber;

    public void StartShowRound(int round , Action OnEndAnimation)
    {
        textRoundNumber.text = "ROUND " + round;

        animationToTimeOneByOne.StartOneByOne();
        animationToTimeOneByOne.OnEndAnimation = OnEndAnimation;
    }

	void Start () 
	{
	
	}

	void Update () 
	{
	
	}
}
