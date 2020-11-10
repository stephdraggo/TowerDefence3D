using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Managers
{
    public class TowerManager : MonoBehaviour
    {
        public GameObject[] prefab;
        [SerializeField, Tooltip("Used to choose what tower to spawn")]
        private int index;

        /// <summary>
        /// Spawns the selected tower
        /// </summary>
        /// <param name="spawnLocation">The location the tower is spawned</param>
        public void SpawnTowerPrefab(Transform spawnLocation)
        {
            Instantiate(prefab[index], spawnLocation.position, Quaternion.identity);
        }

        public void SelectTower(int tower)
        {
            index = tower;
        }
    }

}
