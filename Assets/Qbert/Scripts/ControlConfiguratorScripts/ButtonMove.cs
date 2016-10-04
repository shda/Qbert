using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Qbert.Scripts.ControlConfiguratorScripts
{
    public class ButtonMove : MonoBehaviour , IDragHandler ,
        IPointerDownHandler ,IPointerUpHandler
    {
        public Image centerImage;
        public Button button;

        public Action<ButtonMove> OnButtonStartDrag;
        public Action<ButtonMove> OnButtonEndDrag;
        public Action<ButtonMove> OnButtonDrag;

        public RectTransform parentRect;
        public RectTransform rectTransform;

        public Color intersectColor;

        private Vector2 defaultPosition;
        private Color defaultColor;

        public Bounds bounds
        {
            get
            {
                return CalculateBounds(rectTransform);
            }
        }

        Bounds CalculateBounds(RectTransform transform)
        {
            Bounds bounds = new Bounds(transform.localPosition, new Vector3(transform.rect.width, transform.rect.height, 0.0f));
            return bounds;
        }

        public void SavePosition()
        {
            var pos = transform.localPosition;

            PlayerPrefs.SetFloat(name + "X", pos.x);
            PlayerPrefs.SetFloat(name + "Y", pos.y);
        }

        public void LoadPosition()
        {
            float x = PlayerPrefs.GetFloat(name + "X", defaultPosition.x);
            float y = PlayerPrefs.GetFloat(name + "Y", defaultPosition.y);
            transform.localPosition = new Vector3(x,y,0);
        }

        void Start()
        {
            defaultColor = centerImage.color;
            defaultPosition = transform.localPosition;
            if (button != null)
            {
                button.enabled = false;
            }
        
            rectTransform = GetComponent<RectTransform>();
        }

        public void SetIntersect()
        {
            centerImage.color = intersectColor;
        }

        public void ResetIntersect()
        {
            centerImage.color = defaultColor;
        }
	
        void Update () 
        {
	    
        }

        public void SetButtonToDefaultPosition()
        {
            transform.localPosition = defaultPosition;
            SavePosition();
        }

        public void OnDrag(PointerEventData eventData)
        {
            var screnPos = eventData.pointerCurrentRaycast.screenPosition;

            Vector2 resultRect;
        
            bool isOk = RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, screnPos,
                eventData.enterEventCamera,out resultRect);

            if (isOk)
            {
                transform.localPosition = resultRect;
            }

            if (OnButtonDrag != null)
            {
                OnButtonDrag(this);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Debug.Log(eventData.position);

            if (OnButtonStartDrag != null)
            {
                OnButtonStartDrag(this);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (OnButtonEndDrag != null)
            {
                OnButtonEndDrag(this);
            }
        }
    }
}
