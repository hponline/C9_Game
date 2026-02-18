using UnityEngine;

public class MeleSkillBehaviour : SkillBehaviour
{
    [SerializeField] float radius = 2f;
    [SerializeField] int maxTarget = 10;
    Vector3 gizmosRadius;
    Collider[] hitBuffer;

    private void Start()
    {
        hitBuffer = new Collider[maxTarget];
    }
    public override void Execute()
    {
        if (hitBuffer == null) return;

        int hits = Physics.OverlapSphereNonAlloc(transform.position, radius, hitBuffer, skillData.hitMask);
        for (int i = 0; i < hits; i++)
        {
            var hit = hitBuffer[i];
            var rb = hit.attachedRigidbody;
            if (rb != null)
            {
                var dir = (hit.transform.position - transform.position).normalized;
                rb.AddForce(dir * 4f, ForceMode.Impulse);
            }

            if (!hit.TryGetComponent<IDamageable>(out var target)) continue;

            Vector3 hitPoint = hit.ClosestPoint(transform.position);
            Vector3 hitNormal = (hit.transform.position - transform.position).normalized;
            var ctx = DamageCalculator.Calculate(skillData, playerRunTimeStats, hitPoint, hitNormal);
            target.TakeDamage(ctx);
        }
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
