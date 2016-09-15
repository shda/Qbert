using UnityEngine;
using System.Collections;
using System.Linq;

public class Transport : GameplayObject
{
    [Header("Transport")]
    public Transform pointMoveTransport;
    public float speedMovingToPoint = 1.0f;
    public int startPositionId;
    public int movePositionId;
    public float offsetDrop = 1.0f;

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
        var startPos =  levelController.gameField.mapGenerator.
            GetPointOutsideFieldToId(startPositionId);

        if(startPos != null)
        {
            SetPosition(startPos.Mix().First());
        }

    }

    public void SetPosition(PointOutsideField setPosition)
    {
        var movePos = levelController.gameField.mapGenerator.
            GetCubeById(movePositionId);

        currentPosition = setPosition.curentPoint;
        positionMove = movePos.cubePosition;

        transform.position = setPosition.transform.position;
        transform.rotation = setPosition.transform.rotation * Quaternion.Euler(new Vector3(0, -90, 0));
       

        
        // pointMoveTransport = levelController.gameplayObjects.pointMoveTransport;

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
        var posMove = levelController.gameField.mapGenerator.GetCubeById(movePositionId);
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


}
