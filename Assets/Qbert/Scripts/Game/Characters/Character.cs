﻿using System;
using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    protected ITimeScale iTimeScaler;
    protected float timeScale
    {
        get
        {
            if (iTimeScaler == null)
            {
                Debug.LogError("Not set time scaler interface.");
                return 1.0f;
            }

            return iTimeScaler.timeScale;
        }
    }

    public enum Type
    {
        RedCube,
        PurpleCube,
        Transport,
        BlueCube,
        ColoredCube,
        PinkCube,
        CoinCube,
        Qbert,
    }
    public virtual Type typeObject
    {
        get { return Type.Qbert; }
    }

    [System.Serializable]
    public class EditorRules
    {
        public bool isUsingStartPosition = true;
        public bool isUsingEndPosition = true;
        public bool isNecessaryStartPoint = true;
        public bool isNecessaryEndPoint = true;
    }

    [Header("EditorInfo")]
    public EditorRules editorRules;

    [Header("Character")]
    public Transform root;
    public LevelController levelController;
    public CollisionProxy collisionProxy;

    public float timeMove = 1.0f;
    public float timeRotate = 0.2f;
    public float jumpAmplitude = 0.5f;
    public float timeDropDown = 0.4f;
    public float dropDownHeight = 4.0f;
    public bool isFrize = false;
    public bool isCheckColision = true;
    public AnimationToTime jumpAnimationToTime;

    public PositionCube currentPosition;
    public PositionCube positionMove;
    public bool isMoving
    {
        get { return moveCoroutine != null; }
    }

    protected Coroutine moveCoroutine;

    public Cube GetCubeCurrentPosition()
    {
        return levelController.gameField.GetCube(currentPosition);
    }

    public void SetTimeScaler(ITimeScale timeScaler)
    {
        iTimeScaler = timeScaler;
    }

    protected void AddScore(float score)
    {
        levelController.AddScore(score);
    }

    protected void AddCoins(int coint)
    {
        levelController.AddCoins(coint);
    }

    public new void StopAllCoroutines()
    {
        base.StopAllCoroutines();
        moveCoroutine = null;
    }

    public virtual void Init()
    {

    }

    public virtual bool OnPressCube(Cube cube)
    {
        return false;
    }

    public virtual void SetStartPosition(PositionCube point)
    {
        StopAllCoroutines();

        currentPosition = point;
        positionMove = point;

        var startCube = levelController.gameField.GetCube(point);
        if (startCube)
        {
            root.position = startCube.upSide.position;
            root.rotation = Quaternion.Euler(0,180,0);
        }
        moveCoroutine = null;
    }

    public bool MoveToCube(Cube cube)
    {
        return MoveToCube(cube.currentPosition);
    }

    public bool MoveToCube( PositionCube point )
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


    public virtual Vector3 GetRotationToCube(Cube cube)
    {
        var direction = (cube.upSide.position - root.position).normalized;
        var rotateTo = Quaternion.LookRotation(direction).eulerAngles;
        return new Vector3(0, rotateTo.y, rotateTo.z);
    }

    public virtual Vector3 GetRotationToPoint(Vector3 point)
    {
        var direction = (point - root.position).normalized;
        var rotateTo = Quaternion.LookRotation(direction).eulerAngles;
        return new Vector3(0, rotateTo.y, rotateTo.z);
    }

    protected virtual IEnumerator RotateToCube(Cube cube)
    {
        var rotateTo = GetRotationToCube(cube);
        yield return StartCoroutine(RotateTo(rotateTo));
    }
    protected virtual IEnumerator RotateToPoint(Vector3 point)
    {
        var rotateTo = GetRotationToPoint(point);
        yield return StartCoroutine(RotateTo(rotateTo));
    }

    protected virtual IEnumerator RotateTo(Vector3 rotateTo)
    {
        var rotateStart = root.rotation.eulerAngles;

        float distance = Vector3.Distance(rotateStart, rotateTo);
        float speedMoving = distance / timeRotate;

        while (distance > 0.01f)
        {
            Vector3 move = Vector3.MoveTowards(root.rotation.eulerAngles,
                rotateTo, speedMoving * CoroutinesHalpers.GetTimeDeltatimeScale(iTimeScaler));
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
        yield return StartCoroutine(this.MovingTransformTo(root, dropDownPoint, timeDropDown , iTimeScaler));
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
            positionMove = cube.currentPosition;
            yield return StartCoroutine(RotateToCube(cube));
            yield return StartCoroutine(JumpAndMove(cube));
            currentPosition = cube.currentPosition;
            positionMove = currentPosition;

            cube.OnPressMy(this);
        }

        moveCoroutine = null;

        if (OnEnd != null)
        {
            OnEnd(this);
        }
    }


    protected void SetTimeAnimationJump(float t)
    {
        if (jumpAnimationToTime != null)
        {
            jumpAnimationToTime.timeScale = timeScale;
            jumpAnimationToTime.time = t;
        }
    }

    protected virtual IEnumerator JumpAndMove(Cube cube)
    {
        yield return StartCoroutine(JumpAndMoveToPoint(cube.upSide.position));
    }

    protected virtual IEnumerator JumpAndMoveToPoint(Vector3 point)
    {
        float t = 0;
        var movingTo = point;
        var startTo = root.position;

        while (t < 1)
        {
            t += CoroutinesHalpers.GetTimeDeltatimeScale(iTimeScaler) / timeMove;
            t = Mathf.Clamp01(t);
            Vector3 pos = Vector3.Lerp(startTo, movingTo, t);
            pos = new Vector3(pos.x, pos.y + GetOffsetLerp(t), pos.z);
            root.position = pos;

            SetTimeAnimationJump(t);

            yield return null;
        }

        root.position = movingTo;
    }

    protected virtual float GetOffsetLerp(float t)
    {
        float retFloat = jumpAmplitude;

        if (t < 0.5f)
        {
            retFloat = jumpAmplitude * t;
        }
        else
        {
            retFloat = jumpAmplitude - (jumpAmplitude * t);
        }

        return retFloat;
    }
}
