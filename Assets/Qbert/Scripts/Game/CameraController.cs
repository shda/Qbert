using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera gameCamera;
    public Transform rootCamera;
    public GameFieldGenerator fieldGenerator;

    public float durationMove;

    public void MoveCameraToCube(PositionCube posCube , float cameraSize , float duration)
    {
        Cube findCubeCenter = fieldGenerator.FindCubeToPoint(posCube);
        if (findCubeCenter)
        {
            Vector3 cameraMovePositon = findCubeCenter.upSide.position;
            StartCoroutine(
                this.MovingTransformTo(rootCamera.transform, cameraMovePositon, duration));

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
