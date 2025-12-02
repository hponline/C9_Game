using UnityEngine;

public interface IDamageable
{
    bool IsAlive { get; }
    Transform Transform { get; }
    void TakeDamage(DamageContext ctx);
}
