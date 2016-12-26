using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Qbert.Scripts.GameScene.InputControl
{
    public class ButtonTouchPanel : MonoBehaviour , IPointerClickHandler ,IPointerDownHandler , IPointerUpHandler
    {
        public bool isPressed;
        public float value;
        public bool isPlusValue;
        public int  buttonType;
        public KeyCode keyCode;
        public Action OnClick;
        public float scaleTo = 0.9f;
        public bool isScalePress;
        private Vector3 defaultScele;

        void Start ()
        {
            defaultScele = transform.localScale;
        }

        void OnEnable()
        {
            isPressed = false;
        }

        void Update ()
        {
            if (isPressed)
            {
                value += Time.deltaTime * 3;
                if (value > 1)
                    value = 1;
            }
            else
            {
                value -= Time.deltaTime * 3;
                if (value < 0)
                    value = 0;
            }

            if (Input.GetKeyDown(keyCode))
            {
                isPressed = true;
            }
            else if (Input.GetKeyUp(keyCode))
            {
                isPressed = false;
            }

        }

        public float GetValue()
        {
            if (!isPlusValue)
                return -value;

            return value;
        }

        private bool isChecked = false;
        public bool GetInDown()
        {
            if (Input.GetKeyDown(keyCode))
            {
                return true;
            }

            if (!isChecked)
            {
                isChecked = true;
                return isPressed;
            }
            return false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Input.GetKeyDown(keyCode))
            {
            
                if (OnClick != null)
                {
                    OnClick();
                }
            }

            if (OnClick != null)
            {
            
                OnClick();
            }
        }

        void SetPressScale()
        {
            if(isScalePress)
                this.transform.localScale = new Vector3(scaleTo, scaleTo, scaleTo);
        }

        void SetUnpressScale()
        {
            if(isScalePress)
                this.transform.localScale = defaultScele;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SetPressScale();
            isPressed = true;
            isChecked = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SetUnpressScale();
            isPressed = false;
        }
    }
}
