using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemies : MonoBehaviour
{
    public LevelController levelController;
    public Enemy[] enemiesPaterns;
    public List<Enemy> enemyList = new List<Enemy>();
    public Transform root;

    private Enemy CreateEnemy(Enemy.Type type)
    {
        foreach (var enemiesPatern in enemiesPaterns)
        {
            if (enemiesPatern.typeEnemy == type)
            {
                return enemiesPatern.Create(root , levelController);
            }
        }

        return null;
    }

    public Enemy AddEnemyToGame(Enemy.Type type)
    {
        var enemy = CreateEnemy(type);
        if (enemy)
        {
            enemy.Init();
            enemy.Run();;
            enemyList.Add(enemy);

            enemy.OnDestroyEvents = OnDestroyEvents;
        }

        return enemy;
    }

    private void OnDestroyEvents(Enemy enemy)
    {
        enemyList.Remove(enemy);

        enemy.gameObject.SetActive(false);
        Destroy(enemy.gameObject);
    }

    public void DestroyAllEnemies()
    {
        if (enemyList != null)
        {
            foreach (var enemy in enemyList)
            {
                enemy.gameObject.SetActive(false);
                Destroy(enemy.gameObject);
            }
        }

        enemyList = new List<Enemy>();
    }
}
