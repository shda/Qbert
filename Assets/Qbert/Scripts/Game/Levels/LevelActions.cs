using UnityEngine;
using System.Collections;

public abstract class LevelActions : MonoBehaviour
{
    public GameplayObject.Type[] enemiesInLevel;
    public int maxEnemiesInLevel = 2;
    public float timeDelayCreateEnemies = 4.0f;

    private float currentTime;
    private bool isLevelRun = false;

    protected LevelController levelController;

    public abstract void InitLevel();
    public abstract void OnCharacterPressToCube(Cube cube, Character character);
    public abstract void ResetLevel();
    public abstract bool CheckToWin();

    public virtual void OnCollisionCharacters(Character character1, Character character2)
    {
        Debug.Log(character1.name + " " + character2.name);
    }
    public virtual void StartLevel()
    {
        levelController.enemies.DestroyAllEnemies();
        currentTime = timeDelayCreateEnemies;
        isLevelRun = true;
    }

    public void CreateEnemyIfNead()
    {
        if (levelController.enemies.gameplayObjectsList.Count < maxEnemiesInLevel)
        {
            GameplayObject.Type type = enemiesInLevel[Random.Range(0, enemiesInLevel.Length)];
            levelController.enemies.AddGameplayObjectToGame(type);
        }
    }

    void Update()
    {
        if (isLevelRun)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0)
            {
                currentTime = timeDelayCreateEnemies;
                CreateEnemyIfNead();
            }
        }
    }

    public void SetController(LevelController controller)
    {
        levelController = controller;
    }
}
