using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float SpawnRate
    {
        get
        {
            return spawnRate;
        }
    }

    [SerializeField]
    private float spawnRate;

    private float currentTime = 0;
    private EnemyManager enemyManager;

    private void Start()
    {
        enemyManager = EnemyManager.instance;
    }

    private void Update()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        // increment time by delta time if the current time is less than spawnrate
        if(currentTime < SpawnRate)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;

            // Attempt to spawn enemy
            if(enemyManager != null)
            {
                enemyManager.SpawnEnemy(transform);
            }
        }
    }
}
