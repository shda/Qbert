using System;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.AnimationToTime;
using UnityEngine;

namespace Assets.Qbert.Scripts.ControlConfiguratorScripts
{
    public class ControlConfigurator : MonoBehaviour
    {
        public ButtonMove[] buttons;
        public Transform buttonSave;
        public Transform textDetectIntersectionButtons;
        public AnimationToTimeChangeCanvasGroup animationShowHide;

        public Transform[] objectsDisable;

        public Action<ControlConfigurator> OnEndConfiguration;

        public bool isLock = false;

        private void SwitchObjects(bool isEnable)
        {
            if (objectsDisable != null)
            {
                foreach (var transform1 in objectsDisable)
                {
                    transform1.gameObject.SetActive(isEnable);
                }
            }
        }

        public void Show()
        {
            if (!isLock)
            {
                gameObject.SetActive(true);

                isLock = true;
                StartCoroutine(ActionShow(() =>
                {
                    SwitchObjects(false);
                    isLock = false;
                }));
            }
        }

        IEnumerator ActionShow(Action OnEnd)
        {
            yield return StartCoroutine(animationShowHide.PlayToTime(0.5f));
            if (OnEnd != null)
            {
                OnEnd();
            }
        }

        IEnumerator ActionHide(Action OnEnd)
        {
            yield return StartCoroutine(animationShowHide.PlayToTime(0.5f , null , true));
            if (OnEnd != null)
            {
                OnEnd();
            }
        }

        private void ConnectEvents()
        {
            foreach (var buttonMove in buttons)
            {
                buttonMove.OnButtonStartDrag = OnButtonStartDrag;
                buttonMove.OnButtonEndDrag = OnButtonEndDrag;
                buttonMove.OnButtonDrag = OnButtonDrag;
            }
        }

        private void OnButtonDrag(ButtonMove buttonMove)
        {
            CheckIntersectButtons();
        }

        public void OnSave()
        {
            foreach (var buttonMove in buttons)
            {
                buttonMove.SavePosition();
            }
        }

        public void OnCancel()
        {
            if (!isLock)
            {
                SwitchObjects(true);

                isLock = true;
                StartCoroutine(ActionHide(() =>
                {
                    if (OnEndConfiguration != null)
                    {
                        OnEndConfiguration(this);
                    }

                    isLock = false;
                }));
            }
        }

        public void LoadButtonsPositions()
        {
            foreach (var buttonMove in buttons)
            {
                buttonMove.LoadPosition();
            }
        }

        public void OnDefault()
        {
            foreach (var buttonMove in buttons)
            {
                buttonMove.SetButtonToDefaultPosition();
            }

            CheckIntersectButtons();
        }

        private void OnButtonEndDrag(ButtonMove buttonMove)
        {
            CheckIntersectButtons();
        }
        public void CheckIntersectButtons()
        {
            bool isIntersectButtons = false;

            foreach (var button in buttons)
            {
                button.ResetIntersect();
            }

            foreach (var button in buttons)
            {
                foreach (var button2 in buttons)
                {
                    if(button2 == button)
                        continue;

                    if (button.bounds.Intersects(button2.bounds))
                    {
                        button.SetIntersect();
                        button2.SetIntersect();
                        isIntersectButtons = true;
                    }
                }
            }

            if (isIntersectButtons)
            {
                ShowTextDetectIntersectionButtons();
            }
            else
            {
                HideTextDetectIntersectionButtons();
            }
        }

        private void OnButtonStartDrag(ButtonMove buttonMove)
        {
        
        }

        public void ShowTextDetectIntersectionButtons()
        {
            textDetectIntersectionButtons.gameObject.SetActive(true);
            buttonSave.gameObject.SetActive(false);
        }

        public void HideTextDetectIntersectionButtons()
        {
            textDetectIntersectionButtons.gameObject.SetActive(false);
            buttonSave.gameObject.SetActive(true);
        }

        void Start ()
        {
            StartCoroutine(DelayRun());
        }

        IEnumerator DelayRun()
        {
            yield return new WaitForEndOfFrame();

            HideTextDetectIntersectionButtons();
            ConnectEvents();
            LoadButtonsPositions();
        }
	
        void Update () 
        {
	
        }
    }
}
