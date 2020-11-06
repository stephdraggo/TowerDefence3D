using System.Collections;
using System.Collections.Generic;
using TowerDefense.Managers;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public class EnemyDeath : UnityEvent<Enemy> {  }

    public float XP { get { return xp; }  }
    public int Money { get { return money; } }

    [Header("GeneralStats")]
    [SerializeField, Tooltip("How fast the enemy can move")]
    private float speed = 0.5f;
    [SerializeField, Tooltip("The amount of health the enemy has")]
    private float health = 1;

    [Header("Rewards")]
    [SerializeField, Tooltip("The amount of xp awarded when the enemy is killed")]
    private float xp;
    [SerializeField, Tooltip("The amount of money awarded when the enemy is killed")]
    private int money;

    [Space]

    [SerializeField]
    private EnemyDeath onDeath = new EnemyDeath();

    public EnemyManager enemy;

    //private Player player;

    public bool Damage(float _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            Die();

            return true;
        }
        return false;
    }

    public void Die()
    {
        onDeath.Invoke(this);

        enemy.KillEnemy(this);
    }

    private void Start()
    {
        // accessing the only player in the game
        //player = Player.instance;
        //onDeath.AddListener(player.Addmoney);

    }

    private void Move()
    {
        transform.Translate(transform.right * -speed);
    }

    private void Update()
    {
        Move();
    }



}
