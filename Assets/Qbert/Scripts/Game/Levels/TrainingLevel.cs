using UnityEngine;
using System.Collections;

//Уровень щбучения.
public class TrainingLevel : LevelBase
{
    public override bool OnQbertPressToCube(Cube cube, Qbert qbert)
    {
        return false;
    }
}
