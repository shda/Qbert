using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Qbert.Scripts.GameScene.InputControl
{
    public class InputArrea : MonoBehaviour , IPointerClickHandler , IPointerDownHandler
    {
        public Action<Vector2> OpDownPressToScreen; 

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (OpDownPressToScreen != null)
            {
                OpDownPressToScreen(eventData.position);
            }
        }

        void Start () 
        {
	
        }
        void Update () 
        {
	
        }
    }
}
