using System.Collections;
using System.Collections.Generic;
using TowerDefence.Managers;
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

        private bool waveReady = false,inWave=false;


        private float spawnRateTimer = 0,waveTimer=0;
        private EnemyManager enemyManager;
        #endregion
        #region Properties
        public int WaveNumber { get => waveNumber; }
        private float spawnRate
        {
            get
            {
                return (plebInWave/4 + fastInWave + tankInWave/2) / lengthOfWave;
            }
        }
        #endregion
        #region Start
        private void Start()
        {
            enemyManager = EnemyManager.instance;

            waveNumber = 0;

            //for testing
            totalWaves = 10;
            lengthOfWave = 5;
            waveReady = false;

            GameManager.gameDifficulty = GameDifficulty.Easy;
        }
        #endregion
        #region Update
        void Update()
        {
            //or if wave called early
            if (!waveReady) //if wave not ready, set up new wave
            {
                WaveContents(); //determine contents of wave

                waveReady = true; //wave is ready to go

                waveNumber++;
            }

            if (inWave)
            {
                SpawnWave();
                if (waveTimer > lengthOfWave)
                {
                    inWave = false;
                }
            }


            waveTimer += Time.deltaTime;
            spawnRateTimer += Time.deltaTime;
        }
        #endregion
        #region Functions
        public void StartWave()
        {
            localSpawnRate = spawnRate; //calculate spawn rate for this wave
            inWave = true;
            waveTimer = 0;
        }
        #region spawn wave
        /// <summary>
        /// Spawn contents of wave spread out over wave time.
        /// </summary>
        private void SpawnWave()
        {
            if (spawnRateTimer > localSpawnRate)
            {
                spawnRateTimer = 0; //reset timer

                //spawn here
                if (plebInWave > 0)
                {
                    SpawnEnemies(0, 4);
                }
                else if (fastInWave > 0)
                {
                    SpawnEnemies(1, 1);
                }
                else if (tankInWave > 0)
                {
                    SpawnEnemies(2, 2);
                }
                else
                {
                    waveReady = false; //this prepared wave has been used up
                }

            }
        }
        #endregion
        #region spawn enemies
        /// <summary>
        /// Spawn a given number of enemies of a given type.
        /// </summary>
        /// <param name="_type">type of enemy to spawn</param>
        /// <param name="_amount">amount of that enemy type to spawn</param>
        private void SpawnEnemies(int _type, int _amount)
        {
            //spawn amount of type
            for (int i = 0; i < _amount; i++)
            {
                enemyManager.SpawnEnemy(transform, enemyPrefabs[_type]);
            }

            //decrease the number to spawn
            switch (_type)
            {
                case 0:
                    plebInWave -= _amount;
                    break;
                case 1:
                    fastInWave -= _amount;
                    break;
                case 2:
                    tankInWave -= _amount;
                    break;

                default:
                    Debug.LogError("Index out of bounds, fam.");
                    break;
            }
        }
        #endregion
        #region wave contents
        /// <summary>
        /// Determine contents of wave according to wave number and difficulty setting.
        /// </summary>
        private void WaveContents()
        {
            #region base numbers by difficulty
            int pBase = 0;
            int fBase = 0;
            int tBase = 0;
            switch (GameManager.gameDifficulty)
            {
                case GameDifficulty.None:
                    Debug.LogError("No game difficulty detected, fam.");
                    break;
                case GameDifficulty.Easy:
                    pBase = 8;
                    fBase = 2;
                    tBase = 1;
                    break;
                case GameDifficulty.Medium:
                    pBase = 16;
                    fBase = 4;
                    tBase = 2;
                    break;
                case GameDifficulty.Hard:
                    pBase = 32;
                    fBase = 8;
                    tBase = 4;
                    break;
                default:
                    Debug.LogError("No game difficulty detected, fam.");
                    break;
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
    
        #endregion



    }
    public enum GameDifficulty
    {
        None,
        Easy,
        Medium,
        Hard,
    }
}