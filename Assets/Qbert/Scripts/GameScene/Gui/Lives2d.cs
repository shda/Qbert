using UnityEngine;
using System.Collections;


namespace Scripts.GameScene.Gui
{
    public class Lives2d : MonoBehaviour
    {
        public Transform[] lives;

        public void SetLiveCount(int count)
        {
            foreach (var life in lives)
            {
                life.gameObject.SetActive(false);
            }

            for (int i = 0; i < count; i++)
            {
                if (i < lives.Length)
                {
                    lives[i].gameObject.SetActive(true);
                }
            }
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}

