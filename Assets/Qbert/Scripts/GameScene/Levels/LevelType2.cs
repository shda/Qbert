using UnityEngine;
using System.Collections;

//Уровень 2. Первый прыжок на кубик устанавливает промежуточный цвет.
//Повторный прыжок изменяет цвет на нужный.Последующие прыжки не изменяют цвет.
public class LevelType2 : LevelLogic
{
    public override LevelLogic.Type type
    {
        get { return LevelLogic.Type.LevelType2; ; }
    }

    public override bool OnQbertPressToCube(Cube cube, Qbert qbert)
    {
        cube.SetNextColor();
        return false;
    }
}
