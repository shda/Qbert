using UnityEngine;
using System.Collections;

public class ColoredCube : RedCube 
{
    public override Type typeGameobject
    {
        get { return Type.ColoredCube; }
    }

    public override bool OnPressCube(Cube cube)
    {
        int randomPress = 0; //Random.Range(0, 2);

        if (randomPress == 1)
        {
            cube.SetNextColor();
        }
        else
        {
            cube.SetLastColor();
        }

        //after press to cube, need check to win
        return false;
    }

    public override bool OnColisionToQbert(Qbert qbert)
    {
        OnStartDestroy();
        return true;
    }
}
