using UnityEngine;
using System.Collections;

//Уровень 1.Необходимо прыгнуть на кубик один раз, чтобы изменить цвет
//на нужный.Последующие прыжки не изменяют цвет.
public class LevelType1 : LevelLogic
{
    public override LevelLogic.Type type
    {
        get { return LevelLogic.Type.LevelType1; ; }
    }

    public override bool OnQbertPressToCube(Cube cube, Qbert qbert)
    {
        cube.SetNextColor();
        return false;
    }
}
