using Assets.Qbert.Scripts.GameScene.InputControl;
using Assets.Qbert.Scripts.GameScene.Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Qbert.Scripts.GameScene
{
    public class GameScene : MonoBehaviour
    {
        public LevelController      levelController;
        public FadeScreen           fadeScreen;

        public InputController  inputController;

        public LoadScene.SelectSceneLoader SceneLoaderShowLevel;
        public LoadScene.SelectSceneLoader SceneLoaderMainMenu;


        public void RestartLevel()
        {
            levelController.RestartLevel();
        }

        public void StartGame()
        {
            levelController.ResetScore();
            levelController.InitLevel(GlobalSettings.currentLevel, GlobalSettings.currentRound);
            levelController.StartLevel();
        }

        public void LoadSceneShowLevel()
        {
            inputController.isEnable = false;
            fadeScreen.OnEnd = transform1 =>
            {
                SceneLoaderShowLevel.OnLoadScene();
            };

            fadeScreen.StartEnable(0.5f);
        }


        public void LoadMainScene()
        {
            inputController.isEnable = false;
            fadeScreen.OnEnd = transform1 =>
            {
                SceneLoaderMainMenu.OnLoadScene();
            };

            fadeScreen.StartEnable(0.5f);
        }

        public void OnPressSelectCharacter()
        { }


        public string sceneName;
        public void LoadSelectLevelScene()
        {
            SceneManager.LoadScene(sceneName);
        }

        void Awake()
        {
            // QualitySettings.vSyncCount = 0;
            // Application.targetFrameRate = 10;
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
