using System.Collections;
using System.Collections.Generic;
using TowerDefence.Towers;
using TowerDefence.Utilities;
using UnityEngine;

public class Flammer : BaseTower
{
    [SerializeField]
    public List<Enemy> targets = new List<Enemy>();
    public int index;


    [SerializeField, Tooltip("Displays the tower's range")]
    private Transform towerMaxRange, towerMinRange;


    [SerializeField]
    private Transform turret;

    private void OnMouseEnter()
    {
        Debug.Log("Display Range");
    }

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

                index = targets.IndexOf(enemy);
                if (enemy.Damage(damage))
                {                //enemy is dead
                    if (index != -1)
                    {
                        targets.RemoveAt(index);
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
