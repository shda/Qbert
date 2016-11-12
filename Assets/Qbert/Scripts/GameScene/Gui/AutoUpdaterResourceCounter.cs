using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts;
using Assets.Qbert.Scripts.GameScene.Gui;
using UnityEngine.Networking;

public class AutoUpdaterResourceCounter : MonoBehaviour
{
    public ResourceCounter resourceCounter;

    void Start () 
	{
	
	}

	void Update () 
	{
        resourceCounter.SetValue(GlobalValues.coins);
    }
}
