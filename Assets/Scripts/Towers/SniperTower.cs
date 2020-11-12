using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Managers;
using TowerDefence.Towers;
using TowerDefence.Utilities;
using UnityEngine;

public class SniperTower : BaseTower
{
    [SerializeField]
    private Transform turret;
    [SerializeField]
    private LineRenderer bullet;
    [SerializeField]
    private Transform barrel;

    public EnemyManager enemy;

    [SerializeField]
    private float lineTime;
    private float maxLineTime = 0.2f;

    private float targettingTime;
    private float maxTargettingTime = 3.5f;

    private void Awake()
    {
        enemy = FindObjectOfType<EnemyManager>();   
    }

    protected void Start()
    {
        targettingTime = 2;
        bullet.positionCount = 0;
    }

    protected override void Update()
    {
        base.Update();
        if (TargetedEnemy != null) //if there is an enemy being targeted
        {
            AimAndFire();
        }

        if (isFiring == false)
        {
            bullet.positionCount = 0;
        }
    }

    protected override void RenderAttackVisuals()
    {
        RenderBullet(barrel);
        lineTime = 0;
    }

    protected override void Target()
    {   
        if(enemy.aliveEnemies.Count == 0)
        {
            return;
        }
        if (targettingTime < maxTargettingTime)
        {
            targettingTime += Time.deltaTime;
        }
        else if (targettingTime >= maxTargettingTime)
        {
            target = enemy.aliveEnemies[enemy.aliveEnemies.Count - 1];
            targettingTime = 0;
        }
    }

    private void AimAndFire()
    {
        // gets the distance and direction of the target
        MathUtils.DistanceAndDirection(out float _distance, out Vector3 direction, turret, TargetedEnemy.transform);
        // rotates the turret to look at the direction of the target
        turret.rotation = Quaternion.LookRotation(direction);
    }
    private void RenderBullet(Transform start)
    {
        bullet.positionCount = 2;
        bullet.SetPosition(0, start.position);
        bullet.SetPosition(1, target.transform.position);
    }
}
