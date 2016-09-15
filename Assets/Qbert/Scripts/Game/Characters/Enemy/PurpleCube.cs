using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PurpleCube : RedCube
{
    [Header("PurpleCube")]
    public AnimationToTime rebornAnimation;
    public PathFinder pathFinder;

    public Cube myCube;
    public Cube qbertCube;

    public override Type typeGameobject
    {
        get { return Type.PurpleCube; }
    }

    public override void Run()
    {
        StartCoroutine(WorkThead());
    }

    protected override IEnumerator ReachedLowerLevel(DirectionMove.Direction direction)
    {
        yield return StartCoroutine(RebornToEnemy());
        yield return StartCoroutine(FallowToQbert());
    }

    private IEnumerator RebornToEnemy()
    {
        yield return StartCoroutine(rebornAnimation.PlayToTime(2.0f , iTimeScaler));
    }

    private IEnumerator FallowToQbert()
    {
        while (true)
        {
            Qbert qbert = levelController.qbert;
            Cube cube = FindPathToQberd(qbert);
            if (cube)
            {
                jumpAnimationToTime = null;
                yield return StartCoroutine(MoveToCubeAnimation(cube));
            }


            yield return StartCoroutine(this.WaitForSecondITime(0.5f, iTimeScaler));
        }

        yield return null;
    }

    void Start()
    {
        pathFinder = new PathFinder();
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
        var targetCube = levelController.gameplayObjects.GetGamplayObjectInPoint(step);
        if (targetCube && !targetCube.CanJumpToMy())
        {
            return null;
        }

        return levelController.gameField.GetCube(step);
    }

    public List<Cube> findPath; 

    public PositionCube GetNeighborPoint(PositionCube myPoint, PositionCube qbertPoint)
    {
        var myCubePath      = levelController.gameField.GetCube(myPoint);
        var qbertCubePath   = levelController.gameField.GetCube(qbertPoint);

        if (qbertCubePath == null)
        {
            int drop = GetDistance(myPoint, qbertPoint);

            if (drop == 2)
            {
                if (CheckToDrop(myPoint, qbertPoint))
                {
                    return new PositionCube(-1, -1);
                }
            }

            qbertCubePath = qbertCube;
        }
            

        qbertCube = qbertCubePath;
        findPath = pathFinder.FindPath(myCubePath,qbertCubePath);

        if (findPath != null && findPath.Count >= 2)
        {
            return findPath[findPath.Count - 2].cubePosition;
        }

        return new PositionCube();
    }

    void Update()
    {

    }

    private bool CheckToDrop(PositionCube start , PositionCube end)
    {
        if (end.position < 0 || end.position > end.line)
        {
            /*
            if (end.position > end.line)
            {
                end = new PositionCube(end.line, end.position - 1);
            }
            else
            {
                end = new PositionCube(end.line+ 1, end.position);
            }
            */
            AddScore(ScorePrice.dropPurpeCube);

            Vector3 newPos = root.position - levelController.gameField.GetOffset(start, end);
            MoveToPointAndDropDown(newPos , character =>
            {
                OnStartDestroy();
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

