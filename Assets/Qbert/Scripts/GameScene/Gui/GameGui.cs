using Assets.Qbert.Scripts.GameScene.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.GameScene.Gui
{
    public class GameGui : MonoBehaviour
    {
        public ResourceCounter scoreText;
        public Text levelLabel;
        public Text coinsLabel;
        public CubeChangeTo cubeChangeTo;
        public Lives2d lives;
        public EndGameGui endGameGui;
        //public Transform gameOver;
        //public InfoText infoText;
        public MenuPause menuPause;
        public LevelController levelController;
        
        public void OnPressButtonPause()
        {
            menuPause.Show();

            levelController.SetPauseGamplayObjects(true);
            levelController.SetPauseQbert(true);
            levelController.inputController.isEnable = false;
            levelController.inputController.HideButtons();

            menuPause.OnCompliteGame = () =>
            {
                ShowEndGame();
            };

            menuPause.OnResumeGame = () =>
            {
                levelController.SetPauseGamplayObjects(false);
                levelController.SetPauseQbert(false);
                levelController.inputController.isEnable = true;
                levelController.inputController.ShowButtons();
            };
        }

        public void ShowGameOver()
        {
           // infoText.UpdateInfo();
            endGameGui.ShowGameOver();
        }

        public void ShowEndGame()
        {
           // infoText.UpdateInfo();

            endGameGui.OnShowSecondRails();
        }

        public void SetColorCube(Color color)
        {
            cubeChangeTo.SetColor(color);
        }

        public void UpdateLiveCount()
        {
            lives.SetLiveCount(GlobalSettings.countLive);
        }

        public void UpdateScore()
        {
            scoreText._labelCount = GlobalSettings.score;
            scoreText.UpdateText();
            SetScore(GlobalSettings.score);
        }
        public void SetLevelNumber(int level , int round)
        {
            levelLabel.text = string.Format("{0}-{1}", level + 1, round + 1);
        }
        public void SetScore(float setScore)
        {
            GlobalSettings.score = setScore;
            scoreText.SetValue(setScore);
        }

        public void SetCoins(int setConis)
        {
            coinsLabel.text = string.Format("{0}", (int)setConis);
        }

        public void AddCoins(int addCoins)
        {
            GlobalSettings.coins += addCoins;
            GlobalSettings.Save();

            SetCoins(GlobalSettings.coins);
        }

        public void AddScore(float addScore)
        {
            GlobalSettings.score += addScore;
            SetScore(GlobalSettings.score);
        }
        void Start () 
        {
            menuPause.HideAll();
        }
	
        void Update () 
        {
	
        }
    }
}
