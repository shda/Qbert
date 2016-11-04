using Assets.Qbert.Scripts.GameScene.Gui.EndMenu;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Gui
{
    public class EndGameGui : MonoBehaviour
    {
        public EndGameGuiAnimator endGameGuiAnimator;

        public void ShowGameOver()
        {
            endGameGuiAnimator.ShowGameOver();
        }

        public void OnShowSecondRails()
        {
        
            endGameGuiAnimator.ShowSecondRails();
        }

        void Start()
        {
            
        }

        void Update()
        {

        }
    }
}
