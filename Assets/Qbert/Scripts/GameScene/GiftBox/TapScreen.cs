using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Qbert.Scripts.GameScene.GiftBox
{
    public class TapScreen : MonoBehaviour , IPointerClickHandler
    {
        public Action OnTapScreen;

        void Start () 
        {
	
        }

        void Update () 
        {
	
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnTapScreen != null)
            {
                OnTapScreen();
            }
        }
    }
}
