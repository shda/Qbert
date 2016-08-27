using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PurpleCube : RedCube
{
    [Header("PurpleCube")]
    public AnimationToTime rebornAnimation;
    public override Type typeEnemy
    {
        get { return Type.PurpleCube; }
    }

    public override void Run()
    {
        StartCoroutine(WorkThead());
    }

    protected override IEnumerator WorkThead()
    {
        yield return StartCoroutine(DropToCube());

        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (!isMoving)
            {
                
                var direction = GetRandomDownDirection();
                var cubeTarget = levelController.gameField.GetCubeDirection(direction, currentPosition);
                if (cubeTarget)
                {
                    MoveToCube(cubeTarget);
                    yield return new WaitForSeconds(1.0f);
                }
                else
                {
                    yield return StartCoroutine(RebornToEnemy());
                    yield return StartCoroutine(FallowToQbert());
                    yield break;
                }
                

                //yield return StartCoroutine(RebornToEnemy());
                //yield return StartCoroutine(FallowToQbert());
               // yield break;
            }
        }

        yield return null;
    }

    private IEnumerator RebornToEnemy()
    {
        yield return StartCoroutine(rebornAnimation.PlayToTime(2.0f));
    }


    private IEnumerator FallowToQbert()
    {
        while (true)
        {
            Qbert qbert = levelController.qbert;
            Cube cube = FindPathToQberd(qbert);
            if (cube)
            {
                yield return StartCoroutine(MoveToCubeAnimation(cube));
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }

    private Cube FindPathToQberd(Qbert qbert)
    {
        PointCube qbertPoint = qbert.currentPosition;
        PointCube myPoint = currentPosition;

        PointCube step = GetNeighborPoint(myPoint , qbertPoint);

        return levelController.gameField.GetCube(step);
    }

    //Todo: Rewrite trash algorithm
    public PointCube GetNeighborPoint(PointCube start , PointCube end)
    {   
        PointCube upLeft    = new PointCube(start.line - 1, start.position - 1);
        PointCube upRight   = new PointCube(start.line - 1, start.position);
        PointCube downLeft  = new PointCube(start.line + 1, start.position);
        PointCube downRight = new PointCube(start.line + 1, start.position + 1);

        List<PointCube> list = new List<PointCube>()
        {
            upLeft , upRight , downLeft , downRight,
        };

        float minDist = float.MaxValue;

        foreach (var pointCube in list)
        {
            if (levelController.gameField.GetCube(pointCube))
            {
                minDist = Mathf.Min(GetDistance(pointCube, end), minDist);
            }
        }

        foreach (var pointCube in list)
        {
            if (GetDistance(pointCube, end) == minDist)
            {
                return pointCube;
            }
        }

        return new PointCube();
    }

    public float GetDistance(PointCube start, PointCube end)
    {
        return Mathf.Abs(start.line - end.line) + Mathf.Abs(start.position - end.position);
    }
}
