using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using TowerDefense.Utilities;
using UnityEngine;

namespace TowerDefense.Towers
{
    [System.Serializable]
    public abstract class BaseTower : MonoBehaviour
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

        private float MaxRange
        {
            get
            {
                return maxRange * (rangeUpgrade * 0.5f + 0.5f); 
            }
        }

        protected Enemy TargetedEnemy
        {
            get
            {
                return target;
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
        [SerializeField]
        private float rangeUpgrade = 1;
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

        [SerializeField]
        private Enemy target = null;
        private float currentTime = 0;
        #endregion

        #region AbstractMethods
        //protected abstract void RenderAttackVisuals();
        //protected abstract void RenderLevelUpVisuals();

        #endregion

        #region LevelingMethods
        /// <summary>
        /// Adds xp to the tower when enemies are killed
        /// </summary>
        /// <param name="_enemy">the Enemy to get the xp from</param>
        public void AddExperienceToTower(Enemy _enemy)
        {
            // gets xp from enemy
            xp += _enemy.XP;
            
            // checks if tower is at max level
            // if tower is less then maxlevel
            if (level < maxLevel)
            {
                // if xp is greater or equal to required xp
                if (xp >= RequiredXP)
                {
                    // tower levels up
                    LevelUpTower();
                }
            }
        }
        
        /// <summary>
        /// levels up the tower and resets xp amount
        /// </summary>
        private void LevelUpTower()
        {
            level++;
            xp = 0;

            //RenderLevelUpVisuals();
        }
        #endregion

        #region TowerAttackMethods
        private void Fire()
        {
            if (target != null)
            {
                target.Damage(damage);

                //RenderAttackVisuals();
            }
        }

        private void FireWhenReady()
        {
            if (target != null)
            {
                if(currentTime < fireRate)
                {
                    currentTime += Time.deltaTime;
                }
                else
                {
                    currentTime = 0;   
                    Fire();
                }
            }
        }
        #endregion

        #region EnemyTargettingMethods
        private void Target()
        {
            // get enemies within range
            Enemy[] closeEnemies = EnemyManager.instance.GetClosestEnemies(transform, MaxRange, minRange);

            // sets the target as the closest enemy
            target = GetClosestEnemy(closeEnemies);
        }

        /// <summary>
        /// gets the distance of the closet enemy for the tower to target
        /// </summary>
        /// <param name="_enemies">the array of enemies within range</param>
        /// <returns></returns>
        private Enemy GetClosestEnemy(Enemy[] _enemies)
        {
            float closeDist = float.MaxValue;
            Enemy closest = null;

            foreach (Enemy enemy in _enemies)
            {
                float distToEnemy = Vector3.Distance(enemy.transform.position, transform.position);

                // if the enemy is closer then the current enemy 
                // make the new closest enemy the closest
                if (distToEnemy < closeDist)
                {
                    closeDist = distToEnemy;
                    closest = enemy;
                }
            }
            return closest;
        }

        #endregion

        /// <summary>
        /// used for displaying the range of the tower
        /// </summary>
        public void DisplayTowerRange()
        {
            towerMaxRange.transform.localScale = Vector3.one * MaxRange;
            towerMinRange.transform.localScale = Vector3.one * minRange;
        }

        protected virtual void Update()
        {
            DisplayTowerRange();
            FireWhenReady();
            target();
        }
    }
}                          
                           
                           