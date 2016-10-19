﻿using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SecondMenuPanel : BasePanel
{
    //Up panels
    public Transform earnPanel;
    public Transform rateAppPanel;
    public Transform giftPanel;
    public Transform timeToFreeGift;
    
    //Down panels
    public Transform needCoinsToUnlockCharacter;
    public Transform unlockCharacterPanel;

    public Text neerCountCoinsUnlockCharacter;

    public Text textTimeToGift;
    public Text lastScoreText;
    public Text bestScoreText;

    public void DisableAllPanels()
    {
        var array = new Transform[]
        {
            earnPanel ,
            rateAppPanel ,
            giftPanel,
            timeToFreeGift ,
            needCoinsToUnlockCharacter,
            unlockCharacterPanel
        };

        foreach (var transform1 in array)
        {
            transform1.gameObject.SetActive(false);
        }
    }

    public void OnPressUnlockCharacter()
    {
        if(isPress)
            return;

        DisablePressButtons();
    }

    public void OnPressEarn()
    {
        if (isPress)
            return;

        DisablePressButtons();
    }

    public void OnPressRateApp()
    {
        if (isPress)
            return;


        DisablePressButtons();
    }

    public void OnPressGift()
    {
        if (isPress)
            return;

        GlobalValues.timeInGame = 0;
        GlobalValues.giftTimeIndex++;
        GlobalValues.Save();

        DisablePressButtons();
    }

    public void SetTimeToGiftText(string text)
    {
        textTimeToGift.text = text;
    }

    public override void UpdatePanels()
    {
        SetScores();
        UpdateUpPanel();
        UpdateDownPanel();
        base.UpdatePanels();
    }

    private void UpdateDownPanel()
    {
        if (GlobalValues.coins > GlobalValues.countCoinsToUnlockChar)
        {
            needCoinsToUnlockCharacter.gameObject.SetActive(false);
            unlockCharacterPanel.gameObject.SetActive(true);
        }
        else
        {
            neerCountCoinsUnlockCharacter.text = GlobalValues.countCoinsToUnlockChar.ToString();
            needCoinsToUnlockCharacter.gameObject.SetActive(true);
            unlockCharacterPanel.gameObject.SetActive(false);
        }
    }

    private void UpdateUpPanel()
    {
        int rand = Random.Range(0, 3);

        DisableAllPanels();

        switch (rand)
        {
            //Gift
            case 0:
                EnableGiftPanel();
                break;
            //Earn
            case 1:
                EnableTObjest(earnPanel);
                break;
            //RateApp
            case 2:
                EnableTObjest(rateAppPanel);
                break;
            default:
                EnableTObjest(rateAppPanel);
                break;
        }
    }

    private void EnableGiftPanel()
    {
        var timesNeedToGift = GlobalValues.timeInGameGift;

        var giftTimeIndex = GlobalValues.giftTimeIndex;
        giftTimeIndex = Mathf.Clamp(0, timesNeedToGift.Length, giftTimeIndex);

        if (timesNeedToGift[giftTimeIndex] < GlobalValues.timeInGame)
        {
            EnableTObjest(giftPanel);
        }
        else
        {
            textTimeToGift.text = timesNeedToGift[giftTimeIndex].ToString();
            EnableTObjest(timeToFreeGift);
        }
    }

    private void EnableTObjest(Transform tr)
    {
        tr.gameObject.SetActive(true);
    }

    private void SetScores()
    {
        float bestScore = GlobalValues.bestScore;
        float currentScore = GlobalValues.score;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            GlobalValues.bestScore = bestScore;
        }

        lastScoreText.text = currentScore.ToString();
        bestScoreText.text = bestScore.ToString();
    }

    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}
