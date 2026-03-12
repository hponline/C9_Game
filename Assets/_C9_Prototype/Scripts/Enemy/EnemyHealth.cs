using System;
using UnityEngine;

[RequireComponent(typeof(EnemyRunTimeStats))]
public class EnemyHealth : MonoBehaviour, IDamageable, IPoolable
{
    EnemyRunTimeStats enemyRunTimeStats;

    [SerializeField] EnemyConfigSO enemyConfigSO;
    public bool IsAlive => enemyRunTimeStats.CurrentHealth > 0f;

    public event Action<EnemyHealth> OnDied;
    public event Action<DamageContext> OnDamaged;
    public static event Action<int> OnExpGain;

    HealthBarUI healthBarUI;

    private void Awake()
    {
        enemyRunTimeStats = GetComponent<EnemyRunTimeStats>();
    }

    void Initialized()
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
        DamageEvents.OnDamagePopup?.Invoke(ctx);
        OnDamaged?.Invoke(ctx);

        if (!IsAlive)
            Die();
    }

    void Die()
    {
        OnDied?.Invoke(this);
        OnExpGain?.Invoke(enemyConfigSO.expReward);
        healthBarUI.UnBind();
    }

    public void OnSpawn()
    {
        Initialized();
    }

    public void OnDespawn()
    {
        EnemyHealthBarPool.instance.Release(healthBarUI);
        healthBarUI = null;
    }
}
