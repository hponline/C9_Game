using System;
using UnityEngine;

[RequireComponent (typeof(PlayerRunTimeStats))]
public class PlayerHealth : MonoBehaviour, IDamageable
{
    PlayerRunTimeStats playerRunTimeStats;
    public bool IsAlive => playerRunTimeStats.CurrentHealth > 0f;

    [Header("HealthBar")]
    public float CurrentHealth => playerRunTimeStats.CurrentHealth;
    public float MaxHealth => playerRunTimeStats.MaxHealth;

    public event Action OnDied;
    public event Action<DamageContext> OnDamaged;

    private void Awake()
    {
        playerRunTimeStats = GetComponent<PlayerRunTimeStats>();
    }

    public void TakeDamage(DamageContext ctx)
    {
        if (!IsAlive) return;

        playerRunTimeStats.TakeDamage(ctx.amount);
        OnDamaged?.Invoke(ctx);

        if (!IsAlive)
            Die();
    }

    void Die()
    {
        Debug.Log("Player öldü");
        OnDied?.Invoke();
    }
}
