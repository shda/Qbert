using UnityEngine;
using System.Collections;

public class Level1 : LevelActions
{
    public int startPostitionQberLevel = 0;
    public int startPostitionQberNumber = 0;
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
        levelController.qbert.SetStartPosition(startPostitionQberLevel, startPostitionQberNumber);

        foreach (var cube in levelController.gameField.field)
        {
            CubeSetColor(cube, defaultColor);
            cube.isPress = false;
        }
        
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
}
