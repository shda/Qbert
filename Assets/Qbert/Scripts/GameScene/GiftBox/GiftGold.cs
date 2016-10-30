﻿using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GameScene.Gui;
using UnityEngine.UI;

public class GiftGold : MonoBehaviour
{
    public GiftGoldAnimator giftGoldAnimator;

    public ResourceCounter currentCoinsCount;
    public ResourceCounter addGoldCoinsCount;
    public Text nextGiftTime;

    public void ShowGift()
    {
        currentCoinsCount.SetValueForce(GlobalValues.coins);
        addGoldCoinsCount.SetValueForce(50);

        giftGoldAnimator.GiftDropToGround();
    }

	void Start () 
	{
	    Invoke("ShowGift" , 2.0f);
	}

	void Update () 
	{
	
	}
}
