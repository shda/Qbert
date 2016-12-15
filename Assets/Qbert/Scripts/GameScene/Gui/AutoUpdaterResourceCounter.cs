using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.Gui
{
    public class AutoUpdaterResourceCounter : MonoBehaviour
    {
        public ResourceCounter resourceCounter;

        void Start () 
        {
	
        }

        void Update () 
        {
            resourceCounter.SetValue(GlobalValues.coins);
        }
    }
}
