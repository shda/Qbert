using System;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.InputControl
{
    public class GuiButtonsController : SingletonT<GuiButtonsController>
    {
        [Serializable]
        public class ButtonControl
        {
            public ButtonTouchPanel button;
            public DirectionMove.Direction buttonType;
        }

        public ButtonControl[] controls;
        public InputArrea inputArrea;
        public Action<Vector2> OnPressToScreen;

        public AnimationToTimeChangeCanvasGroup changeCanvasGroup;

        public void DisableButtons()
        {
            StopAllCoroutines();
            StartCoroutine(this.WaitCoroutine(changeCanvasGroup.PlayToTime(0.2f), transform1 =>
            {

            }));
        }

        public void EnableButtons()
        {

            StopAllCoroutines();
            StartCoroutine(this.WaitCoroutine(changeCanvasGroup.PlayToTime(0.2f , null , true), transform1 =>
            {

            }));
        }

        public override void AwakeFirst()
        {
        
        }

        void Start () 
        {
            inputArrea.OpDownPressToScreen = OpDownPressToScreen;
            ConnectButtons();
        }

        void ConnectButtons()
        {
            foreach (var buttonControl in controls)
            {
                buttonControl.button.buttonType = (int) buttonControl.buttonType;
            }
        }

        public bool GetIsButtonPress(DirectionMove.Direction buttonType)
        {
            return GetButtonByType(buttonType).button.GetInDown();
        }

        public float GetButtonValue(DirectionMove.Direction buttonType)
        {
            return GetButtonByType(buttonType).button.GetValue();
        }

        public bool GetButtonIsDown(DirectionMove.Direction buttonType)
        {
            return GetButtonByType(buttonType).button.isPressed;
        }

        public ButtonControl GetButtonByType(DirectionMove.Direction buttonType)
        {
            foreach (var buttonControl in controls)
            {
                if (buttonControl.button.buttonType == (int)buttonType)
                {
                    return buttonControl;
                }
            }
            return null;
        }

        private void OpDownPressToScreen(Vector2 vector2)
        {
            if (OnPressToScreen != null)
            {
                OnPressToScreen(vector2);
            }
        }


        void Update () 
        {
	
        }
    }
}
