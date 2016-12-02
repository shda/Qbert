using System;
using UnityEngine;
using System.Collections;
using Assets.Qbert.Scripts.GameScene;

public class PreStartLevel : MonoBehaviour
{
    public CameraFallowToCharacter cameraFallowToCharacter;
    public PreStartLevelNormalLevel preStartLevelNormalLevel;
    public PreStartLevelBonus preStartLevelBonus;

    public void OnStart(Action OnEnd)
    {
        /*
        cameraFallowToCharacter.StartResizeCameraSizeToCharacter(() =>
        {
            preStartLevelNormalLevel.StartAnimation(() =>
            {
                cameraFallowToCharacter.StareFallow();
                if (OnEnd != null)
                {
                    OnEnd();
                }
            });

            

            
        });
        */

        
        preStartLevelNormalLevel.StartAnimation(() =>
        {
            cameraFallowToCharacter.StartResizeCameraSizeToCharacter(() =>
            {
                cameraFallowToCharacter.StareFallow();

                if (OnEnd != null)
                {
                    OnEnd();
                }
            });
        });
        
    }

    void Start () 
	{
	
	}

	void Update () 
	{
	
	}
}
