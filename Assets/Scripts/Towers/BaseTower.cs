using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Towers
{
    [System.Serializable]
    public class BaseTower : MonoBehaviour
    {
        #region Properties
        #region Ui Properties
        public string TowerName // The public accessor for the towerName variable. 
        {
            get => towerName;
        }

        public string Description // the public accessor for the description variable.
        {
            get => description;
        }

        public int Cost // the public accessor for the cost variable.
        {
            get => cost;
        }

        /// <summary>
        /// Gets a formmatted string containing al of the description, tower properties <br/>
        /// name and cost to be displayed on the UI.
        /// </summary>
        public string UiDisplayText
        {
            get
            {
                string display = string.Format("Name: {0} Cost: {1}\n", TowerName, Cost.ToString());
                display += Description + "\n";
                display += string.Format("Minimum Range: {0}, Maximum Range: {1}, Damage: {2}", minRange.ToString(), maxRange.ToString(), damage.ToString());
                return display;
            }
        }
        #endregion

        /// <summary>
        /// Calculates the required experience based on the current level<br/>
        /// level and the experience scalar.
        /// </summary>
        private float RequiredXP
        {
            get
            {
                //if the level is equal to 1, return the base xp requirement
                if(level == 1)
                {
                    return requiredXp;
                }

                // Multiply the level by the experienceScalar to get the multiplier
                // for the requireXp
                return requiredXp * (level * xpScale);
            }
        }
        #endregion

        #region Variables
        #region General
        [Header("General Tower Stats")]
        [SerializeField]
        private string towerName = "";
        [SerializeField, TextArea]
        private string description = "";
        [SerializeField, Range(1, 200)]
        private int cost = 1;
        #endregion

        #region Attack
        [Header("Tower Attack Stats")]
        [SerializeField, Min(0.1f)]
        private float damage = 1;
        [SerializeField, Min(0.1f)]
        private float minRange = 1;
        [SerializeField]
        private float maxRange = 2;
        [SerializeField, Min(0.1f)]
        private float fireRate = 1;
        #endregion

        #region Experience
        [Header("Tower Experience Stats")]
        [SerializeField, Range(2, 10)]
        private int maxLevel = 3;
        [SerializeField, Min(1)]
        private float requiredXp = 5;
        [SerializeField, Min(1)]
        private float xpScale = 1;
        #endregion

        [SerializeField, Tooltip("Displays the towers max range")]
        private Transform towerMaxRange;
        [SerializeField, Tooltip("Displays the towers min range")]
        private Transform towerMinRange;
        private int level = 1;
        private float xp = 0;
        #endregion

        private void Update()
        {
            DisplayTowerRange();
        }

        private void DisplayTowerRange()
        {
            towerMaxRange.transform.localScale = Vector3.one * maxRange;
            towerMinRange.transform.localScale = Vector3.one * minRange;
        }
    }
}                          
                           
                           