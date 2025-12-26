using System;
using UnityEngine;

[RequireComponent(typeof(EnemyRunTimeStats))]
[RequireComponent(typeof(EnemyHealth))]
public abstract class Enemy : MonoBehaviour, IAttackSource
{
    [Header("References")]
    [SerializeField] protected EnemyConfigSO enemyConfigSO;
    [SerializeField] protected Transform attackOrigin;
    [SerializeField] protected Animator animator;
    protected EnemyRunTimeStats runTimeStats;
    protected EnemyHealth health;

    public GameObject Owner => throw new NotImplementedException();
    public Transform AttackOrigin => attackOrigin;

    //[Header("Level Scaling")]
    //public int level = 1;


    protected virtual void Awake()
    {
        runTimeStats = GetComponent<EnemyRunTimeStats>();
        health = GetComponent<EnemyHealth>();
        animator= GetComponent<Animator>();

        health.OnDied += OnEnemyDead;

        runTimeStats.Init(enemyConfigSO, 1);

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
