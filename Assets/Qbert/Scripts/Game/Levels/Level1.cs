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
            CubeSetColor(cube , pressColor);
            cube.isPress = true;
        }

        if (CheckToWin())
        {
            ResetLevel();
        }
    }

    private void CubeSetColor(Cube cube , Color color)
    {
        cube.upSideRender.material.color = color;
    }

    public override void ResetLevel()
    {
        levelController.qbert.SetStartPosition(startPostitionQber);

        foreach (var cube in levelController.gameField.field)
        {
            CubeSetColor(cube, defaultColor);
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
