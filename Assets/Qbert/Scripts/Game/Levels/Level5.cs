using UnityEngine;
using System.Collections;

//Уровень 5 (и до конца игры). Первый прыжок на кубик устанавливает промежуточный цвет.
//Второй прыжок изменяет цвет на нужный.Третий прыжок возвращает кубику начальный цвет.
//Этот цикл повторяется.
public class Level5 : LevelBase
{
    public override bool OnQbertPressToCube(Cube cube, Qbert qbert)
    {
        if (cube.isSet)
        {
            cube.SetLastColor();
            cube.colorLerp.valueLerp = 0.0f;
            cube.SetLastColor();
            cube.colorLerp.valueLerp = 0.0f;
            cube.colorLerp.value = 0.0f;
        }
        else
        {
            cube.SetNextColor();
        }
        return false;
    }
}
