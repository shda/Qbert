using Assets.Qbert.Scripts.GameScene.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.GameScene.Gui
{
    public class GameGui : MonoBehaviour
    {
        public ResourceCounter scoreText;
        public Text levelLabel;
        public ResourceCounter coinsLabel;
        public CubeChangeTo cubeChangeTo;
        public Lives2d lives;
        public EndGameGui endGameGui;
        public MenuPause menuPause;
        public LevelController levelController;

        private bool isWatchAd = false;
        
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
            endGameGui.ShowGameOver();
        }

        public void ShowEndGame()
        {
            endGameGui.OnShowSecondRails();
        }

        public void SetColorCube(Color color)
        {
            cubeChangeTo.SetColor(color);
        }

        public void UpdateLiveCount()
        {
            lives.SetLiveCount(GlobalValues.countLive);
        }

        public void UpdateScore()
        {
            scoreText._labelCount = GlobalValues.score;
            scoreText.UpdateText();
            SetScore(GlobalValues.score);
        }
        public void SetLevelNumber(int level , int round)
        {
            levelLabel.text = string.Format("{0}-{1}", level + 1, round + 1);
        }
        public void SetScore(float setScore)
        {
            GlobalValues.score = setScore;
            scoreText.SetValue(setScore);
        }

        public void SetCoins(int setConis)
        {
            Debug.Log(setConis);
            coinsLabel.SetValueForce(setConis);
        }

        public void AddCoins(int addCoins)
        {
            GlobalValues.coins += addCoins;
            GlobalValues.Save();

            SetCoins(GlobalValues.coins);
        }

        public void AddScore(float addScore)
        {
            GlobalValues.score += addScore;
            SetScore(GlobalValues.score);
        }
        void Start () 
        {
            SetCoins(GlobalValues.coins);
            menuPause.HideAll();
        }
	
        void Update () 
        {
	
        }
    }
}
