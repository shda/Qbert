using UnityEngine;
using System.Collections;

//Уровень 1.Необходимо прыгнуть на кубик один раз, чтобы изменить цвет
//на нужный.Последующие прыжки не изменяют цвет.
public class LevelType1 : LevelBehaviour
{
    public override LevelBehaviour.Type type
    {
        get { return LevelBehaviour.Type.LevelType1; ; }
    }

    public override bool OnQbertPressToCube(Cube cube, Qbert qbert)
    {
        cube.SetNextColor();
        return false;
    }
}
