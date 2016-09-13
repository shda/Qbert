using UnityEngine;
using System.Collections;
using System.Linq;

public class CubeCreateAnimator : MonoBehaviour
{
    public GameFieldGenerator gameFieldGenerator;
    public float offset;
    public float duration;

    public void StartAnimateShow()
    {
        PositionCube centerPoint = new PositionCube(gameFieldGenerator.startLine , gameFieldGenerator.startPos);

        var workCubes = gameFieldGenerator.map.Where(x => x.cubePosition != centerPoint).ToArray();

        for (int c = 0; c < workCubes.Count(); c++)
        {
            var currentCube = workCubes[c];

            currentCube.gameObject.SetActive(false);

            StartCoroutine(StartCubeShow(currentCube, offset, duration, c * (duration * 0.5f)));
        }
    }

    IEnumerator StartCubeShow(Cube cube , float offset , float duration , float delayStart)
    {
        yield return new WaitForSeconds(delayStart);

        cube.gameObject.SetActive(true);

        Vector3 endMovePositino = cube.transform.position;
        cube.transform.position -= new Vector3(0,offset , 0);

        yield return StartCoroutine(
            this.MovingTransformTo(cube.transform, endMovePositino, duration));
    }

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
