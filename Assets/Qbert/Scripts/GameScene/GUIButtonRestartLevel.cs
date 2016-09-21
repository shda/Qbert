using UnityEngine;
using System.Collections;

public class GUIButtonRestartLevel : MonoBehaviour
{
    public void OnRestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
	void Start () 
	{
	
	}
	void Update () 
	{
	
	}
}
