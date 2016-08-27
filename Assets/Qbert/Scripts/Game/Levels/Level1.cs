using UnityEngine;
using System.Collections;

public class Level1 : LevelActions
{
    public PointCube startPostitionQber;
    public Color defaultColor;
    public Color pressColor;

    public override void OnCharacterPressToCube(Cube cube, Character character)
    {
        if (character is Qbert)
        {
            cube.colorLerp.value = 1.0f;
            //CubeSetColor(cube , pressColor);
            cube.isPress = true;
        }

        if (CheckToWin())
        {
            ResetLevel();
        }
    }

    private void CubeSetColor(Cube cube , Color color)
    {
        cube.colorLerp.SetColorEnd(pressColor);
        cube.colorLerp.SetColorEnd(pressColor);
        cube.colorLerp.value = 0.0f;
    }

    public override void ResetLevel()
    {
        levelController.qbert.SetStartPosition(startPostitionQber);

        foreach (var cube in levelController.gameField.field)
        {
            cube.colorLerp.value = 0.0f;
            //CubeSetColor(cube, defaultColor);
            cube.isPress = false;
        }

        levelController.enemies.DestroyAllEnemies();
    }

    public override bool CheckToWin()
    {
        foreach (var cube in levelController.gameField.field)
        {
            if (!cube.isPress)
            {
                return false;
            }
        }

        return true;
    }

    public override void InitLevel()
    {
        ResetLevel();
    }

    public override void OnCollisionCharacters(Character character1, Character character2)
    {
        ResetLevel();
    }
}
