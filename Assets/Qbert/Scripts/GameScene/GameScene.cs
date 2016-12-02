using Assets.Qbert.Scripts.GameScene.InputControl;
using Assets.Qbert.Scripts.GameScene.Levels;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene
{
    public class GameScene : MonoBehaviour
    {
        public LevelController  levelController;
        public FadeScreen   fadeScreen;
        public PreStartLevel preStartLevel;

        public InputController  inputController;

        public LoadScene.SelectSceneLoader sceneLoaderShowLevel;
        public LoadScene.SelectSceneLoader sceneLoaderMainMenu;

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
            StartGame();
            levelController.SetPauseGamplayObjects(true);
            inputController.isEnable = false;

            cameraFallowToCharacter.ResizeCameraShowAllMap();

            fadeScreen.OnEnd = transform1 =>
            {
                preStartLevel.OnStart(() =>
                {
                    levelController.SetPauseGamplayObjects(false);
                    inputController.isEnable = true;
                });
            };

            fadeScreen.StartDisable(0.5f);
        }

        void Update()
        {

        }
    }
}
