using Assets.Qbert.Scripts.GameScene.Bonus;
using Assets.Qbert.Scripts.GameScene.InputControl;
using Assets.Qbert.Scripts.GameScene.Levels;
using Assets.Qbert.Scripts.GameScene.PreLevel;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.GameScene
{
    public class GameScene : MonoBehaviour
    {
        public LevelController  levelController;
        public FadeScreen   fadeScreen;
        public PreStartLevel preStartLevel;

        public InputController  inputController;
        public GuiButtonsController guiButtonsController;
        public Transform imageButtonPause;

        public BonusLogic bonusLogic;
        public Transform bonusTimerTransform;

        public LoadScene.SelectSceneLoader sceneLoaderShowLevel;
        public LoadScene.SelectSceneLoader sceneLoaderMainMenu;
        public LoadScene.SelectSceneLoader sceneLoaderLevel;

        public CameraFallowToCharacter cameraFallowToCharacter;

        public void RestartLevel()
        {
            fadeScreen.OnEnd = transform1 =>
            {
                sceneLoaderLevel.OnLoadScene();
            };

            fadeScreen.StartEnable(0.5f);

           // levelController.RestartLevel();
        }

        public void StartGame()
        {
            levelController.InitLevel(GlobalValues.currentLevel, GlobalValues.currentRound);
            levelController.StartLevel();
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

        public void ReloadScene()
        {
            inputController.isEnable = false;
            fadeScreen.OnEnd = transform1 =>
            {
                sceneLoaderLevel.OnLoadScene();
            };

            fadeScreen.StartEnable(0.5f);
        }

        public void LoadMainScene()
        {
            inputController.isEnable = false;
            fadeScreen.OnEnd = transform1 =>
            {
                sceneLoaderMainMenu.OnLoadScene();
            };

            fadeScreen.StartEnable(0.5f);
        }

        void Awake()
        {

        }

        void Start()
        {
            if (GlobalValues.isBonusLevel)
            {
                StartBonusLevel();
            }
            else
            {
                StartLevel();
            }
        }

        void StartLevel()
        {
            StartGame();

            bonusTimerTransform.gameObject.SetActive(false);
            levelController.SetPauseGamplayObjects(true);
            inputController.gameObject.SetActive(false);
            imageButtonPause.gameObject.SetActive(false);

            cameraFallowToCharacter.ResizeCameraShowAllMap();

            
            fadeScreen.OnEnd = transform1 =>
            {
                //StartLevelIn();
                
                preStartLevel.OnStart(() =>
                {
                    StartLevelIn();
                });
                
            };

            fadeScreen.StartDisable(0.5f);
        }


        void StartLevelIn()
        {
            levelController.SetPauseGamplayObjects(false);
            inputController.gameObject.SetActive(true);
            guiButtonsController.EnableButtons();

            imageButtonPause.gameObject.SetActive(true);
            inputController.isEnable = true;
        }

        void StartBonusLevel()
        {
            bonusLogic.Init();
            bonusLogic.StartLevel();
        }
    }
}
