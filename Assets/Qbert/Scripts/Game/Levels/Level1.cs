using UnityEngine;
using System.Collections;

//Уровень 1.Необходимо прыгнуть на кубик один раз, чтобы изменить цвет
//на нужный.Последующие прыжки не изменяют цвет.
public class Level1 : LevelBase
{
    public override bool OnQbertPressToCube(Cube cube, Qbert qbert)
    {
        cube.SetNextColor();
        return false;
    }
}
