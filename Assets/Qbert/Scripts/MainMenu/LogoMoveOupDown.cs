using UnityEngine;

namespace Assets.Qbert.Scripts.MainMenu
{
    public class LogoMoveOupDown : MonoBehaviour
    {
        public GameScene.Characters.Qbert qbert;
        public RectTransform logo;
        private float startPosition;

        void Start ()
        {
            startPosition = logo.anchoredPosition.y;

        }

        void Update ()
        {
            Vector3 v = qbert.rootModel.localScale;

            logo.anchoredPosition = new Vector2(logo.anchoredPosition.x ,
                startPosition + v.y * 100.0f - 100.0f);

            // Debug.Log(v.y);

        }
    }
}
