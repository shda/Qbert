using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GameScene.Gui
{
    public class GameGui : MonoBehaviour
    {
        public ResourceCounter scoreText;
        public Text levelLabel;
        public Text coinsLabel;

        public CubeChangeTo cubeChangeTo;
       // public GuiLive lives;
        public Lives2d lives;

        public Transform gameOver;

        public void ShowGameOver()
        {
            gameOver.gameObject.SetActive(true);
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
            SetCoins(GlobalSettings.coins);
        }

        public void AddScore(float addScore)
        {
            GlobalSettings.score += addScore;
            SetScore(GlobalSettings.score);
        }
        void Start () 
        {
	    
        }
	
        void Update () 
        {
	
        }
    }
}
