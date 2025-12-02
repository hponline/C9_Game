using System;
using UnityEngine;

[RequireComponent (typeof(RunTimeStats))]
public class Health : MonoBehaviour, IDamageable
{
    RunTimeStats stats;
    public bool IsAlive => stats.CurrentHealth > 0f;
    public Transform Transform => transform;

    public Action<Health> OnDied;

    private void Awake()
    {
        stats = GetComponent<RunTimeStats>();
    }

    public void TakeDamage(DamageContext ctx)
    {
        if (!IsAlive) return;

        float finalDamage = Mathf.Max(0, ctx.amount - stats.Defense);
        stats.ModifyHealth(-finalDamage);

        // hit reaction, particles (polish aþamalarý)

        if (!IsAlive)
        {
            OnDied?.Invoke(this);
        }
    }
}
