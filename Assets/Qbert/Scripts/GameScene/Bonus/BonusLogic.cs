﻿using Assets.Qbert.Scripts.Bonus;
using Assets.Qbert.Scripts.GameScene.InputControl;
using Assets.Qbert.Scripts.GameScene.Levels;
using Assets.Qbert.Scripts.GameScene.PreLevel;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Bonus
{
    public class BonusLogic : MonoBehaviour
    {
        public LevelController levelController;
        public Transform[] disableBonusObjects;

        public FadeScreen fadeScreen;
        public PreStartLevel preStartLevel;

        public InputController inputController;
        public GuiButtonsController guiButtonsController;
        public Transform imageButtonPause;

        public BonusTimer timerCountdown;
        public BonusInfoWindow bonusInfoWindow;
        public WindowShowCollectedCoins windowShowCollectedCoins;

        public Assets.Qbert.Scripts.LoadScene.SelectSceneLoader sceneLoaderShowLevel;

        public CameraFallowToCharacter cameraFallowToCharacter;

        private float startCoints;

        public void Init()
        {
            if (disableBonusObjects != null)
            {
                foreach (var disableBonusObject in disableBonusObjects)
                {
                    disableBonusObject.gameObject.SetActive(false);
                }
            }
        }

        public void StartLevel()
        {
            inputController.gameObject.SetActive(false);
            imageButtonPause.gameObject.SetActive(false);

            timerCountdown.SetTimer(
                (int)levelController.levelLogicSwitcher.GetTimeScaleBonusByLevel(GlobalValues.currentLevel));

            levelController.InitBonusLevel();
            levelController.StartLevel();

            timerCountdown.iTimeScaler = levelController.gameplayObjects;
            cameraFallowToCharacter.ResizeCameraShowAllMap();

            fadeScreen.OnEnd = transform1 =>
            {
                startCoints = GlobalValues.coins;

                if (GlobalValues.isShowInfoWindowToBonusLevel)
                {
                    GlobalValues.isShowInfoWindowToBonusLevel = false;

                    bonusInfoWindow.OnClose = () =>
                    {
                        PlayBonusLevel();
                    };

                    bonusInfoWindow.ShowInfo();
                }
                else
                {
                    PlayBonusLevel();
                }
            };

            fadeScreen.StartDisable(0.5f);
        }

        private void PlayBonusLevel()
        {
            preStartLevel.OnStart(() =>
            {
                levelController.SetPauseGamplayObjects(false);
                inputController.gameObject.SetActive(true);
                imageButtonPause.gameObject.SetActive(true);
                guiButtonsController.EnableButtons();

                inputController.isEnable = true;

                timerCountdown.OnEndTimer = () =>
                {
                    levelController.SetPauseGamplayObjects(true);
                    inputController.gameObject.SetActive(false);
                    imageButtonPause.gameObject.SetActive(false);

                    GlobalValues.isBonusLevel = false;
                    EndTimer();
                };

                timerCountdown.StartTimer();
            });
        }

        public void EndTimer()
        {
            windowShowCollectedCoins.OnClose = () =>
            {
                LoadSceneShowLevel();
            };

            windowShowCollectedCoins.ShowInfo((int) (GlobalValues.coins - startCoints));
        }

        public void LoadSceneShowLevel()
        {
            inputController.isEnable = false;
            fadeScreen.OnEnd = transform1 =>
            {
                sceneLoaderShowLevel.OnLoadScene();
            };

            fadeScreen.StartEnable(0.5f);
        }
    }
}
