using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SecondMenuPanel : MonoBehaviour
{
    //Up panels
    public Transform earnPanel;
    public Transform rateAppPanel;
    public Transform giftPanel;
    public Transform timeToFreeGift;
    
    //Down panels
    public Transform needCoinsToUnlockCharacter;
    public Transform unlockCharacterPanel;

    public Text textTimeToGift;

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
        
    }

    public void OnPressEarn()
    {
        
    }

    public void OnPressRateApp()
    {
        
    }

    public void OnPressGift()
    {
        
    }

    public void SetTimeToGiftText(string text)
    {
        textTimeToGift.text = text;
    }


    void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}
