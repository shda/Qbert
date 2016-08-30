using UnityEngine;
using System.Collections;

//Уровень 2. Первый прыжок на кубик устанавливает промежуточный цвет.
//Повторный прыжок изменяет цвет на нужный.Последующие прыжки не изменяют цвет.
public class Level2 : LevelBase
{
    public override bool OnQbertPressToCube(Cube cube, Qbert qbert)
    {
        cube.SetNextColor();
        return false;
    }
}
