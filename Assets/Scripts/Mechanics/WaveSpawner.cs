using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using UnityEngine;

namespace TowerDefence.Mechanics.Spawning
{
    public class WaveSpawner : MonoBehaviour
    {
        #region Variables
        [SerializeField, Min(0)] private int waveNumber, totalWaves, plebInWave, fastInWave, tankInWave;

        [SerializeField] private float lengthOfWave, localSpawnRate;

        [SerializeField, Tooltip("0: pleb, 1: fast, 2: tank")]
        private GameObject[] enemyPrefabs;

        private bool waveReady = false;

        private float currentTime = 0;
        private EnemyManager enemyManager;
        #endregion
        #region Properties
        public int WaveNumber { get => waveNumber; }
        private float spawnRate
        {
            get
            {
                return (plebInWave + fastInWave + tankInWave) / lengthOfWave;
            }
        }
        #endregion
        #region Start
        private void Start()
        {
            enemyManager = EnemyManager.instance;

            waveNumber = 0;
            totalWaves = 10; //for testing
        }
        #endregion
        #region Update
        void Update()
        {
            //or if wave called early
            if (!waveReady) //if wave not ready, set up new wave
            {
                WaveContents(); //determine contents of wave

                localSpawnRate = spawnRate; //calculate spawn rate for this wave

                waveReady = true; //wave is ready to go

                waveNumber++;
            }



            //start wave (button?)
            //spread wave spawning according to spawnrate
            //decrease count of spawnables as they spawn

        }
        #endregion
        #region Functions
        private void SpawnWave()
        {

        }
        private void WaveContents()
        {
            #region base numbers by difficulty
            //base easy values
            int pBase = 5;
            int fBase = 2;
            int tBase = 1;

            if (GameManager.gameDifficulty == GameDifficulty.Medium)
            {
                pBase = 7;
                fBase = 4;
                tBase = 2;
            }
            else if (GameManager.gameDifficulty == GameDifficulty.Hard)
            {
                pBase = 10;
                fBase = 8;
                tBase = 4;
            }
            #endregion
           
            //add to spawnables count according to wave number
            plebInWave += pBase * (waveNumber + 1);
            if (waveNumber >= 1)
            {
                fastInWave += fBase * (waveNumber + 1);
                if (waveNumber >= 2)
                {
                    tankInWave += tBase * (waveNumber + 1);
                }
            }
        }
        #endregion

        private void SpawnEnemies()
        {
            // increment time by delta time if the current time is less than spawnrate
            if (currentTime < spawnRate)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime = 0;

                // Attempt to spawn enemy
                if (enemyManager != null)
                {
                    enemyManager.SpawnEnemy(transform, enemyPrefabs[0]);
                }
            }
        }


    }
    public enum GameDifficulty
    {
        None,
        Easy,
        Medium,
        Hard,
    }
}