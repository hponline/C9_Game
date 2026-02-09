using UnityEngine;

public class WhirlwindSkill : SkillBehaviour
{
    // Base deðerler burada durup exp aldýkça base + SO deðerleri yükseltirebilir
    [SerializeField] int maxTarget = 30;
    [SerializeField] ParticleSystem[] shockwawePrefab;
    [SerializeField] PlayerVFX playerWhirlwindTrailVFX;
    Collider[] hitBuffer;

    bool isActive;
    float tickTimer;

    private void Awake()
    {
        hitBuffer = new Collider[maxTarget];
    }

    private void Update()
    {
        if (!isActive) return;

        tickTimer += Time.deltaTime;
        if (tickTimer >= skillData.tickInterval)
        {
            tickTimer -= skillData.tickInterval;
            DealDamage();            
        }
    }

    public override void Execute()
    {       
        StartShockWawe();        
    }

    public override void Stop()
    {        
        StopShockWawe();        
    }

    void DealDamage()
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, skillData.pullRadius, hitBuffer, skillData.hitMask);

        for (int i = 0; i < hits; i++)
        {
            var hit = hitBuffer[i];

            var rb = hit.attachedRigidbody;
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

    void StartShockWawe()
    {
        if (isActive) return;
        isActive = true;

        tickTimer = 0;
        TriggerCameraEffect();
        playerWhirlwindTrailVFX.StartWhirlwindTrail();

        foreach (var shockwawe in shockwawePrefab)
        {
            shockwawe.Play();
        }
    }
    void StopShockWawe()
    {
        if (!isActive) return;
        isActive = false;

        playerWhirlwindTrailVFX.EndWhirlwindTrail();
        foreach (var shockwawe in shockwawePrefab)
        {
            shockwawe.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, skillData.pullRadius);
        Gizmos.DrawWireSphere(transform.position, skillData.damageRadius);
    }
}
