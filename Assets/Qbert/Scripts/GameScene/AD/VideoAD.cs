using System;
using System.Collections;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.AD
{
    public class VideoAD : MonoBehaviour
    {
        public Transform root;

        private Action<bool> OnEndShow;

        public bool ShowAD(Action<bool> OnEndShow)
        {
            transform.gameObject.SetActive(true);

            this.OnEndShow = OnEndShow;
            StartCoroutine(Show());
            return true;
        }


        IEnumerator Show()
        {
            root.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            OnButtonClose();
        }


        public void OnButtonClose()
        {
            root.gameObject.SetActive(false);

            if (OnEndShow != null)
            {
                //Is ok video
                OnEndShow(true);
            }
        }

        void Awake()
        {
            root.gameObject.SetActive(false);
        }

        void Start () 
        {
	
        }

        void Update () 
        {
	
        }
    }
}
