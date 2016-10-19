using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.GameScene.Gui
{
    public class InfoText : MonoBehaviour
    {
        public Text coinsText;
        public Text scoreText;
        public Text levelText;

        public void UpdateInfo()
        {
            if(coinsText)
                coinsText.text = "" + GlobalValues.coins;

            if (scoreText)
                scoreText.text = "" + GlobalValues.score;

            if (levelText)
                levelText.text = string.Format("level {0}-{1}", 
                    GlobalValues.currentLevel + 1,
                    GlobalValues.currentRound + 1);
        }

        void Start ()
        {
            UpdateInfo();
        }
	
        void Update () 
        {
	
        }
    }
}
