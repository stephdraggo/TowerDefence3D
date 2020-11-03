using System.Collections;
using System.Collections.Generic;
using TowerDefense.Towers;
using TowerDefense.Utilities;
using UnityEngine;

public class AssualtTower : BaseTower
{
    [SerializeField]
    private Transform turret;
    [SerializeField]
    private LineRenderer bullet;
    [SerializeField]
    private Transform barrel;


    [SerializeField]
    private float fire;

    private void AimAndFire()
    {
        // gets the distance and direction of the target
        MathUtils.DistanceAndDirection(out float _distance, out Vector3 direction, turret, TargetedEnemy.transform);
        // rotates the turret to look at the direction of the target
        turret.rotation = Quaternion.LookRotation(direction);
    }

   
    protected void Start()
    {
        bullet.positionCount = 0;
    }

    protected override void Update()
    {
        base.Update();

        if (TargetedEnemy != null) //if there is an enemy being targeted
        {
            AimAndFire();
        }
    }

    private void RenderBullet(Transform start)
    {
        bullet.positionCount = 2;
        bullet.SetPosition(0, start.position);
        bullet.SetPosition(1, TargetedEnemy.transform.position);
    }
}
