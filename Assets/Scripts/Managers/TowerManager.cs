using System.Collections;
using System.Collections.Generic;
using TowerDefence.notPlayer;
using TowerDefence.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.Managers
{
    public class TowerManager : MonoBehaviour
    {
        [System.Serializable]
        public class TowerStats
        {
            [Tooltip("the tower we want to get the stats from")] public BaseTower tower;
            [Tooltip("The place to display the cost")] public Text costText;
            [Tooltip("The place to display the name")] public Text nameText;
            [Tooltip("The sprite we want to display for the tower")] public Sprite towerSprite;
            [Tooltip("the place to display the sprite")] public Image image;

            /// <summary>
            /// Sets what will display for the tower
            /// </summary>
            public void Display()
            {
                costText.text = string.Format("${0}", tower.cost);
                nameText.text = tower.TowerName;
                image.sprite = towerSprite;
            }
        }

        #region Variables
        [SerializeField, Tooltip("Displays the clicked on towers description")]
        private Text towerDescription;
        [SerializeField, Tooltip("Displays the clicked on towers name")]
        private Text towerName;
        #region MiscVariables
        [SerializeField, Tooltip("The Towers we want to spawn")]
        private BaseTower[] prefab;
        [SerializeField]
        private Player player;
        [SerializeField, Tooltip("The list of stats to display for each tower")]
        private List<TowerStats> towerStats = new List<TowerStats>();

        [Space]

        [SerializeField, Tooltip("Used to choose what tower to spawn")]
        private int index;
        #endregion
        #endregion

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

        private void DisplayTowerStats()
        {
            #region Old Code
            //_assualtTowerCost.text = string.Format("$" + prefab[0].Cost);
            //_assualtName.text = string.Format("" + prefab[0].TowerName);

            //_sniperTowerCost.text = string.Format("$" + prefab[1].Cost);
            //_sniperName.text = string.Format("" + prefab[1].TowerName);

            //_flameTowerCost.text = string.Format("$" + prefab[2].Cost);
            //_flameTowerName.text = string.Format("" + prefab[2].TowerName);

            // used to set the tower with its name and cost
            #endregion

            // will display the stats of the tower in a list
            foreach (TowerStats stat in towerStats)
            {
                stat.Display();
            }
        }

        public void SelectTower(int tower)
        {
            index = tower;
            towerDescription.text = prefab[tower].Description;
            towerName.text = prefab[tower].TowerName;
        }

        private void Update()
        {
            DisplayTowerStats();
        }
    }

}
