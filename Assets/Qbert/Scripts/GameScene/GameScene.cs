using Assets.Qbert.Scripts.GameScene.InputControl;
using Assets.Qbert.Scripts.GameScene.Levels;
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

        public BonusTimer timerCountdown;
        public BonusInfoWindow bonusInfoWindow;

        public CameraFallowToCharacter cameraFallowToCharacter;

        public void RestartLevel()
        {
            levelController.RestartLevel();
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
                preStartLevel.OnStart(() =>
                {
                    levelController.SetPauseGamplayObjects(false);
                    inputController.gameObject.SetActive(true);
                    guiButtonsController.EnableButtons();

                    imageButtonPause.gameObject.SetActive(true);
                    inputController.isEnable = true;
                });
            };

            fadeScreen.StartDisable(0.5f);
        }

        void StartBonusLevel()
        {
            bonusLogic.Init();

            inputController.gameObject.SetActive(false);
            imageButtonPause.gameObject.SetActive(false);

            timerCountdown.SetTimer(5);

            StartBonus();

            timerCountdown.iTimeScaler = levelController.gameplayObjects;
           // levelController.SetPauseGamplayObjects(true);
            cameraFallowToCharacter.ResizeCameraShowAllMap();

            fadeScreen.OnEnd = transform1 =>
            {
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

               // bonusLogic.StartLevel(this);

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

        public void StartBonus()
        {
            levelController.InitBonusLevel();
            levelController.StartLevel();
        }

        void Update()
        {

        }
    }
}
