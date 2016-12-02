using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene.GiftBox
{
    public class GiftBox3d : MonoBehaviour 
    {
        public Transform rootGiftBox;
        public GameObject prefabGift;
        public Vector3 setGravity;
        public Transform giftBox;
        private Vector3 oldGravity;

        public GiftAnimations animations;


        public void CreateNewBox()
        {
            if (giftBox != null)
            {
                giftBox.gameObject.SetActive(false);
                Destroy(giftBox.gameObject);
            }

            GameObject giftBoxObject = Instantiate(prefabGift);
            giftBox = giftBoxObject.transform;


            giftBox.transform.SetParent(rootGiftBox);
            giftBox.transform.localPosition = Vector3.zero;
            giftBox.transform.localScale = new Vector3(1, 1, 1);

            animations = giftBox.GetComponentInChildren<GiftAnimations>();
        }


        public void SetGraviry()
        {
            oldGravity = Physics.gravity;
            Physics.gravity = setGravity;
        }

        public void ReturnGravity()
        {
            Physics.gravity = oldGravity;
        }


        void Start () 
        {
	
        }

        void Update () 
        {
	
        }
    }
}
