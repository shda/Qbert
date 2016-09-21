using System;
using System.Collections;
using Assets.Qbert.Scripts.GameScene.MapLoader;
using Assets.Qbert.Scripts.Utils;
using UnityEngine;

namespace Assets.Qbert.Scripts.GameScene
{
    public class CameraController : MonoBehaviour
    {
        public Camera gameCamera;
        public Transform rootCamera;
        public GameFieldGenerator fieldGenerator;

        public float durationMove;

        public void MoveCameraToPoint(Vector3 positionMove,  float duration,
            Action<Transform> OnEndCameraMove = null)
        {
            StartCoroutine(this.MovingTransformTo(
                rootCamera.transform, positionMove, duration, null, OnEndCameraMove));
        }

        public void MoveCameraToCube(PositionCube posCube , float cameraSize , float duration , Action<Transform> OnEndCameraMove = null )
        {
            Cube findCubeCenter = fieldGenerator.FindCubeToPoint(posCube);
            if (findCubeCenter)
            {
                Vector3 cameraMovePositon = findCubeCenter.upSide.position;
                StartCoroutine(
                    this.MovingTransformTo(rootCamera.transform, cameraMovePositon, duration , null , OnEndCameraMove));

                StartCoroutine( ChangeCameraSize(duration, cameraSize) );
            }
        }
        public IEnumerator ChangeCameraSize( float duration  , float toCameraSize)
        {
            float t = 0;
            float startSize = gameCamera.orthographicSize;
            float to = toCameraSize;

            while (t < 1)
            {
                t += Time.smoothDeltaTime / duration;
                gameCamera.orthographicSize = startSize + ((to - startSize)*t);
                yield return null;
            }
        }

        void Start () 
        {
	
        }
	
        void Update () 
        {
	
        }
    }
}
