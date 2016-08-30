using UnityEngine;
using System.Collections;

//Уровень 3. Первый прыжок на кубик изменяет цвет на нужный.Однако повторный 
//прыжок возвращает кубику начальный цвет. 
//Каждый последующий прыжок переключает цвет кубика между начальным и нужным.
public class Level3 : LevelBase
{
    public override bool OnQbertPressToCube(Cube cube, Qbert qbert)
    {
        if (cube.isSet)
        {
            cube.SetLastColor();
        }
        else
        {
            cube.SetNextColor();
        }

        return false;
    }
}
