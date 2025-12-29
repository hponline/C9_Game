using UnityEngine;

public class EnemyHitVFXController : MonoBehaviour
{
    [SerializeField] ParticleSystem getHitVfx;
    EnemyHealth enemyHealth;
    Animator animator;


    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }

    void OnDamaged(DamageContext ctx)
    {
        animator.SetTrigger(GameTags.EnemyAnimationTags.ENEMY_GETHIT_TAG);
        if (getHitVfx != null)
            Instantiate(getHitVfx, ctx.hitPoint + new Vector3(0,1,0), Quaternion.LookRotation(ctx.hitNormal));
    }    

    private void OnEnable()
    {
        enemyHealth.OnDamaged += OnDamaged; 
    }

    private void OnDisable()
    {
        enemyHealth.OnDamaged -= OnDamaged;
    }
}
