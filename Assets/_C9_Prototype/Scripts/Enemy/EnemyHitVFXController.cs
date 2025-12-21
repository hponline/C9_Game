using UnityEngine;

public class EnemyHitVFXController : MonoBehaviour
{
    [SerializeField] ParticleSystem hitVfx;
    EnemyHealth enemyHealth;


    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void PlayHitVFX(DamageContext ctx)
    {
        Instantiate(hitVfx, ctx.hitPoint, Quaternion.LookRotation(ctx.hitNormal));
    }

    void PlayDeathVFX()
    {
        Debug.Log("Dead vfx");
    }

    private void OnEnable()
    {
        enemyHealth.OnDamaged += PlayHitVFX; 
        enemyHealth.OnDied += PlayDeathVFX; 
    }

    private void OnDisable()
    {
        enemyHealth.OnDamaged -= PlayHitVFX;
        enemyHealth.OnDied -= PlayDeathVFX;
    }
}
