using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene
{
    public class GUIButtonRestartLevel : MonoBehaviour
    {
        public void OnRestartLevel()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        void Start () 
        {
	
        }
        void Update () 
        {
	
        }
    }
}
