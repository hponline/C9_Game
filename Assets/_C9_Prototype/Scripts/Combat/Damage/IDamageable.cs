public interface IDamageable
{
    bool IsAlive { get; }
    void TakeDamage(DamageContext ctx);
}
