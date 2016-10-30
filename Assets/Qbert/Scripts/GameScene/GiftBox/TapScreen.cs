using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TapScreen : MonoBehaviour , IPointerClickHandler
{
    public Action OnTapScreen;

	void Start () 
	{
	
	}

	void Update () 
	{
	
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnTapScreen != null)
        {
            OnTapScreen();
        }
    }
}
