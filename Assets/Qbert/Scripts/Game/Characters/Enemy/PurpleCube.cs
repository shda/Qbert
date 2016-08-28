using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PurpleCube : RedCube
{
    [Header("PurpleCube")]
    public AnimationToTime rebornAnimation;
    public override Type typeGameobject
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
        PositionCube qbertPoint = qbert.currentPosition;
        PositionCube myPoint = currentPosition;

        

        if (qbertPoint == myPoint)
        {
            return null;
        }

        PositionCube step = GetNeighborPoint(myPoint , qbertPoint);

        return levelController.gameField.GetCube(step);
    }

    //Todo: Rewrite trash algorithm
    public PositionCube GetNeighborPoint(PositionCube start , PositionCube end)
    {
        int drop = GetDistance(start,end);

        if (drop == 1)
        {
            if (CheckToDrop(start, end))
            {
                return new PositionCube(-1, -1);
            }
        }

        PositionCube upLeft    = new PositionCube(start.line - 1, start.position - 1);
        PositionCube upRight   = new PositionCube(start.line - 1, start.position);
        PositionCube downLeft  = new PositionCube(start.line + 1, start.position);
        PositionCube downRight = new PositionCube(start.line + 1, start.position + 1);

        List<PositionCube> list = new List<PositionCube>()
        {
            upLeft , upRight , downLeft , downRight,
        };

        int minDist = int.MaxValue;

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

        return new PositionCube();
    }

    private bool CheckToDrop(PositionCube start , PositionCube end)
    {
        if (end.position < 0 || end.position > end.line)
        {
            if (end.position > end.line)
            {
                end = new PositionCube(end.line, end.position - 1);
            }
            else
            {
                end = new PositionCube(end.line+ 1, end.position);
            }

            Vector3 newPos = root.position + levelController.gameField.GetOffset(start, end);
            MoveToPointAndDropDown(newPos , character =>
            {
                OnDestroyEnemy();
            });

            return true;
        }


        return false;
    }

    public int GetDistance(PositionCube start, PositionCube end)
    {
        return Mathf.Abs(start.line - end.line) + Mathf.Abs(start.position - end.position);
    }
}

