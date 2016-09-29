using System;
using UnityEngine;
using System.Collections;

public class ControlConfigurator : MonoBehaviour
{
    public ButtonMove[] buttons;
    public Transform buttonSave;
    public Transform textDetectIntersectionButtons;

    private void ConnectEvents()
    {
        foreach (var buttonMove in buttons)
        {
            buttonMove.OnButtonStartDrag = OnButtonStartDrag;
            buttonMove.OnButtonEndDrag = OnButtonEndDrag;
            buttonMove.OnButtonDrag = OnButtonDrag;
        }
    }

    private void OnButtonDrag(ButtonMove buttonMove)
    {
        CheckIntersectButtons();
    }

    public void OnSave()
    {
        foreach (var buttonMove in buttons)
        {
            buttonMove.SavePosition();
        }
    }

    public void OnCancel()
    {
       
    }

    public void LoadButtonsPositions()
    {
        foreach (var buttonMove in buttons)
        {
            buttonMove.LoadPosition();
        }
    }

    public void OnDefault()
    {
        foreach (var buttonMove in buttons)
        {
            buttonMove.SetButtonToDefaultPosition();
        }

        CheckIntersectButtons();
    }

    private void OnButtonEndDrag(ButtonMove buttonMove)
    {
        CheckIntersectButtons();
    }
    public void CheckIntersectButtons()
    {
        bool isIntersectButtons = false;

        foreach (var button in buttons)
        {
            button.ResetIntersect();
        }

        foreach (var button in buttons)
        {
            foreach (var button2 in buttons)
            {
                if(button2 == button)
                    continue;

                if (button.bounds.Intersects(button2.bounds))
                {
                    button.SetIntersect();
                    button2.SetIntersect();
                    isIntersectButtons = true;
                }
            }
        }

        if (isIntersectButtons)
        {
            ShowTextDetectIntersectionButtons();
        }
        else
        {
            HideTextDetectIntersectionButtons();
        }
    }

    private void OnButtonStartDrag(ButtonMove buttonMove)
    {
        
    }

    public void ShowTextDetectIntersectionButtons()
    {
        textDetectIntersectionButtons.gameObject.SetActive(true);
        buttonSave.gameObject.SetActive(false);
    }

    public void HideTextDetectIntersectionButtons()
    {
        textDetectIntersectionButtons.gameObject.SetActive(false);
        buttonSave.gameObject.SetActive(true);
    }

    void Start ()
    {
        StartCoroutine(DelayRun());
    }

    IEnumerator DelayRun()
    {
        yield return new WaitForEndOfFrame();

        HideTextDetectIntersectionButtons();
        ConnectEvents();
        LoadButtonsPositions();
    }
	
	void Update () 
	{
	
	}
}
