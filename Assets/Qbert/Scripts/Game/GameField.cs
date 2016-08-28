using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameField : MonoBehaviour
{
    public GameFieldGenerator mapGenerator;
    public Action<Cube, Character> OnPressCubeEvents; 
    public Cube[] field;

    public void Init()
    {
        ParseMap();
        ConnectEvents();
    }


    public void ConnectEvents()
    {
        foreach (var cube in field)
        {
            cube.OnPressEvents = OnPressEvents;
        }
    }

    private void OnPressEvents(Cube cube, Character character)
    {
        if (OnPressCubeEvents != null)
        {
            OnPressCubeEvents(cube , character);
        }
    }

    public void ParseMap()
    {
        field = mapGenerator.GetComponentsInChildren<Cube>();
        ConnectEvents();
    }

    public Cube GetCube(PositionCube cube)
    {
        return field.FirstOrDefault(x => x.cubePosition == cube);
    }

    public Cube GetCubeDirection(DirectionMove.Direction buttonType , PositionCube point)
    {
        return GetCube(GetPointCubeDirection(buttonType , point));
    }

    public PositionCube GetPointCubeDirection(DirectionMove.Direction buttonType, PositionCube point)
    {
        int findLevel = -1;
        int findNumber = -1;

        if (buttonType == DirectionMove.Direction.DownLeft)
        {
            findLevel = point.line + 1;
            findNumber = point.position;
        }
        else if (buttonType == DirectionMove.Direction.UpRight)
        {
            findLevel = point.line - 1;
            findNumber = point.position;
        }
        else if (buttonType == DirectionMove.Direction.DownRight)
        {
            findLevel = point.line + 1;
            findNumber = point.position + 1;
        }
        else if (buttonType == DirectionMove.Direction.UpLeft)
        {
            findLevel = point.line - 1;
            findNumber = point.position - 1;
        }

        return new PositionCube(findLevel , findNumber);
    }

    public Vector3 GetOffsetDirection(DirectionMove.Direction direction)
    {
        float x = 0, y = 0;

        if (direction == DirectionMove.Direction.UpRight)
        {
            x += 1;
            y += 1;
        }
        else
        if (direction == DirectionMove.Direction.UpLeft)
        {
            x += -1;
            y += 1;
        }
        if (direction == DirectionMove.Direction.DownRight)
        {
            x += 1;
            y += -1;
        }
        else
        if (direction == DirectionMove.Direction.DownLeft)
        {
            x += -1;
            y += -1;
        }


        return new Vector3(x * mapGenerator.offsetX, 0,
            y * mapGenerator.offsetX);
    }
}
