using System.Collections;
using System.Collections.Generic;
using TowerDefence.notPlayer;
using TowerDefence.Towers;
using UnityEngine;

namespace TowerDefence.Managers
{
    public class TowerManager : MonoBehaviour
    {
        public BaseTower[] prefab;
        [SerializeField, Tooltip("Used to choose what tower to spawn")]
        private int index;
        [SerializeField]
        private Player player;

        /// <summary>
        /// Spawns the selected tower
        /// </summary>
        /// <param name="spawnLocation">The location the tower is spawned</param>
        public void SpawnTowerPrefab(Transform spawnLocation)
        {
            if (player.money >= prefab[index].cost)
            {
                Instantiate(prefab[index], spawnLocation.position, Quaternion.identity);
                player.PurchaseTower(prefab[index].Cost);
            }
            else
            {
                Debug.LogError("NEED MORE MONEY");
            }
            
        }

        public void SelectTower(int tower)
        {
            index = tower;
        }
    }

}
