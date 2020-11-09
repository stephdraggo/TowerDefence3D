using System.Collections;
using System.Collections.Generic;
using TowerDefence.Towers;
using TowerDefence.Utilities;
using UnityEngine;

public class AssualtTower : BaseTower
{
    [SerializeField]
    private Transform turret;
    [SerializeField]
    private LineRenderer bullet;
    [SerializeField]
    private Transform barrel;
    [SerializeField, Tooltip("Displays the towers max range")]
    private Transform towerMaxRange;
    [SerializeField, Tooltip("Displays the towers min range")]
    private Transform towerMinRange;

    [SerializeField]
    private float lineTime;
    private float maxLineTime = 0.2f;

    [SerializeField]
    private float fire;

    private void AimAndFire()
    {
        // gets the distance and direction of the target
        MathUtils.DistanceAndDirection(out float _distance, out Vector3 direction, turret, TargetedEnemy.transform);
        // rotates the turret to look at the direction of the target
        turret.rotation = Quaternion.LookRotation(direction);
    }
    protected override void RenderAttackVisuals()
    {
        if (currentTime >= fireRate)
        {
            bullet.positionCount = 0;
        }

        RenderBullet(barrel);
    }


    protected void Start()
    {
        bullet.positionCount = 0;
    }

    protected override void Update()
    {
        base.Update();
        DisplayTowerRange();
        if (TargetedEnemy != null) //if there is an enemy being targeted
        {
            AimAndFire();
        }

        if (isFiring == false)
        {
            bullet.positionCount = 0;
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

    private void RenderBullet(Transform start)
    {
        bullet.positionCount = 2;
        bullet.SetPosition(0, start.position);
        bullet.SetPosition(1, TargetedEnemy.transform.position);
    }
}
