using Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GameScene
{
    public class GameOverText : MonoBehaviour
    {
        public Text text;

        void OnEnable()
        {
            text.color =  new Color(1,1,1,0);
            StartCoroutine(this.ChangeColor(text.color, color =>
            {
                text.color = color;
            }, new Color(1, 1, 1, 1), 0.5f));
        }

        void Start () 
        {
	
        }
	
        void Update () 
        {
	
        }
    }
}
