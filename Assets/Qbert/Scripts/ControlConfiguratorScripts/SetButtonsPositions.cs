using UnityEngine;
using System.Collections;
using System.Linq;

public class SetButtonsPositions : MonoBehaviour
{
    public Transform root;

	void Start ()
	{
	    UpdatePositions();
	}

    public void UpdatePositions()
    {
        var childs = root.GetComponentsInChildren<ButtonMove>();
        foreach (var buttonMove in childs)
        {
            buttonMove.LoadPosition();
        }
    }
	
	void Update () 
	{
	
	}
}
