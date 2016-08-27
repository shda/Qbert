using System;
using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    [Header("Character")]
    public Transform root;
    [HideInInspector]
    public LevelController levelController;
    public CollisionProxy collisionProxy;

    public float timeMove = 1.0f;
    public float timeRotate = 0.2f;
    public float jumpAmplitude = 0.5f;
    public float timeDropDown = 0.4f;
    public float dropDownHeight = 4.0f;

    public AnimationToTime jumpAnimationToTime;

    public PointCube currentPosition;
    public bool isMoving
    {
        get { return moveCoroutine != null; }
    }

    protected Coroutine moveCoroutine;

    public virtual void SetStartPosition(PointCube point)
    {
        StopAllCoroutines();

        currentPosition = point;

        var startCube = levelController.gameField.GetCube(point);
        if (startCube)
        {
            root.position = startCube.upSide.position;
            root.rotation = Quaternion.Euler(0,180,0);
        }
        moveCoroutine = null;
    }

    public  bool MoveToCube(Cube cube)
    {
        return MoveToCube(cube.cubePosition);
    }

    public bool MoveToCube( PointCube point )
    {
        var cube = levelController.gameField.GetCube(point);

        if (cube && moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(MoveToCubeAnimation(cube));
            return true;
        }

        return false;
    }

    public bool MoveToPoint(Vector3 point)
    {
        if (moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(MoveToPointAnimation(point));
            return true;
        }

        return false;
    }

    public bool MoveToPointAndDropDown(Vector3 point , Action<Character> OnEnd = null)
    {
        if (moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(DropDownAnimation(point , OnEnd));
            return true;
        }

        return false;
    }


    public Vector3 GetRotationToCube(Cube cube)
    {
        var direction = (cube.upSide.position - root.position).normalized;
        var rotateTo = Quaternion.LookRotation(direction).eulerAngles;
        return new Vector3(0, rotateTo.y, rotateTo.z);
    }

    public Vector3 GetRotationToPoint(Vector3 point)
    {
        var direction = (point - root.position).normalized;
        var rotateTo = Quaternion.LookRotation(direction).eulerAngles;
        return new Vector3(0, rotateTo.y, rotateTo.z);
    }

    protected IEnumerator RotateToCube(Cube cube)
    {
        var rotateTo = GetRotationToCube(cube);
        yield return StartCoroutine(RotateTo(rotateTo));
    }
    protected IEnumerator RotateToPoint(Vector3 point)
    {
        var rotateTo = GetRotationToPoint(point);
        yield return StartCoroutine(RotateTo(rotateTo));
    }

    protected IEnumerator RotateTo(Vector3 rotateTo)
    {
        var rotateStart = root.rotation.eulerAngles;

        float distance = Vector3.Distance(rotateStart, rotateTo);
        float speedMoving = distance / timeRotate;

        while (distance > 0.01f)
        {
            Vector3 move = Vector3.MoveTowards(root.rotation.eulerAngles,
                rotateTo, speedMoving * Time.deltaTime);
            root.rotation = Quaternion.Euler(move);
            distance = Vector3.Distance(root.rotation.eulerAngles, rotateTo);
            yield return null;
        }
    }

    protected virtual IEnumerator DropDownAnimation(Vector3 point, Action<Character> OnEnd = null)
    {
        if (!isMoving)
        {
            yield return StartCoroutine(RotateToPoint(point));
            yield return StartCoroutine(JumpAndMoveToPoint(point));
            yield return StartCoroutine(DropDown());
        }

        moveCoroutine = null;

        if (OnEnd != null)
        {
            OnEnd(this);
        }
    }

    protected virtual IEnumerator DropDown()
    {
        Vector3 dropDownPoint = root.position - new Vector3(0, dropDownHeight, 0);
        yield return StartCoroutine(this.MovingTransformTo(root, dropDownPoint, timeDropDown));
    }

    protected virtual IEnumerator MoveToPointAnimation(Vector3 point, Action<Character> OnEnd = null)
    {
        if (!isMoving)
        {
            yield return StartCoroutine(RotateToPoint(point));
            yield return StartCoroutine(JumpAndMoveToPoint(point));
        }

        moveCoroutine = null;

        if (OnEnd != null)
        {
            OnEnd(this);
        }
    }

    protected virtual IEnumerator MoveToCubeAnimation(Cube cube , Action<Character> OnEnd = null)
    {
        if (!isMoving)
        {
            yield return StartCoroutine(RotateToCube(cube));
            yield return StartCoroutine(JumpAndMove(cube));
            cube.OnPressMy(this);
        }

        moveCoroutine = null;

        currentPosition = cube.cubePosition;

        if (OnEnd != null)
        {
            OnEnd(this);
        }
    }


    private void SetTimeAnimationJump(float t)
    {
        if (jumpAnimationToTime != null)
        {
            jumpAnimationToTime.time = t;
        }
    }

    protected IEnumerator JumpAndMove(Cube cube)
    {
        yield return StartCoroutine(JumpAndMoveToPoint(cube.upSide.position));
    }

    protected IEnumerator JumpAndMoveToPoint(Vector3 point)
    {
        float t = 0;
        var movingTo = point;
        var startTo = root.position;

        var currentPosition = startTo;

        while (t < 1)
        {
            t += Time.smoothDeltaTime / timeMove;
            Vector3 pos = Vector3.Lerp(startTo, movingTo, t);
            pos = new Vector3(pos.x, pos.y + GetOffsetLerp(t), pos.z);
            root.position = pos;

            SetTimeAnimationJump(t);

            yield return null;
        }

        root.position = movingTo;
    }

    protected float GetOffsetLerp(float t)
    {
        float retFloat = jumpAmplitude;

        if (t < 0.5f)
        {
            retFloat = retFloat * t;
        }
        else
        {
            retFloat = jumpAmplitude - (retFloat * t);
        }

        return retFloat;
    }
}
