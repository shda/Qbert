using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameField : MonoBehaviour
{
    public GameFieldGenerator mapGenerator;
    public Cube[] field; 

    void Awake()
    {
        ParseMap();
    }

    private void ParseMap()
    {
        field = mapGenerator.GetComponentsInChildren<Cube>();
    }

    public Cube GetCube(int line, int number)
    {
        return field.FirstOrDefault(x => x.lineNumber == line && x.numberInLine == number);
    }

    public Cube GetCubeDirection(ControlController.ButtonType buttonType , int level , int number)
    {
        int levels = mapGenerator.levels;

        Cube findCube = null;

        int findLevel = -1;
        int findNumber = -1;

        if (buttonType == ControlController.ButtonType.DownLeft)
        {
            if (level < levels - 1)
            {
                findLevel = level + 1;
                findNumber = number;
            }
        }
        else if (buttonType == ControlController.ButtonType.UpRight)
        {
            if (level > 0)
            {
                findLevel = level - 1;
                findNumber = number;
            }
        }
        else if (buttonType == ControlController.ButtonType.DownRight)
        {
            if (level < levels - 1)
            {
                findLevel = level + 1;
                findNumber = number + 1;
            }

        }
        else if (buttonType == ControlController.ButtonType.UpLeft)
        {
            if (level > 0)
            {
                findLevel = level - 1;
                findNumber = number - 1;
            }
        }


        return GetCube(findLevel, findNumber);
    }

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
