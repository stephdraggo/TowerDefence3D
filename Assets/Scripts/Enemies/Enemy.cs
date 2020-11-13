using TowerDefence.Managers;
using TowerDefence.notPlayer;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public class EnemyDeath : UnityEvent<Enemy> {  }
    #region Properties
    public float XP { get { return xp; }  }
    public float Money { get { return money; } }
    public float Speed { get => speed; }
    #endregion

    #region Variables
    #region GeneralStats
    [Header("GeneralStats")]
    [SerializeField, Tooltip("How fast the enemy can move")]
    protected float speed = 0.5f;
    [SerializeField, Tooltip("The amount of health the enemy has")]
    protected float health = 1;
    [SerializeField, Tooltip("How much damage the enemy will do when it reaches the end goal")]
    protected float damage = 10;
    #endregion

    [Header("Rewards")]
    [SerializeField, Tooltip("The amount of xp awarded when the enemy is killed")]
    protected float xp;
    [SerializeField, Tooltip("The amount of money awarded when the enemy is killed")]
    protected float money;

    [Space]

    [SerializeField]
    private EnemyDeath onDeath = new EnemyDeath();
    public EnemyManager enemy;
    public Player player;
    #endregion

    public bool Damage(float _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            Die();
            player.PlayerMoneyReward(Money);
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

        player = FindObjectOfType<Player>();
    }



}
