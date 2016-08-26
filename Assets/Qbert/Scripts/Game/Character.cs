using System;
using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    [Header("Character")]
    public Transform root;
    public GameField gameField;
    public float speedMove = 1.0f;

    public bool isMoving
    {
        get { return moveCoroutine != null; }
    }

    private Coroutine moveCoroutine;

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

    IEnumerator RotateToCube(Cube cube)
    {
        yield return null;

        float time = 0.1f;

        var rotateStart = root.rotation.eulerAngles;
        var rotateTo = Quaternion.LookRotation(cube.upSide.position).eulerAngles;
        rotateTo = new Vector3(0, rotateTo.y, rotateTo.z);

        float distance = Vector3.Distance(rotateStart, rotateTo);
        float speedMoving = distance / time;

        while (distance > 0.001f)
        {
            Vector3 move = Vector3.MoveTowards(root.rotation.eulerAngles, 
                rotateTo, speedMoving * Time.deltaTime);
            root.rotation = Quaternion.Euler(move);
            distance = Vector3.Distance(root.rotation.eulerAngles, rotateTo);
            yield return null;
        }

        /*
        root.rotation = Quaternion.LookRotation(cube.upSide.position);

        root.rotation = Quaternion.Euler( 
            new Vector3(0 , root.rotation.eulerAngles.y , root.rotation.eulerAngles.z));
            */
        /*
        Vector3 direction = (cube.upSide.position - root.position).normalized;


        if (!isMoving)
        {
            yield return null;

            yield return StartCoroutine(this.MovingTransformTo(root, cube.upSide.position, speedMove));
        }
        */


        // moveCoroutine = null;
    }

    IEnumerator MoveToCubeAnimation(Cube cube , Action<Character> OnEnd = null)
    {
        if (!isMoving)
        {
            yield return StartCoroutine(RotateToCube(cube));
            yield return StartCoroutine(this.MovingTransformTo(root, cube.upSide.position, speedMove));
        }

        moveCoroutine = null;

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
        StartCoroutine(TestMove());
    }
	
	void Update () 
	{
	
	}
}
