using System;
using UnityEngine;

[RequireComponent (typeof(PlayerRunTimeStats))]
public class PlayerHealth : MonoBehaviour, IDamageable
{
    public event Action<DamageContext> OnDamaged;
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

        if (!IsAlive)
            Die();
    }

    void Die()
    {
        Debug.Log("Player öldü");        
    }
}
