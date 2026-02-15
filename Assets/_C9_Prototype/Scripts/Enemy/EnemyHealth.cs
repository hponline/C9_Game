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

    HealthBarUI healthBarUI;

    private void Awake()
    {
        enemyRunTimeStats = GetComponent<EnemyRunTimeStats>();
    }

    private void Start()
    {
        healthBarUI = EnemyHealthBarPool.instance.Get();
        healthBarUI.Bind(transform);
        healthBarUI.SetValue(1f);
        healthBarUI.Hide();
    }

    public void TakeDamage(DamageContext ctx)
    {
        if (!IsAlive) return;

        enemyRunTimeStats.TakeDamage(ctx.amount);

        if (!healthBarUI.IsVisible)
            healthBarUI.Show();

        healthBarUI.SetValue(enemyRunTimeStats.CurrentHealth / enemyRunTimeStats.MaxHealth);
        DamageEvents.OnDamageDealt?.Invoke(ctx.amount, transform);
        OnDamaged?.Invoke(ctx);

        if (!IsAlive)
            Die();
    }

    void Die()
    {
        OnDied?.Invoke();
        healthBarUI.UnBind();
        EnemyHealthBarPool.instance.Release(healthBarUI);
    }
}
