using System;
using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene;
using Assets.Qbert.Scripts.Utils;

public class PreStartLevel : MonoBehaviour
{
    public CameraFallowToCharacter cameraFallowToCharacter;
    public PreStartLevelNormalLevel preStartLevelNormalLevel;
    public PreStartLevelBonus preStartLevelBonus;

    public void OnStart(Action OnEnd)
    {
        //Wait for second
        StartCoroutine(this.WaitForSecondCallback(1.0f, transform1 =>
        {
            //Move camera to character
            cameraFallowToCharacter.StartResizeCameraSizeToCharacter(() =>
            {
                StartCoroutine(this.WaitForSecondCallback(0.2f, transform2 =>
                {
                    //Show label round number and show change to
                    preStartLevelNormalLevel.StartAnimation(() =>
                    {
                        cameraFallowToCharacter.StareFallow();
                        if (OnEnd != null)
                        {
                            OnEnd();
                        }
                    });
                }));
            });
        }));

    }

    void Start () 
	{
	
	}

	void Update () 
	{
	
	}
}
