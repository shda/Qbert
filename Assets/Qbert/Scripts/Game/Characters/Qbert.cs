using System;
using UnityEngine;
using System.Collections;

public class Qbert : GameplayObject
{
    public virtual Type typeObject
    {
        get { return Type.Qbert; }
    }

    public override void SetStartPosition(PositionCube startPositionQbert)
    {
        base.SetStartPosition(startPositionQbert);
        root.rotation = Quaternion.Euler(0, -135, 0);
    }

    public override bool OnPressCube(Cube cube)
    {
        if (cube.lastState < cube.stateColor && !cube.isSet)
        {
            AddScore(ScorePrice.pressCubeMediumColor);
        }
        else if (cube.lastState < cube.stateColor && cube.isSet)
        {
            AddScore(ScorePrice.pressCubeNeedColor);
        }
        else if (cube.lastState > cube.stateColor && !cube.isSet)
        {
            AddScore(ScorePrice.pressCubeMediumColor);
        }
        
        return base.OnPressCube(cube);
    }

    public void OnCommandMove(DirectionMove.Direction buttonType)
    {
        if (!isFrize)
        {
            Cube findCube = levelController.gameField.GetCubeDirection(buttonType, currentPosition);
            if (findCube)
            {
                MoveToCube(findCube);
            }
            else
            {
                var pointToJump = levelController.gameField.GetPointCubeDirection(buttonType, currentPosition);
                var findObject = levelController.gameplayObjects.GetGamplayObjectInPoint(pointToJump);
                if (findObject && findObject.typeObject == Type.Transport)
                {
                    MoveToPointAndDropDown(findObject.transform.position);
                }
                else
                {
                    Vector3 newPos = root.position + levelController.gameField.GetOffsetDirection(buttonType);
                    MoveToPointAndDropDown(newPos, character =>
                    {
                        levelController.OnQubertDead();
                        Debug.Log("OnDead");
                    });
                }


            }
        }
    }
}
