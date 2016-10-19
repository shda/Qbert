using UnityEngine;
using System.Collections;

public class EndPanelsLogic : MonoBehaviour 
{
    public FirstMenuPanel firstPanel;
    public SecondMenuPanel secondPanel;

    public void UpdatePanels()
    {
        firstPanel.UpdatePanels();
        secondPanel.UpdatePanels();
    }

	void Start () 
	{
	
	}

	void Update () 
	{
	
	}
}
