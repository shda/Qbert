using UnityEngine;
using System.Collections;

public abstract class LevelBase : MonoBehaviour
{
    public Round[] rounds;
    public int roundCurrent;
    public Round currentRoundConfig;

    private float currentTime;
    private bool isLevelRun = false;

    protected LevelController levelController;

    public abstract void InitLevel();
    public abstract void OnCharacterPressToCube(Cube cube, Character character);

    public virtual void ResetLevel()
    {
        currentRoundConfig.ResetRound();
    }
    public abstract bool CheckToWin();

    public virtual void OnCollisionCharacters(Character character1, Character character2)
    {
        if (character1 is GameplayObject && character2 is Qbert)
        {
            OnCollisionQbertToGameplayObject(character1 as GameplayObject , character2 as Qbert);
        }
    }

    public void StartRound(int round)
    {
        roundCurrent = round;
        currentRoundConfig = rounds[round];
        currentRoundConfig.Init(levelController);
        currentRoundConfig.Run();
    }

    public virtual void OnCollisionQbertToGameplayObject(GameplayObject gameplayObject , Qbert qbert)
    {
        if (!gameplayObject.OnColisionToQbert(qbert))
        {
            ResetLevel();
        }
    }

    public virtual void StartLevel()
    {
        levelController.gameplayObjects.DestroyAllEnemies();
        isLevelRun = true;
        StartRound(0);
    }

    void Update()
    {
        if (isLevelRun)
        {
            currentRoundConfig.Update();
        }
    }

    public void SetController(LevelController controller)
    {
        levelController = controller;
    }
}
