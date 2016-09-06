using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RedCube : GameplayObject
{
    public PositionCube[] startPositions;
    [Header("RedCube")]
    public float heightDrop = 3.0f;
   
    public override Type typeGameobject
    {
        get { return Type.RedCube; }
    }

    public override GameplayObject Create(Transform root, LevelController levelController)
    {
        List<PositionCube> positions = new List<PositionCube>(startPositions);
        positions = positions.Mix();

        foreach (var positionCube in positions)
        {
            var gaToPoint = levelController.gameplayObjects.GetGamplayObjectInPoint(positionCube);
            if (gaToPoint == null || gaToPoint.CanJumpToMy())
            {
                return SetObject(root, levelController, positionCube);
            }
        }

        return null;
    }

    public virtual GameplayObject SetObject(Transform root, LevelController levelController , PositionCube startPosition)
    {
        var gObject = base.Create(root, levelController);
        gObject.SetStartPosition(startPosition);
        gObject.transform.rotation *= Quaternion.Euler(0, 45, 0);
        return gObject;
    }

    public override void Run()
    {
        StartCoroutine(WorkThead());
    }

    protected virtual IEnumerator DropToCube()
    {
        var targetPosition = root.position;
        root.position = new Vector3(root.position.x, root.position.y + heightDrop, root.position.z);
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(this.MovingTransformTo(root, targetPosition, 0.2f , iTimeScaler));
        yield return null;
    }

    protected virtual IEnumerator WorkThead()
    {
        yield return StartCoroutine(DropToCube());

        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (!isMoving)
            {
                Cube cubeTarget = null;
                DirectionMove.Direction direction = DirectionMove.Direction.DownLeft;;

                if (GetMoveCube(ref cubeTarget , ref direction))
                {
                    if (cubeTarget)
                    {
                        MoveToCube(cubeTarget);
                        yield return StartCoroutine(this.WaitForSecondITime(1.0f, iTimeScaler));
                    }
                    else if (currentPosition.line == levelController.gameField.mapGenerator.levels - 1)
                    {
                        yield return StartCoroutine(ReachedLowerLevel(direction));
                        yield break;
                    }
                }

            }
        }

        yield return null;
    }

    protected virtual IEnumerator ReachedLowerLevel(DirectionMove.Direction direction)
    {
        Vector3 newPos = root.position + levelController.gameField.GetOffsetDirection(direction);
        MoveToPointAndDropDown(newPos, character =>
        {
            OnStartDestroy();
        });

        yield break;
    }
    protected virtual bool GetMoveCube(ref Cube refCube, ref DirectionMove.Direction refDirection)
    {
        List<DirectionMove.Direction> directions = new List<DirectionMove.Direction>();
        directions.Add(DirectionMove.Direction.DownLeft);
        directions.Add(DirectionMove.Direction.DownRight);

        directions = directions.Mix();

        foreach (var direction in directions)
        {
            var cubeTarget = levelController.gameField.GetCubeDirection(direction, currentPosition);
            if (cubeTarget)
            {
                var targetCube = levelController.gameplayObjects.GetGamplayObjectInPoint(cubeTarget.cubePosition);
                if (targetCube == null || targetCube.CanJumpToMy())
                {
                    refCube = cubeTarget;
                    refDirection = direction;
                    return true;
                }
            }
        }

        return true;
    }
}