using UnityEngine;

public class MeleSkillBehaviour : SkillBehaviour
{
    [SerializeField] float radius = 2f;
    [SerializeField] LayerMask hitMask;

    public override void Execute(IAttackSource source)
    {
        Vector3 origin = source.AttackOrigin.position;
        Collider[] hits = Physics.OverlapSphere(origin, radius, hitMask);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out var dmg))
            {
                if (dmg.Transform.CompareTag("Enemy"))
                {
                    var ctx = new DamageContext
                    {
                        amount = Data.damage,
                        hitPoint = hit.ClosestPoint(origin),
                        hitNormal = Vector3.up
                    };
                    dmg.TakeDamage(ctx);
                }

            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
