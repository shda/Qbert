﻿using Assets.Qbert.Scripts.GameScene;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.GUI.Button;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GUI.GUISettings
{
    public class GuiSettings : MonoBehaviour
    {
        public GuiHandle guiHandle;
        public LoadScene.SelectSceneLoader selectSceneLoader;
        public FadeScreen fadeScreen;


        public void StartGame()
        {
            UnityEngine.Debug.Log("StartGame");

            guiHandle.enabled = false;
            fadeScreen.OnEnd = transform1 =>
            {
                GlobalSettings.currentLevel = 0;
                GlobalSettings.currentRound = 0;

                selectSceneLoader.OnLoadScene();
            };

            fadeScreen.StartEnable(0.5f);
        }

        void Start()
        {
            fadeScreen.OnEnd = transform1 =>
            {
            
            };

            fadeScreen.StartDisable(1.0f);
        }
    }
}
