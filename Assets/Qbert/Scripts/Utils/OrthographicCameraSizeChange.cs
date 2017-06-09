using UnityEngine;

namespace Assets.Qbert.Scripts.Utils
{
    public class OrthographicCameraSizeChange : MonoBehaviour
    {
        public float workSize = 2.0f;
        public Camera orthographicCamera;

        public float developWidth = 1024;
        public float developHight = 768;

        void Start () 
        {
	
        }
	
        void Update ()
        {
            float fd = developWidth/developHight;
            float sd = Screen.width/ (float)Screen.height;

            float step = (float)fd/ sd;

            orthographicCamera.orthographicSize = workSize*step;
        }
    }
}
