using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager instance = null;

        [SerializeField]
        private GameObject enemyPrefab;

        public List<Enemy> aliveEnemies = new List<Enemy>();



        /// <summary>
        /// Spawns an Enemy gameObject then adds that enemy to the alive enemies lists
        /// </summary>
        public void SpawnEnemy(Transform _spawner)
        {
            // spawns a new enemy
            GameObject newEnemy = Instantiate(enemyPrefab, _spawner.position, enemyPrefab.transform.rotation);

            newEnemy.GetComponent<Enemy>().enemy = this; //give the enemy a reference to this enemy manager

            // sets the parent of the objects
            newEnemy.transform.SetParent(_spawner);

            // adds the spawned enemy to the alive enemy list
            aliveEnemies.Add(newEnemy.GetComponent<Enemy>());
        }

        /// <summary>
        /// spawns enemy of a given type and adds to list
        /// </summary>
        /// <param name="_spawner">location on map</param>
        /// <param name="_enemyPrefab">which enemy to spawn</param>
        public void SpawnEnemy(Transform _spawner, GameObject _enemyPrefab)
        {
            // spawns a new enemy
            GameObject newEnemy = Instantiate(_enemyPrefab, _spawner.position, _enemyPrefab.transform.rotation);

            newEnemy.GetComponent<Enemy>().enemy = this; //give the enemy a reference to this enemy manager

            // sets the parent of the objects
            newEnemy.transform.SetParent(_spawner);

            // adds the spawned enemy to the alive enemy list
            aliveEnemies.Add(newEnemy.GetComponent<Enemy>());
        }
        /// <summary>
        /// Used to kills the enemy and then removes the dead enemy from the list
        /// </summary>
        /// <param name="_enemy"></param>
        public void KillEnemy(Enemy _enemy)
        {
            int index = aliveEnemies.IndexOf(_enemy);
            if (index != -1)
            {
                // the enemy exists and can be killed
                Destroy(_enemy.gameObject);
                // removes enemy from list
                aliveEnemies.RemoveAt(index);

            }
        }

        /// <summary>
        /// checks through all enemies alive in game, finding the closest enemy within a certain range.
        /// </summary>
        /// <param name="_target">The object we are comparing the distance to</param>
        /// <param name="_maxRange">the max range we are finding enemies within</param>
        /// <param name="_minRange">the range the enemies need to atleast be from the target</param>
        /// <returns>The list of enemies wihtin the given range</returns>
        public Enemy[] GetClosestEnemies(Transform _target, float _maxRange, float _minRange = 0)
        {
            // making a list of close enemies
            List<Enemy> closeEnemies = new List<Enemy>();

            if (aliveEnemies != null) //if there are enemies in scene
            {
                // for each enemy in the alive enemy list 
                foreach (Enemy enemy in aliveEnemies)
                {
                    // get the distance between a and b (enemy and target)
                    float dist = Vector3.Distance(enemy.transform.position, _target.position);
                    // if the distance is less then the max range and great then the min range
                    if (dist < _maxRange && dist > _minRange)
                    {
                        // enemy is added to the list
                        closeEnemies.Add(enemy);
                    }
                }
            }
            // converts list to array
            return closeEnemies.ToArray();
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
