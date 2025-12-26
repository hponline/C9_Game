using UnityEngine;

public class MeleSkillBehaviour : SkillBehaviour
{
    [SerializeField] float radius = 2f;
    Vector3 gizmosRadius;

    public override void Execute(IAttackSource source)
    {
        Vector3 origin = source.AttackOrigin.position;
        gizmosRadius = source.AttackOrigin.position;

        Collider[] hits = Physics.OverlapSphere(origin, radius, skillData.hitMask); //NonAlloc yapýlabilir
        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent<IDamageable>(out var target)) continue;
            var ctx = new DamageContext
            {
                amount = skillData.damage,
                hitPoint = hit.ClosestPoint(origin),
                hitNormal = (hit.transform.position - origin).normalized,
                //sourceOwner = source.Owner // ??
            };
            target.TakeDamage(ctx);
            // damagePopup   
            // DamagePopupManager.Instance.Spawn(ctx.hitPoint + vector3.up * 0.5f, ctx.amount);
        }

        //if (skillData.skillPrefab != null)
        //{
        //    // skillData.SkillPrefab üzerinden Vfx/Sound/Spawn tetikleme yeri
        //}
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gizmosRadius, radius);
    }
}
