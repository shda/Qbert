using UnityEngine;
using System.Collections;
using System.Linq;

public class Transport : GameplayObject
{
    [Header("Transport")]
    public Transform pointMoveTransport;
    public float speedMovingToPoint = 1.0f;
    public float offsetDrop = 1.0f;

    public override Type typeObject
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
        var startPos =  levelController.gameField.mapGenerator.
            GetPointOutsideFieldStartPointToType(Type.Transport);

        if(startPos != null)
        {
            foreach (var pos in startPos.Mix())
            {
                if (!levelController.gameplayObjects.GetGamplayObjectInPoint(pos.curentPoint))
                {
                    SetPosition(pos);
                }
            }
        }
        else
        {
            Debug.Log("Don't find start transport positions.");
        }
    }

    public void SetPosition(PointOutsideField setPosition)
    {
        currentPosition = setPosition.curentPoint;

        var movePosCube = levelController.gameField.mapGenerator.
            GetCubeEndByType(Type.Transport);


        if (movePosCube != null)
        {
            positionMove = movePosCube.currentPosition;
            
        }
        else
        {
            var movePosOut = levelController.gameField.mapGenerator.
                GetPointsOutsideFieldEndPointToType(Type.Transport);

            if (movePosOut != null && movePosOut.Count > 0)
            {
                positionMove = movePosOut[0].curentPoint;
            }
            else
            {
                Debug.LogError("Dont set end point position");

            }
        }
            
        transform.position = setPosition.transform.position;
        transform.rotation = setPosition.transform.rotation * Quaternion.Euler(new Vector3(0, -90, 0));
    }

    public override bool OnColisionToQbert(Qbert qbert)
    {
        if (!isMoving)
        {
            qbert.isFrize = true;
            qbert.isCheckColision = false;
            qbert.currentPosition = currentPosition;
            qbert.StopAllCoroutines();
            qbert.root.position = root.position;

            StartCoroutine(MoveTransport(qbert));

            return true;
        }

        return true;
    }

    public IEnumerator MoveTransport(Qbert qbert)
    {
        var posMove = GetMovePosition();

        StartCoroutine(this.MovingSpeedTransformTo(qbert.root, posMove, speedMovingToPoint));
        yield return StartCoroutine(this.MovingSpeedTransformTo(transform, posMove, speedMovingToPoint));
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
        qbert.isFrize = false;
        qbert.isCheckColision = true;
        qbert.MoveToCube(positionMove);

        OnStartDestroy();
    }


    private Vector3 GetMovePosition()
    {
        var posMove = levelController.gameField.mapGenerator.GetCubeEndByType(typeObject);
        if (posMove != null)
        {
            return posMove.upSide.position + new Vector3(0, offsetDrop, 0);
        }
        else
        {
            Debug.LogError("Don't find move point to transport.");
        }

        return Vector3.zero;
    }

    public override bool CanJumpToMy()
    {
        return true;
    }

}
