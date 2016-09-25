using UnityEngine;

namespace Assets.Qbert.Scripts.Utils
{
    public class OrthographicCameraSizeChange : MonoBehaviour
    {
        public float resizeCamera = 2.0f;
        public Camera orthographicCamera;

        public float developSize = 1.0f;
        public float developWidth = 1024;
        public float developHight = 768;

        void Start () 
        {
	
        }
	
        void Update ()
        {
            float step = (float) developSize/ Screen.height;




            //float f = (float)Screen.height/Screen.width;

            orthographicCamera.orthographicSize = resizeCamera + Screen.width * step;

            Debug.Log(step);
        }
    }
}
