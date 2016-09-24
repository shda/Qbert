using UnityEngine;
using System.Collections;

public class DebugLog : MonoBehaviour
{
    public string toLog = "DebugLog";

    public void OnLog()
    {
        Debug.Log(name + " + " + toLog);
    }


    void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
