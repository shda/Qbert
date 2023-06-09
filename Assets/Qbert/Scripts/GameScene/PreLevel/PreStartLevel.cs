﻿using System;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.PreLevel
{
    public class PreStartLevel : MonoBehaviour
    {
        public CameraFallowToCharacter cameraFallowToCharacter;
        public PreStartLevelNormalLevel preStartLevelNormalLevel;

        public void OnStart(Action OnEnd)
        {
            if (GlobalValues.isBonusLevel)
            {
                ShowBonus(OnEnd);
            }
            else
            {
                RoundShow(OnEnd);
            }
        }

        public void ShowBonus(Action OnEnd)
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
                        preStartLevelNormalLevel.StartBonusAnimation(() =>
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


        public void RoundShow(Action OnEnd)
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
}
