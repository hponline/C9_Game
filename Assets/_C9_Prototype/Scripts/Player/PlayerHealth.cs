using System;
using UnityEngine;

[RequireComponent (typeof(PlayerRunTimeStats))]
public class PlayerHealth : MonoBehaviour, IDamageable
{
    public event Action<DamageContext> OnDamaged;
    public event Action<float, float> OnHealthChanged;
    public event Action OnDied;

    PlayerRunTimeStats playerRunTimeStats;

    [Header("HealthBar")]
    public bool IsAlive => playerRunTimeStats.CurrentHealth > 0f;
    public float CurrentHealth => playerRunTimeStats.CurrentHealth;
    public float MaxHealth => playerRunTimeStats.MaxHealth;


    private void Awake()
    {
        playerRunTimeStats = GetComponent<PlayerRunTimeStats>();
    }

    public void TakeDamage(DamageContext ctx)
    {
        if (!IsAlive) return;

        playerRunTimeStats.TakeDamage(ctx.amount);

        OnDamaged?.Invoke(ctx);
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (!IsAlive)
            Die();
    }

    void Die()
    {
        OnDied?.Invoke();
        Debug.Log("Player öldü");        
    }

    void UpdateHealthUI()
    {
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    private void OnEnable()
    {
        playerRunTimeStats.OnStatsChanged += UpdateHealthUI;
    }
    private void OnDisable()
    {
        playerRunTimeStats.OnStatsChanged -= UpdateHealthUI;
    }
}
