using System;
using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    [Header("Character")]
    public Transform root;
    public GameField gameField;
    public float timeMove = 1.0f;
    public float timeRotate = 0.2f;

    public int currentLevel = 0;
    public int currentNumber = 0;

    public bool isMoving
    {
        get { return moveCoroutine != null; }
    }

    protected Coroutine moveCoroutine;

    public  bool MoveToCube(Cube cube)
    {
        return MoveToCube(cube.lineNumber , cube.numberInLine);
    }

    public bool MoveToCube(int line , int numberInLine)
    {
        var cube = gameField.GetCube(line, numberInLine);

        if (cube && moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(MoveToCubeAnimation(cube));
            return true;
        }

        return false;
    }

    protected IEnumerator RotateToCube(Cube cube)
    {
        var rotateStart = root.rotation.eulerAngles;

        var direction = (cube.upSide.position - root.position).normalized;

        var rotateTo = Quaternion.LookRotation(direction).eulerAngles;
        rotateTo = new Vector3(0, rotateTo.y, rotateTo.z);

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

    protected virtual IEnumerator MoveToCubeAnimation(Cube cube , Action<Character> OnEnd = null)
    {
        if (!isMoving)
        {
            yield return StartCoroutine(RotateToCube(cube));
            yield return StartCoroutine(this.MovingTransformTo(root, cube.upSide.position, timeMove));

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

    IEnumerator TestMove()
    {
        yield return new WaitForSeconds(0.5f);
        MoveToCube(1, 1);
    }

    void Start ()
    {
       // StartCoroutine(TestMove());
    }
	
	void Update () 
	{
	
	}
}
