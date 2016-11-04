using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;

public class GiftCharacter : MonoBehaviour
{
    public AnimationToTimeOneByOne animationShowCnaracter;

	void Start ()
	{
	    animationShowCnaracter.StartOneByOne();
	}

	void Update () 
	{
	
	}
}
