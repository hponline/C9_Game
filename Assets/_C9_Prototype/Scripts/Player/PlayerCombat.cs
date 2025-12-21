using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] float attackRadius = 2f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackOrigin;

    [Header("Attack Damage")]
    [SerializeField] float baseDamage = 25f;

    private void Start()
    {
        Debug.Log("Attack animasyon Trigger, delay");
    }

    public void DoAttack()
    {
        Collider[] hits = Physics.OverlapSphere(attackOrigin.position, attackRadius, enemyLayer); //Non alloc yapýlabilir
        foreach (Collider hit in hits)
        {
            if (!hit.TryGetComponent<IDamageable>(out var damageable))
                return;

            DamageContext damageContext = new DamageContext
            {
                amount = baseDamage,
                hitPoint = hit.ClosestPoint(attackOrigin.position),
                hitNormal = (hit.transform.position - transform.position).normalized,
                sourceOwner = gameObject
            };
            damageable.TakeDamage(damageContext);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }

}
