using System;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.InputControl
{
    public class InputController : MonoBehaviour
    {
        public Action<DirectionMove.Direction> OnPress; 
        public GuiButtonsController guiButtonsController;

        public bool isEnable;

        void Start ()
        {
        }

        public void HideButtons()
        {
            guiButtonsController.DisableButtons();
        }


        public void ShowButtons()
        {
            guiButtonsController.EnableButtons();
        }

        public void DirectionSwipe(DirectionMove.Direction direction)
        {
            if (OnPress != null)
            {
                OnPress(direction);
            }
        }
	
        void Update () 
        {
            if (isEnable)
            {
                foreach (var value in Enum.GetValues(typeof(DirectionMove.Direction)))
                {
                    var eValue = (DirectionMove.Direction)value;

                    if (GlobalValues.isRespondButtonPressed)
                    {
                        if (guiButtonsController.GetButtonIsDown(eValue))
                        {
                            if (OnPress != null)
                            {
                                OnPress(eValue);
                            }
                        }
                    }
                    else
                    {
                        if(guiButtonsController.GetIsButtonPress(eValue))
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
}
