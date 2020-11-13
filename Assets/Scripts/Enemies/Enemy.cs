using TowerDefence.Managers;
using TowerDefence.Mechanics.Enemies;
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
    protected float currentHealth = 1;
    [SerializeField, Tooltip("The amount of health the enemy has \n" +
    "Set this one instead")]
    protected float maxHealth;
    [SerializeField, Tooltip("How much damage the enemy will do when it reaches the end goal")]
    protected float damage = 10;
    #endregion

    [Header("rangeBool")]
    [SerializeField]
    public bool inRange;

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
    public EnemyPathfinding enemyPath;
    [SerializeField]
    private UnityEngine.UI.Image healthBarEnemy;
    [SerializeField]
    private Canvas _heatlhbar;
    #endregion

    public bool Damage(float _damage)
    {
        currentHealth -= _damage;
        if (currentHealth <= 0)
        {
            Die();
            player.AddMoney(Money);
            return true;
        }
        return false;
    }

    public void EnemySetHealth(float _health)
    {
        healthBarEnemy.fillAmount = Mathf.Clamp01(_health / maxHealth);
    }

    public void Die()
    {
        onDeath.Invoke(this);
        enemy.KillEnemy(this);
    }

    public void AttackPlayer()
    {
        player.Damage(damage);
    }

    private void Start()
    {
        // accessing the only player in the game
        //player = Player.instance;
        //onDeath.AddListener(player.Addmoney);
        currentHealth = maxHealth;

        inRange = false;

        enemyPath = gameObject.GetComponent<EnemyPathfinding>();
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<EnemyManager>();

        _heatlhbar.worldCamera = Camera.main;
    }

    private void Update()
    {
        if (inRange == true)
        {
            Die();
            player.Damage(damage);
        }

       // _heatlhbar.transform.LookAt(Camera.main.transform);

        enemyPath.EnemyHasReachedGoal();
        EnemySetHealth(currentHealth);
    }

}
