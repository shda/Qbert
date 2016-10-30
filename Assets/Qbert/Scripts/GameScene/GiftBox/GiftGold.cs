using System;
using UnityEngine;
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

    public Action OnEndGiftAction;

    public void ShowGift(Action OnEndGiftAction)
    {
        this.OnEndGiftAction = OnEndGiftAction;
        gameObject.SetActive(true);

        giftGoldAnimator.OnEndGift = OnEndGift;

        currentCoinsCount.SetValueForce(GlobalValues.coins);
        addGoldCoinsCount.SetValueForce(50);

        giftGoldAnimator.GiftDropToGround();
    }

    private void OnEndGift()
    {
        if (OnEndGiftAction != null)
        {
            OnEndGiftAction();
        }

        //Invoke("ShowGift", 2.0f);
    }

    void Start () 
	{
	    //Invoke("ShowGift" , 3.0f);
	}

	void Update () 
	{
	
	}
}
