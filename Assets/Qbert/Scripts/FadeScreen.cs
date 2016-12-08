using System;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts
{
    public class FadeScreen : MonoBehaviour
    {
        public GraphicRaycaster graphicRaycaster;
        public Image frontImage;
        public Action<Transform> OnEnd;

        public void StartEnable(float duration)
        {
            gameObject.SetActive(true);

            StopAllCoroutines();
            StartCoroutine(this.ChangeColorGraphic(frontImage, new Color(0, 0, 0, 1), duration , transform1 =>
            {
                OnEndAction(true);
            }));
        }

        public void StartDisable(float duration)
        {
            StopAllCoroutines();
            StartCoroutine(this.ChangeColorGraphic(frontImage, new Color(0, 0, 0, 0), duration , transform1 =>
            {
                OnEndAction(false);
                gameObject.SetActive(false);
            }));
        }


        public void OnEndAction(bool enableRaycaster)
        {
            graphicRaycaster.enabled = enableRaycaster;

            if (OnEnd != null)
            {
                OnEnd(transform);
            }
        }

        public void SetEnable()
        {
            frontImage.color = new Color(0, 0, 0, 1);
            OnEndAction(true);
        }

        public void SetDisable()
        {
            frontImage.color = new Color(0, 0, 0, 0);
            OnEndAction(false);
        }

        void Awake()
        {
            SetEnable();
        }

        void Update () 
        {
	
        }
    }
}
