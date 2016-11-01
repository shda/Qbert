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
        giftGoldAnimator.OnPressVideoToGift = OnPressVideoToGift;

        UpdateNextTimeToGift();

        if (!GlobalValues.isShowGiftDoubleFromVideo)
        {
            GlobalValues.isShowGiftDoubleFromVideo = true;
            GlobalValues.Save();
            giftGoldAnimator.showDoubleGift = true;
        }
        else
        {
            GlobalValues.isShowGiftDoubleFromVideo = false;
            GlobalValues.Save();
        }

        currentCoinsCount.SetValueForce(GlobalValues.coins);
        addGoldCoinsCount.SetValueForce(50);
        giftGoldAnimator.GiftDropToGround();
    }

    public void UpdateNextTimeToGift()
    {
        nextGiftTime.text = string.Format("{0}h",
            GlobalValues.GetNextTimeGift());
    }

    private void OnPressVideoToGift(bool isOkVideo)
    {
        if (isOkVideo)
        {
            giftGoldAnimator.showDoubleGift = false;
            currentCoinsCount.SetValueForce(GlobalValues.coins);
            addGoldCoinsCount.SetValueForce(50);
            giftGoldAnimator.GiftDropToGround();
        }
        else
        {
            OnEndGiftAction();
        }
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
