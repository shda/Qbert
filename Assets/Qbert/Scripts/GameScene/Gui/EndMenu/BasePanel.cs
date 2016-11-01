using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Gui.EndMenu
{
    public class BasePanel : MonoBehaviour 
    {
        protected bool isPress = false;

        protected void DisablePressButtons()
        {
            isPress = true;
        }

        protected void EnablePressButtons()
        {
            isPress = false;
        }

        public virtual void UpdatePanels()
        {
            EnablePressButtons();
        }
    }
}
