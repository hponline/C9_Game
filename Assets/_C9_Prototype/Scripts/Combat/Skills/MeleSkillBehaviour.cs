using UnityEngine;

public class MeleSkillBehaviour : SkillBehaviour
{
    [SerializeField] float radius = 2f;
    Vector3 gizmosRadius;

    public override void Execute()
    {
        //Vector3 origin = source.AttackOrigin.position;
        //gizmosRadius = source.AttackOrigin.position;

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, skillData.hitMask); //NonAlloc yapýlabilir
        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent<IDamageable>(out var target)) continue;
            var ctx = new DamageContext
            {
                amount = skillData.damage,
                hitPoint = hit.ClosestPoint(transform.position),
                hitNormal = (hit.transform.position - transform.position).normalized,
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

    public override void Stop()
    {
        Debug.Log("Basic ATTACK End");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gizmosRadius, radius);
    }
}
