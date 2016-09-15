using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RedCube : GameplayObject
{
    //public PositionCube[] startPositions;
   // public int idStartPosition;
    [Header("RedCube")]
    public float heightDrop = 3.0f;
   
    public override Type typeGameobject
    {
        get { return Type.RedCube; }
    }

    public override GameplayObject Create(Transform root, LevelController levelController)
    {
        var positions = levelController.gameField.mapGenerator.GetCubesById(startPositionId).ToList();
        positions = positions.Mix();

        foreach (var positionCube in positions)
        {
            var gaToPoint = levelController.gameplayObjects.GetGamplayObjectInPoint(positionCube.cubePosition);
            if (gaToPoint == null || gaToPoint.CanJumpToMy())
            {
                return SetObject(root, levelController, positionCube.cubePosition);
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
                    else if (IsEndLine())
                    {
                        yield return StartCoroutine(ReachedLowerLevel(direction));
                        yield break;
                    }
                }

            }
        }

        yield return null;
    }

    private bool IsEndLine()
    {
        var cube = levelController.gameField.GetCube(currentPosition);
        return cube.nodes.Exists(x => x.cubePosition.line < currentPosition.line);
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