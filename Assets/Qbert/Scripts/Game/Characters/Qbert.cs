using System;
using UnityEngine;
using System.Collections;

public class Qbert : Character
{
    [Header("Qbert")]
    public float jumpAmplitude = 1.0f;

    protected override IEnumerator MoveToCubeAnimation(Cube cube, Action<Character> OnEnd = null)
    {
        if (!isMoving)
        {
            yield return StartCoroutine(RotateToCube(cube));
            yield return StartCoroutine(JumpNamMove(cube , timeMove));
            //yield return StartCoroutine(this.MovingTransformTo(root, cube.upSide.position, timeMove));

            cube.OnPressMy();
        }

        moveCoroutine = null;

        currentLevel = cube.lineNumber;
        currentNumber = cube.numberInLine;

        if (OnEnd != null)
        {
            OnEnd(this);
        }
    }


    private IEnumerator JumpNamMove(Cube cube , float timeMove)
    {
        float t = 0;
        var movingTo = cube.upSide.position;
        var startTo = root.position;

        var currentPosition = startTo;

        while (t < 1)
        {
            t += Time.smoothDeltaTime / timeMove;
            Vector3 pos = Vector3.Lerp(startTo, movingTo, t);
            pos = new Vector3(pos.x, pos.y + GetOffsetLerp(t), pos.z);
            root.position = pos;
            yield return null;
        }
    }

    private float GetOffsetLerp(float t)
    {
        float retFloat = jumpAmplitude;

        if (t < 0.5f)
        {
            retFloat = retFloat*t;
        }
        else
        {
            retFloat = jumpAmplitude - (retFloat * t);
        }

        return retFloat;
    }
}
