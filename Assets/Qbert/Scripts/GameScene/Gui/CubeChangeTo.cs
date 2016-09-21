using UnityEngine;

namespace Scripts.GameScene.Gui
{
    public class CubeChangeTo : MonoBehaviour
    {
        public Renderer render;

        public void SetColor(Color color)
        {
            render.material.color = color;
        }

        void Start () 
        {
	
        }
	
        void Update () 
        {
	
        }
    }
}
