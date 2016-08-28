using UnityEngine;
using System.Collections;

public class Transport : GameplayObject
{
    [Header("Transport")]
    public Transform pointMoveTransport;
    public float speedMovingToPoint = 1.0f;
    public override Type typeGameobject
    {
        get { return Type.Transport; }
    }

    public override GameplayObject Create(Transform root, LevelController levelController)
    {
        var transport = base.Create(root, levelController) as Transport;
        return transport;
    }

    public override void Init()
    {
        int maxLevels = levelController.gameField.mapGenerator.levels;
        var transports = levelController.gameplayObjects.GetObjectsToType<Transport>();

        int left = 0;
        int right = 0;

        foreach (var transport in transports)
        {
            if (transport.currentPosition.position == -1)
            {
                left++;
            }
            else
            {
                right++;
            }
        }

        int randomLevel = 0;

        bool ok = false;

        do
        {
            randomLevel = Random.Range(0, maxLevels);

            ok = true;

            foreach (var transport in transports)
            {
                if (transport.currentPosition.line == randomLevel)
                {
                    ok = false;
                }
            }

        } while (!ok);


        SetPosition(new PositionCube(randomLevel, left > right ? randomLevel : 0) );
    }

    public void SetPosition(PositionCube setPosition)
    {
        var cube = levelController.gameField.GetCube(setPosition);
        PositionCube cubePos = cube.cubePosition;

        Vector3 newPos;

        if (setPosition.position > 0)
        {
            newPos = cube.upSide.position +
                levelController.gameField.GetOffsetDirection(DirectionMove.Direction.UpRight);
            cubePos = new PositionCube(cubePos.line - 1, cubePos.position);

        }
        else
        {
            newPos = cube.upSide.position +
                levelController.gameField.GetOffsetDirection(DirectionMove.Direction.UpLeft);
            
            cubePos = new PositionCube(cubePos.line, cubePos.position - 1);

        }

        currentPosition = cubePos;

        transform.position = newPos;
        transform.rotation = cube.upSide.rotation * Quaternion.Euler(new Vector3(-90, 0, 0));
        pointMoveTransport = levelController.gameplayObjects.pointMoveTransport;

    }

    public override bool OnColisionToQbert(Qbert qbert)
    {
        if (!isMoving)
        {
            qbert.isFrize = true;
            qbert.currentPosition = currentPosition;
            qbert.StopAllCoroutines();
            qbert.root.position = root.position;

            StartCoroutine(MoveTransport(qbert));

            return true;
        }

        return false;
    }

    public IEnumerator MoveTransport(Qbert qbert)
    {
        var posMove = pointMoveTransport.position;

        StartCoroutine(this.MovingSpeedTransformTo(qbert.root,posMove, speedMovingToPoint));
        yield return StartCoroutine(this.MovingSpeedTransformTo(transform, posMove, speedMovingToPoint));
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
        qbert.isFrize = false;
        qbert.MoveToCube(new PositionCube(0, 0));

        OnDestroyEnemy();
    }

    
}
