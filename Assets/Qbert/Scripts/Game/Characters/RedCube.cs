using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RedCube : Enemy
{
    public float heightDrop = 3.0f;
    public float durationDrop = 1.0f;

    public PointCube[] startPositions;

    public override Type typeEnemy
    {
        get { return Type.RedCube; }
    }

    public override Enemy Create(Transform root, GameField gameField)
    {
        var enemy = base.Create(root, gameField);
        var startPosition = startPositions[Random.Range(0, startPositions.Length)];
        enemy.SetStartPosition(startPosition);
        return enemy;
    }

    public override void Run()
    {
        StartCoroutine(WorkThead());
    }

    IEnumerator DropToCube()
    {
        var targetPosition = root.position;
        root.position = new Vector3(root.position.x, root.position.y + heightDrop, root.position.z);
        yield return StartCoroutine(this.MovingTransformTo(root, targetPosition, 0.2f));
        yield return null;
    }

    IEnumerator WorkThead()
    {
        yield return StartCoroutine(DropToCube());

        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (!isMoving)
            {
                var direction = GetRandomDirection();
                var cubeTarget = gameField.GetCubeDirection(direction, currentPosition);
                if (cubeTarget)
                {
                    MoveToCube(cubeTarget);
                    yield return new WaitForSeconds(1.0f);
                }
                else
                {
                    Vector3 newPos = root.position + gameField.GetOffsetDirection(direction);
                    MoveToPointAndDropDown(newPos, character =>
                    {
                        OnDestroyEnemy();
                    });
                    yield break;
                }
            }
        }

        yield return null;
    }

    private DirectionMove.Direction GetRandomDirection()
    {
        List<DirectionMove.Direction> directions = new List<DirectionMove.Direction>();
        directions.Add(DirectionMove.Direction.DownLeft);
        directions.Add(DirectionMove.Direction.DownRight);

        return directions.GetRandom();
    }
}