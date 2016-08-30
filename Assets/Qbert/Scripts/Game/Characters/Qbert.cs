using System;
using UnityEngine;
using System.Collections;

public class Qbert : Character
{
    public override void SetStartPosition(PositionCube point)
    {
        base.SetStartPosition(point);
        root.rotation = Quaternion.Euler(0, -135, 0);
    }
}
