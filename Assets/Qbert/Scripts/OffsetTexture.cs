using UnityEngine;

namespace Assets.Qbert.Scripts
{
    public class OffsetTexture : MonoBehaviour
    {
        public float speetX = 1;
        public float speedY = 1;

        public Renderer render;
        void Start () 
        {
	
        }
	
        void Update () 
        {
            render.material.mainTextureOffset = new Vector2(
                render.material.mainTextureOffset.x + speetX * Time.deltaTime,
                render.material.mainTextureOffset.y + speedY * Time.deltaTime);

        }
    }
}
