using System.Collections;
using System.Collections.Generic;
using TowerDefense.Towers;
using UnityEngine;

public class Flammer : BaseTower
{
    [SerializeField]
    public List<Enemy> targets = new List<Enemy>();

    protected override void RenderAttackVisuals()
    {
        // flame visuals
    }

    protected override void Fire()
    {
        base.Fire();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            Debug.Log("enemy");
            targets.Add(other.gameObject.GetComponent<Enemy>());
        }   
    }
}
