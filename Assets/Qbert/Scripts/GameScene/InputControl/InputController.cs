using System;
using UnityEngine;

namespace Scripts.GameScene.InputControl
{
    public class InputController : MonoBehaviour
    {
        public Action<DirectionMove.Direction> OnPress; 
        public GuiButtonsController guiButtonsController;

        public bool isEnable;

        void Start ()
        {
        }
	
        void Update () 
        {
            if (isEnable)
            {
                foreach (var value in Enum.GetValues(typeof(DirectionMove.Direction)))
                {
                    var eValue = (DirectionMove.Direction)value;
                    if (guiButtonsController.GetIsButtonDown(eValue))
                    {
                        if (OnPress != null)
                        {
                            OnPress(eValue);
                        }
                    }
                }
            }
        }
    }
}
