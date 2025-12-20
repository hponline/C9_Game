using UnityEngine;

[RequireComponent (typeof(PlayerRunTimeStats))]
public class PlayerHealth : MonoBehaviour, IDamageable
{
    PlayerRunTimeStats playerRunTimeStats;
    public bool IsAlive => playerRunTimeStats.CurrentHealth > 0f;
    public Transform Transform => transform;

    //public Action<Health> OnDied;

    private void Awake()
    {
        playerRunTimeStats = GetComponent<PlayerRunTimeStats>();
    }

    public void TakeDamage(DamageContext ctx)
    {
        if (!IsAlive) return;

        playerRunTimeStats.TakeDamage(ctx.amount);

    }
}
