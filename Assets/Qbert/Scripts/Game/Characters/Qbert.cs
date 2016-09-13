using System;
using UnityEngine;
using System.Collections;

public class Qbert : Character
{
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
}
