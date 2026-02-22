using System;
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
    [SerializeField] protected EnemyHealth health;
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected Transform target;
    [SerializeField] protected PlayerHealth playerHealth;

    protected virtual void Awake()
    {
        runTimeStats = GetComponent<EnemyRunTimeStats>();
        health = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();

        target = GameObject.FindWithTag("Player").transform;
        playerHealth = target.GetComponent<PlayerHealth>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        health.OnDied += OnEnemyDead;

        runTimeStats.Init(enemyConfigSO, gameManager.currentLevel);

        /* Level Scale iþlemleri
          float maxHealth = config.baseHealth + 
                          config.healthPerLevel * (level - 1);

        float damage = config.baseDamage + 
                       config.damagePerLevel * (level - 1);

        runTimeStats.Init(maxHealth);
        runTimeStats.SetDamage(damage);
         */
    }



    void OnEnemyDead()
    {
        HandleDeath(health);
    }

    protected abstract void HandleDeath(EnemyHealth health);
}
