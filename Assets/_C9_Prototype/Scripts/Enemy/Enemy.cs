using UnityEngine;

[RequireComponent(typeof(EnemyRunTimeStats))]
[RequireComponent(typeof(EnemyHealth))]
public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy References")]
    [SerializeField] protected EnemyConfigSO enemyConfigSO;
    [SerializeField] protected Transform attackOrigin;
    [SerializeField] protected Animator animator;
    [SerializeField] protected EnemyRunTimeStats runTimeStats;
    [SerializeField] protected EnemyHealth enemyHealth;
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected Transform target;
    [SerializeField] protected PlayerHealth playerHealth;

    protected virtual void Awake()
    {
        runTimeStats = GetComponent<EnemyRunTimeStats>();
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();

        target = GameObject.FindWithTag("Player").transform;
        playerHealth = target.GetComponent<PlayerHealth>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        runTimeStats.Init(enemyConfigSO, gameManager.globalLevel);
    }

    protected abstract void HandleDeath(EnemyHealth health);
}
