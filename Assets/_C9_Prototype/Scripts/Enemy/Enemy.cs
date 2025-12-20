using System;
using UnityEngine;

[RequireComponent(typeof(EnemyRunTimeStats))]
[RequireComponent(typeof(EnemyHealth))]
public abstract class Enemy : MonoBehaviour, IAttackSource
{
    [SerializeField] protected EnemyConfigSO enemyConfigSO;
    [SerializeField] protected Transform attackOrigin;
    public Transform AttackOrigin => attackOrigin;

    public GameObject Owner => throw new NotImplementedException();

    protected EnemyRunTimeStats runTimeStats;
    protected EnemyHealth health;

    //[Header("Level Scaling")]
    //public int level = 1;


    protected virtual void Awake()
    {
        runTimeStats = GetComponent<EnemyRunTimeStats>();
        health = GetComponent<EnemyHealth>();
        //health.OnDied += HandleDeath;

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

    protected abstract void HandleDeath(EnemyHealth health);
}
