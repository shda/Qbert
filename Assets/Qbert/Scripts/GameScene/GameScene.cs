using Scripts.GameScene.InputControl;
using Scripts.GameScene.Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.GameScene
{
    public class GameScene : MonoBehaviour
    {
        public LevelController      levelController;
        public FadeScreen           fadeScreen;

        public InputController  inputController;

        public LoadScene.SelectSceneLoader sceneLoaderShowLevel;
        public LoadScene.SelectSceneLoader sceneLoaderMainMenu;

        public void RestartLevel()
        {
            levelController.RestartLevel();
        }

        public void StartGame()
        {
            levelController.InitLevel(GlobalSettings.currentLevel, GlobalSettings.currentRound);
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

            fadeScreen.OnEnd = transform1 =>
            {
                levelController.SetPauseGamplayObjects(false);
                inputController.isEnable = true;
            };

            fadeScreen.StartDisable(0.5f);
        }

        void Update()
        {

        }
    }
}
