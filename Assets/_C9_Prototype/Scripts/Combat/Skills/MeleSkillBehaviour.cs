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
            if (hit.TryGetComponent<IDamageable>(out var target))
            {
                if (target.Transform.CompareTag("Enemy"))
                {
                    var ctx = new DamageContext
                    {
                        amount = skillData.damage,
                        hitPoint = hit.ClosestPoint(origin),
                        hitNormal = Vector3.up
                    };
                    target.TakeDamage(ctx);
                    // damagePopup                     
                }
            }
        }

        //if (skillData.skillPrefab != null)
        //{
        //    // skillData.SkillPrefab üzerinden Vfx/Sound/Spawn tetikleme yeri
        //}
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
