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
                    if (guiButtonsController.GetButtonIsDown(eValue))
                    //if (guiButtonsController.GetIsButtonPress(eValue))
                    {
                        Debug.Log("IsDown");

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
