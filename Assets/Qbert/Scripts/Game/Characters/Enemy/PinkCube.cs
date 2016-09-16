using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class PinkCube : RedCube 
{
    public enum SideCube
    {
        Left,
        Right,
    }

    public SideCube sideCube;
    public Transform jumpRoot;
    public Vector3 jumpVector;

    public PositionCube stuckPosition;

    
    public float maxStuckTime = 4.0f;

    private bool stuck = false;
    private float currentStuckTimer;

    public override Type typeObject
    {
        get { return Type.PinkCube; }
    }

    public override GameplayObject Create(Transform root, LevelController levelController)
    {
        //Todo:fix this
        /*
        if (startPositions == null || startPositions.Length == 0)
        {
            startPositions = GetStartPositions(levelController);
        }

        List<PositionCube> positions = new List<PositionCube>(startPositions);
        positions = positions.Mix();

        foreach (var positionCube in positions)
        {
            var gaToPoint = levelController.gameplayObjects.GetGamplayObjectInPoint(positionCube);
            if (gaToPoint == null || gaToPoint.CanJumpToMy())
            {
                if (positionCube.position == 0)
                {
                    sideCube = SideCube.Left;
                    
                }
                else
                {
                    sideCube = SideCube.Right;
                }

                currentPosition = positionCube;

                return SetObject(root, levelController, positionCube);
            }
        }
        */
        return null;
    }

    public PositionCube[] GetStartPositions(LevelController levelController)
    {
        int level = levelController.gameField.mapGenerator.levels - 1;

        return new[] {new PositionCube(level, 0), new PositionCube(level, level),};
    }   

    public override void SetStartPosition(PositionCube point)
    {
        StopAllCoroutines();

        //Todo:fix this
        /*
        stuckPosition = point;
        positionMove = point;

        var startCube = levelController.gameField.GetCube(point);
        if (startCube)
        {
            if (sideCube == SideCube.Left)
            {
                root.position = startCube.leftSide.position;
            }
            else
            {
                root.position = startCube.rightSide.position;
            }

            root.rotation = startCube.rightSide.rotation;
        }
        moveCoroutine = null;
        */
    }

    protected override IEnumerator DropToCube()
    {
        var targetPosition = root.position;

        var cube = levelController.gameField.GetCube(stuckPosition);

        if (sideCube == SideCube.Left)
        {
            root.position += Vector3.left*heightDrop;
            root.rotation = cube.leftSide.rotation;
        }
        else
        {
            root.position += Vector3.right * heightDrop;
            root.rotation = cube.rightSide.rotation;
        }

        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(this.MovingTransformTo(root, targetPosition, 0.5f, iTimeScaler));
        yield return null;
    }

    protected override IEnumerator WorkThead()
    {
        //yield return null;
        yield return StartCoroutine(DropToCube());

        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (!isMoving)
            {
                Cube cubeTarget = null; GetNextCube();

                if (GetMoveCube(ref cubeTarget))
                {
                    stuck = false;

                    if (cubeTarget)
                    {
                        MoveToCube(cubeTarget);
                        yield return StartCoroutine(this.WaitForSecondITime(1.0f, iTimeScaler));
                    }
                    else
                    {
                        yield return StartCoroutine(ReachedLowerLevel());
                        yield break;
                    }
                }
                else
                {
                    stuck = true;
                }
            }
        }

        yield return null;
    }

    protected virtual IEnumerator ReachedLowerLevel()
    {
        Vector3 newPos = root.position - jumpVector;
       // root.position = newPos;

        yield return StartCoroutine(JumpAndMoveToPoint(newPos));
        yield return StartCoroutine(DropDown());
        OnStartDestroy();
    }

    protected override IEnumerator DropDown()
    {
        Vector3 dropDownPoint = root.position - new Vector3(dropDownHeight * 0.5f, 0, -dropDownHeight * 0.5f);

        if (sideCube == SideCube.Left)
        {
            dropDownPoint = root.position + new Vector3(dropDownHeight * 0.5f, 0, dropDownHeight * 0.5f);
        }

        yield return StartCoroutine(this.MovingTransformTo(root, dropDownPoint, timeDropDown, iTimeScaler));
    }

    public override Vector3 GetRotationToCube(Cube cube)
    {
        if (sideCube == SideCube.Left)
        {
            return cube.leftSide.rotation.eulerAngles;
        }

        return cube.rightSide.rotation.eulerAngles;
    }

    protected override IEnumerator RotateToCube(Cube cube)
    {
        var rotateTo = GetRotationToCube(cube);
        yield return StartCoroutine(RotateTo(rotateTo));
    }

    protected override IEnumerator JumpAndMove(Cube cube)
    {
        Vector3 cubeSide = GetSideCube(cube);

        jumpVector = root.position - cubeSide;

        yield return StartCoroutine(JumpAndMoveToPoint(GetSideCube(cube)));
        yield return null;
    }

    protected override IEnumerator MoveToCubeAnimation(Cube cube, Action<Character> OnEnd = null)
    {
        if (!isMoving)
        {
            positionMove = GetHungPosition(cube.currentPosition);
            yield return StartCoroutine(RotateToCube(cube));
            yield return StartCoroutine(JumpAndMove(cube));
            stuckPosition = cube.currentPosition;
            positionMove = new PositionCube(-10,-10);

            currentPosition = GetHungPosition(stuckPosition);

            cube.OnPressMy(this);
        }

        moveCoroutine = null;

        if (OnEnd != null)
        {
            OnEnd(this);
        }
    }

    public PositionCube GetHungPosition(PositionCube stuckPosition)
    {
        if (sideCube == SideCube.Left)
        {
            return new PositionCube(stuckPosition.line + 1, stuckPosition.position);
        }

        return new PositionCube(stuckPosition.line + 1, stuckPosition.position + 1);
    }

    protected override IEnumerator JumpAndMoveToPoint(Vector3 point)
    {
        float t = 0;
        var movingTo = point;
        var startTo = root.position;

        while (t < 1)
        {
            t += CoroutinesHalpers.GetTimeDeltatimeScale(iTimeScaler) / timeMove;
            t = Mathf.Clamp01(t);
            Vector3 pos = Vector3.Lerp(startTo, movingTo, t);
            JumpOffset(t);
            SetTimeAnimationJump(t);
            root.position = pos;
            yield return null;
        }

        root.position = movingTo;
    }

    protected virtual void JumpOffset(float t)
    {
        float lerp = GetOffsetLerp(t);
        jumpRoot.localPosition = new Vector3(0, 0, -lerp);
    }

    protected override float GetOffsetLerp(float t)
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

    public GameplayObject GetGamplayObjectInPoint(PositionCube point)
    {
        foreach (var gameplayObject in levelController.gameplayObjects.gameplayObjectsList)
        {
            if (gameplayObject.currentPosition == point || gameplayObject.positionMove == point && gameplayObject != this)
            {
                return gameplayObject;
            }
        }

        return null;
    }

    private bool GetMoveCube(ref Cube retCube)
    {
        retCube = GetNextCube();

        if (retCube && currentStuckTimer < maxStuckTime)
        {
            //var gObject     = GetGamplayObjectInPoint(retCube.cubePosition);
            var hungObject  = GetGamplayObjectInPoint(GetHungPosition(retCube.currentPosition));

            if (/*gObject ||*/ hungObject)
            {
                return false;
            }
        }

        currentStuckTimer = 0;

        return true;
    }

    private Cube GetNextCube()
    {
        bool down = Random.Range(0, 2) == 0;

        int x, y;

        if (sideCube == SideCube.Left)
        {
            if (stuckPosition.position < stuckPosition.line)
            {
                if (down)
                {
                    x = stuckPosition.line - 1;
                    y = stuckPosition.position;
                }
                else
                {
                    x = stuckPosition.line ;
                    y = stuckPosition.position + 1;
                }

                return levelController.gameField.GetCube(new PositionCube(x, y));
            }
        }
        else
        {
            if (stuckPosition.position > 0)
            {
                if (down)
                {
                    x = stuckPosition.line - 1;
                    y = stuckPosition.position - 1;
                }
                else
                {
                    x = stuckPosition.line;
                    y = stuckPosition.position - 1;
                }

                return levelController.gameField.GetCube(new PositionCube(x, y));
            }
        }

        return null;
    }

    public Vector3 GetSideCube(Cube cude)
    {
        if (sideCube == SideCube.Left)
        {
            return cude.leftSide.position;
        }

        return cude.rightSide.position;
    }


    void Update()
    {
        if (stuck)
        {
            currentStuckTimer += timeScale*Time.deltaTime;
        }
        else
        {
            currentStuckTimer = 0;
        }
    }

}
