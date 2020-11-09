using System.Collections;
using System.Collections.Generic;
using TowerDefense.Towers;
using TowerDefense.Utilities;
using UnityEngine;

public class Flammer : BaseTower
{
    [SerializeField]
    public List<Enemy> targets = new List<Enemy>();


    [SerializeField, Tooltip("Displays the towers max range")]
    private Transform towerMaxRange;
    [SerializeField, Tooltip("Displays the towers min range")]
    private Transform towerMinRange;
    [SerializeField]
    private Transform turret;


    private void AimAndFire()
    {
        // gets the distance and direction of the target
        MathUtils.DistanceAndDirection(out float _distance, out Vector3 direction, turret, TargetedEnemy.transform);
        // rotates the turret to look at the direction of the target
        turret.rotation = Quaternion.LookRotation(direction);
    }

    protected override void RenderAttackVisuals()
    {
        // flame visuals
    }

    protected override void Fire()
    {
        if (targets != null)
        {
            //foreach(Enemy enemy in targets)
            for(int x = targets.Count -1; x >= 0; x--)
            {
                Enemy enemy = targets[x];

                int _index = targets.IndexOf(enemy);
                if (enemy.Damage(damage))
                {                //enemy is dead
                    if (_index != -1)
                    {
                        targets.RemoveAt(_index);
                    }
                }
            }


        }
        else if (targets == null)
        {
            return;
        }
    }

    protected override void FireWhenReady()
    {
        base.FireWhenReady();
        if (targets != null)
        {
            Fire();
        }
        else if (targets == null)
        {
            return;
        }
    }

    /// <summary>
    /// used for displaying the range of the tower
    /// </summary>
    public void DisplayTowerRange()
    {
        SetGlobalScale(towerMaxRange.transform, Vector3.one * MaxRange * 2);
        SetGlobalScale(towerMinRange.transform, Vector3.one * minRange * 2);
    }

    protected override void Update()
    {
        base.Update();
        DisplayTowerRange();
        if (target != null)
        {
            AimAndFire();
        }
        
    }
}
