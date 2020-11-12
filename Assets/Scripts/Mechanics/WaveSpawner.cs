using System.Collections;
using System.Collections.Generic;
using TowerDefence.Managers;
using UnityEngine;

namespace TowerDefence.Mechanics.Spawning
{
    public class WaveSpawner : MonoBehaviour
    {
        #region Variables
        [SerializeField] private int waveNumber, totalWaves;

        [Min(0)] private int plebSetInWave, fastSetInWave, tankSetInWave;

        [SerializeField, Tooltip("0: pleb, 1: fast, 2: tank")]
        private GameObject[] enemyPrefabs;

        private bool waveReady = false, inWave = false;

        private float spawnRateTimer = 0, waveTimer = 0, lengthOfWave, localSpawnRate, waveLengthBase;

        private EnemyManager enemyManager;
        #endregion
        
        #region Properties
        public int WaveNumber { get => waveNumber; }
        public int TotalWaves { get => totalWaves; }
        public int WaveContent { get => plebSetInWave + fastSetInWave + tankSetInWave; }
        private float WaveLength { get => waveLengthBase + waveNumber * 2; }
        private float SpawnRate { get => WaveLength / WaveContent; }
        #endregion
        
        #region Start
        private void Start()
        {
            enemyManager = EnemyManager.instance;

            waveNumber = 0;
            waveReady = false;

            if (GameManager.gameDifficulty == GameDifficulty.None) //set default game difficulty to easy
            {
                GameManager.gameDifficulty = GameDifficulty.Easy;
            }

            #region set number of waves and base length by difficulty
            switch (GameManager.gameDifficulty)
            {
                case GameDifficulty.None:
                    Debug.LogError("Something ain't right, chief.");
                    break;
                case GameDifficulty.Easy:
                    totalWaves = 10;
                    waveLengthBase = 5;
                    break;
                case GameDifficulty.Medium:
                    totalWaves = 15;
                    waveLengthBase = 6;
                    break;
                case GameDifficulty.Hard:
                    totalWaves = 20;
                    waveLengthBase = 7;
                    break;
                default:
                    break;
            }
            #endregion
        }
        #endregion
        
        #region Update
        void Update()
        {
            #region check if wave ready
            if (!waveReady) //if wave not ready, set up new wave
            {
                WaveContents(); //determine contents of wave

                waveReady = true; //wave is ready to go

                waveNumber++;
            }
            #endregion
           
            #region check if in wave
            if (inWave)
            {
                SpawnWave();
                if (waveTimer > lengthOfWave)
                {
                    //showing off my ternary operator skills right here
                    //if there are enemies left, we're still in the wave, otherwise the wave ends
                    inWave = (plebSetInWave + fastSetInWave + tankSetInWave > 0) ? true : false;
                }
                else
                {
                    waveTimer += Time.deltaTime;
                }
            }
            #endregion
        }
        #endregion
        
        #region Functions
        #region start wave
        /// <summary>
        /// Tells wave to start, attach to UI button please.
        /// </summary>
        public void StartWave()
        {
            if (waveReady && !inWave)
            {
                localSpawnRate = SpawnRate; //calculate spawn rate for this wave
                inWave = true;
                waveTimer = 0;
                lengthOfWave = WaveLength;
            }
        }
        #endregion
        
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
                if (plebSetInWave > 0) //if this wave has plebs
                {
                    SpawnEnemies(0, 4); //spawn plebs
                }
                else if (fastSetInWave > 0) //fast
                {
                    SpawnEnemies(1, 1);
                }
                else if (tankSetInWave > 0) //tank
                {
                    SpawnEnemies(2, 2);
                }

                if (WaveContent <= 0) //if this wave is now empty
                {
                    inWave = false; //end current wave
                    waveReady = false; //prepare next wave
                }
            }
            else
            {
                spawnRateTimer += Time.deltaTime;
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
                    plebSetInWave--;
                    break;
                case 1:
                    fastSetInWave--;
                    break;
                case 2:
                    tankSetInWave--;
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
                    pBase = 2;
                    fBase = 1;
                    tBase = 1;
                    break;
                case GameDifficulty.Medium:
                    pBase = 4;
                    fBase = 2;
                    tBase = 1;
                    break;
                case GameDifficulty.Hard:
                    pBase = 8;
                    fBase = 4;
                    tBase = 2;
                    break;
                default:
                    Debug.LogError("No game difficulty detected, fam.");
                    break;
            }
            #endregion

            //add to spawnables count according to wave number
            plebSetInWave += pBase * (waveNumber + 1);
            if (waveNumber >= 1)
            {
                fastSetInWave += fBase * (waveNumber + 1);
                if (waveNumber >= 2)
                {
                    tankSetInWave += tBase * (waveNumber);
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