using System.Collections;
using System.Collections.Generic;
using TowerDefence.Managers;
using TowerDefence.Utilities;
using UnityEngine;

namespace TowerDefence.Towers
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

        //[SerializeField]
        //protected float MaxRange
        //{
        //    get
        //    {
        //        return maxRange * (rangeUpgrade); 
        //    }
        //    set 
        //    {
        //        maxRange = value;
        //    }
        //}

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
        public string towerName = "";
        [SerializeField, TextArea]
        public string description = "";
        [SerializeField, Range(1, 200)]
        public int cost = 1;
        #endregion

        #region Attack
        [Header("Tower Attack Stats")]
        [SerializeField, Min(0.01f)]
        public float damage = 1;
        [SerializeField, Min(0.1f)]
        protected float minRange = 1;
        [SerializeField]
        public float maxRange = 2;
        [SerializeField]
        public float rangeUpgrade = 1;
        [SerializeField, Min(0.1f)]
        public float fireRate = 2;
        #endregion

        [SerializeField]
        protected bool isFiring = false;

        [SerializeField]
        protected Enemy target = null;
        protected float currentTime = 0;
        #endregion

        #region AbstractMethods
        protected abstract void RenderAttackVisuals();
        //protected abstract void RenderLevelUpVisuals();

        #endregion

        #region TowerAttackMethods
        protected virtual void Fire()
        {
            if (target != null)
            {
                target.Damage(damage);
                RenderAttackVisuals();
            }
        }

        protected virtual void FireWhenReady()
        {
            isFiring = false;
            if (target != null)
            {
                if(currentTime < fireRate)
                {
                    currentTime += Time.deltaTime;
                    
                }
                else
                {
                    isFiring = true;
                    currentTime = 0;
                    Fire();
                }
            }
        }
        #endregion

        #region EnemyTargettingMethods
        protected virtual void Target()
        {
            // get enemies within range
            Enemy[] closeEnemies = EnemyManager.instance.GetClosestEnemies(transform, maxRange, minRange);

            // sets the target as the closest enemy
            target = GetClosestEnemy(closeEnemies);
            if (target != null)
            {
                if (Vector3.Distance(target.transform.position, transform.position) > maxRange)
                {
                    target = null;
                }
            }
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

            if (_enemies != null) //if there are enemies in range
            {
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
            }
            return closest;
        }

        #endregion



        public void SetGlobalScale(Transform transform2, Vector3 globalScale)
        {
            transform2.localScale = Vector3.one;
            transform2.localScale = new Vector3(globalScale.x / transform2.lossyScale.x, globalScale.y / transform2.lossyScale.y, globalScale.z / transform2.lossyScale.z);
        }

        protected virtual void Update()
        {
            FireWhenReady();
            Target();
        }
    }
}                          
                           
                           