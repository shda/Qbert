using System;
using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using UnityEngine.UI;

public class FirstMenuPanel : BasePanel
{
    public Transform watchwVideoToContinue;
    public Transform watchwVideoToContinueDisable;

    public Text textCountCoinsToContinueGame;

    private bool isWatchAdPress = false;
    private int  counterInvestPress = 0;

    public AnimationToTimeMassive showFirstMenu;

    public IEnumerator AnimatedShowPanel()
    {
        yield return StartCoroutine(showFirstMenu.PlayToTime(0.5f));
    }

    public IEnumerator AnimatedHidePanel(float timeShow)
    {
        yield return StartCoroutine(showFirstMenu.PlayToTime(timeShow, null, true));
    }

    public void OnPressButtonWatchVideo()
    {
        if(isPress)
            return;

        Debug.Log("PressMy");

        UpdatePanels();

        isWatchAdPress = true;
        DisablePressButtons();
    }

    public void OnPressButtonInvestToContinueGame()
    {
        if (isPress)
            return;

        counterInvestPress++;

        var cast = GlobalValues.castCoinsInvest;
        counterInvestPress = Mathf.Clamp(counterInvestPress , 0, cast.Length);
        textCountCoinsToContinueGame.text = cast[counterInvestPress].ToString();

        DisablePressButtons();
    }

    public override void UpdatePanels()
    {
        ShowWatchVideoEnable(!isWatchAdPress);
        base.UpdatePanels();
    }

    private void ShowWatchVideoEnable(bool enable)
    {
        watchwVideoToContinue.gameObject.SetActive(enable);
        watchwVideoToContinueDisable.gameObject.SetActive(!enable);
    }

    void Start ()
    {
        ShowWatchVideoEnable(false);
    }
	
	void Update ()
    {
	
	}
}
