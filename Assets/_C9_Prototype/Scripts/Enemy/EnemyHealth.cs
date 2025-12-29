using System;
using UnityEngine;

[RequireComponent(typeof(EnemyRunTimeStats))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    EnemyRunTimeStats enemyRunTimeStats;
    EnemyMelee enemyMelee;

    public bool IsAlive => enemyRunTimeStats.CurrentHealth > 0f;
    public Transform Transform => transform;

    public event Action OnDied;
    public event Action<DamageContext> OnDamaged;

    [Header("HealthBar")]
    [HideInInspector] public float CurrentHealth => enemyRunTimeStats.CurrentHealth;
    [HideInInspector] public float MaxHealth => enemyRunTimeStats.MaxHealth;

    private void Awake()
    {
        enemyRunTimeStats = GetComponent<EnemyRunTimeStats>();
        enemyMelee = GetComponent<EnemyMelee>();
    }

    public void TakeDamage(DamageContext ctx)
    {
        if (!IsAlive) return;
        OnTakeDamage();

        enemyRunTimeStats.TakeDamage(ctx.amount);
        OnDamaged?.Invoke(ctx);

        if (!IsAlive)
            Die();
    }

    void OnTakeDamage()
    {
        if (enemyMelee.isAttacking && !enemyMelee.canDealDamage)
        {
            enemyMelee.CancelAttack();
        }
    }

    void Die()
    {
        OnDied?.Invoke();
        //Destroy(gameObject);
    }
}
