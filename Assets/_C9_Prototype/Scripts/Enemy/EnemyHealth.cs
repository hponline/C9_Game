using System;
using UnityEngine;

[RequireComponent(typeof(EnemyRunTimeStats))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    EnemyRunTimeStats enemyRunTimeStats;
    public bool IsAlive => enemyRunTimeStats.CurrentHealth > 0f;
    public Transform Transform => transform;

    public event Action OnDied;
    public event Action<DamageContext> OnDamaged;

    private void Awake()
    {
        enemyRunTimeStats = GetComponent<EnemyRunTimeStats>();
    }

    public void TakeDamage(DamageContext ctx)
    {
        if (!IsAlive) return;

        enemyRunTimeStats.TakeDamage(ctx.amount);
        OnDamaged?.Invoke(ctx);

        if (!IsAlive)
            OnDied?.Invoke();
    }
}
