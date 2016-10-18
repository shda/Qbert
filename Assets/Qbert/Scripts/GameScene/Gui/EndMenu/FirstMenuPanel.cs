using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FirstMenuPanel : MonoBehaviour
{
    public Transform watchwVideoToContinue;
    public Transform watchwVideoToContinueDisable;

    public Text textCountCoinsToCOntinueGame;

    public void OnPressButtonWatchVideo()
    {

    }

    public void OnPressButtonInvestToContinueGame()
    {

    }


    void Start ()
    {
        watchwVideoToContinue.gameObject.SetActive(false);
    }
	
	void Update ()
    {
	
	}
}
