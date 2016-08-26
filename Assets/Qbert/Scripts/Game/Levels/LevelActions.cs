using UnityEngine;
using System.Collections;

public abstract class LevelActions : MonoBehaviour
{
    protected LevelController levelController;

    public abstract void InitLevel();
    public abstract void OnCharacterPressToCube(Cube cube, Character character);
    public abstract void ResetLevel();

    public abstract bool CheckToWin();

    public void SetController(LevelController controller)
    {
        levelController = controller;
    }
}
