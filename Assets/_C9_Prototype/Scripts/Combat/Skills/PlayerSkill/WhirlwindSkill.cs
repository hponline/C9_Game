using UnityEngine;

public class WhirlwindSkill : SkillBehaviour
{
    // Base deðerler burada durup exp aldýkça base + SO deðerleri yükseltirebilir
    public ParticleSystem shockwawePrefab;
    bool isActive;
    float tickTimer;

    private void Update()
    {
        if (!isActive) return;

        tickTimer += Time.deltaTime;
        if (tickTimer >= skillData.tickInterval)
        {
            DealDamage();
            ShockwaweSpawn();
            tickTimer = 0;
        }
    }

    public override void Execute()
    {
        tickTimer = 0;
        isActive = true;
    }

    public override void Stop()
    {
        isActive = false;
    }

    void DealDamage()
    {
        var hits = Physics.OverlapSphere(transform.position, skillData.pullRadius, skillData.hitMask);

        foreach (var hit in hits)
        {
            var rb = hit.GetComponent<Rigidbody>();
            if (rb)
            {
                var dir = (transform.position - hit.transform.position).normalized;
                rb.AddForce(dir * skillData.pullForce, ForceMode.Impulse);
            }

            if (!hit.TryGetComponent<IDamageable>(out var damageable)) continue;
            float distance = Vector3.Distance(hit.transform.position, transform.position);
            if (distance <= skillData.damageRadius)
            {
                damageable.TakeDamage(new DamageContext
                {
                    amount = skillData.damage,
                    hitPoint = hit.transform.position,
                    hitNormal = (hit.transform.position - transform.position).normalized
                });
            }
        }
    }

    public void ShockwaweSpawn()
    {
        Instantiate(shockwawePrefab, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, skillData.pullRadius);
        Gizmos.DrawWireSphere(transform.position, skillData.damageRadius);
    }
}
