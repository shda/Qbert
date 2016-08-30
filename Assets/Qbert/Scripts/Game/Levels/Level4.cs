using UnityEngine;
using System.Collections;

//Уровень 4. Первый прыжок на кубик устанавливает промежуточный цвет. 
//Повторный прыжок изменяет цвет на нужный. Однако третий прыжок возвращает 
//кубику промежуточный цвет. Каждый последующий прыжок переключает цвет кубика между промежуточным 
//и нужным.
public class Level4 : LevelBase
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
