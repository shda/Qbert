﻿using UnityEngine;
using System.Collections;

//Уровень 5 (и до конца игры). Первый прыжок на кубик устанавливает промежуточный цвет.
//Второй прыжок изменяет цвет на нужный.Третий прыжок возвращает кубику начальный цвет.
//Этот цикл повторяется.
public class LevelType5 : LevelBehaviour
{
    public override LevelBehaviour.Type type
    {
        get { return LevelBehaviour.Type.LevelType5; ; }
    }

    public override bool OnQbertPressToCube(Cube cube, Qbert qbert)
    {
        if (cube.isSet)
        {
            cube.DropColor();
        }
        else
        {
            cube.SetNextColor();
        }
        return false;
    }
}