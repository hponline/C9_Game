using System;
using UnityEngine;

[RequireComponent(typeof(EnemyRunTimeStats))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    EnemyRunTimeStats enemyRunTimeStats;
    [SerializeField] int expReward = 10;

    public bool IsAlive => enemyRunTimeStats.CurrentHealth > 0f;

    public event Action OnDied;
    public event Action<DamageContext> OnDamaged;
    public static event Action<int> OnExpGain;

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
        DamageEvents.OnDamagePopup?.Invoke(ctx);
        OnDamaged?.Invoke(ctx);

        if (!IsAlive)
            Die();
    }

    void Die()
    {
        OnExpGain?.Invoke(expReward);
        OnDied?.Invoke();
        healthBarUI.UnBind();
        EnemyHealthBarPool.instance.Release(healthBarUI);
    }
}
