using System.Collections;
using System.Collections.Generic;
using Assets.Qbert.Scripts;
using Assets.Qbert.Scripts.GameScene;
using Assets.Qbert.Scripts.GameScene.InputControl;
using Assets.Qbert.Scripts.GameScene.Levels;
using UnityEngine;
using UnityEngine.UI;

public class BonusLogic : MonoBehaviour
{
    public LevelController levelController;
    public Transform[] disableBonusObjects;

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
    /*
    public void StartLevel(GameScene gameScene)
    {
        gameScene.inputController.gameObject.SetActive(false);
        gameScene.imageButtonPause.gameObject.SetActive(false);

        gameScene.timerCountdown.SetTimer(5);

        gameScene.StartBonus();

        gameScene.timerCountdown.iTimeScaler = levelController.gameplayObjects;
        // levelController.SetPauseGamplayObjects(true);
        gameScene.cameraFallowToCharacter.ResizeCameraShowAllMap();

        gameScene.fadeScreen.OnEnd = transform1 =>
        {
            if (GlobalValues.isShowInfoWindowToBonusLevel)
            {
                GlobalValues.isShowInfoWindowToBonusLevel = false;

                gameScene.bonusInfoWindow.OnClose = () =>
                {
                    PlayBonusLevel();
                };

                gameScene.bonusInfoWindow.ShowInfo();
            }
            else
            {
                gameScene.PlayBonusLevel();
            }
        };

        gameScene.fadeScreen.StartDisable(0.5f);
    }

    private void PlayBonusLevel(GameScene gameScene)
    {
        preStartLevel.OnStart(() =>
        {
            levelController.SetPauseGamplayObjects(false);
            inputController.gameObject.SetActive(true);
            imageButtonPause.gameObject.SetActive(true);
            guiButtonsController.EnableButtons();


            inputController.isEnable = true;

            bonusLogic.StartLevel(this);

            timerCountdown.OnEndTimer = () =>
            {
                levelController.SetPauseGamplayObjects(true);
                inputController.gameObject.SetActive(false);
                imageButtonPause.gameObject.SetActive(false);

                GlobalValues.isBonusLevel = false;

                LoadSceneShowLevel();
            };

            //timerCountdown.StartTimer();
        });
    }
    */
}
