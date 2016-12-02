using System;
using UnityEngine;
using System.Collections;

public class PreStartLevelNormalLevel : MonoBehaviour
{
    public PreLevelAnimationGui preLevelAnimationGui;

    public void StartAnimation(Action OnEnd)
    {
        preLevelAnimationGui.StartShowRound(1 , OnEnd);
    }


	void Start () 
	{
	
	}

	void Update () 
	{
	
	}
}
