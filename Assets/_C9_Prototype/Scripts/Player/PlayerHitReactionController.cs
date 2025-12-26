using UnityEngine;

public class PlayerHitReactionController : MonoBehaviour
{
    [SerializeField] GameObject getHitVfx;

    Animator animator;
    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        playerHealth.OnDamaged += OnDamaged;
        playerHealth.OnDied += OnDeath;
    }
    private void OnDisable()
    {
        playerHealth.OnDamaged -= OnDamaged;
        playerHealth.OnDied -= OnDeath;
    }

    void OnDamaged(DamageContext ctx)
    {
        animator.SetTrigger(GameTags.EnemyAnimationTags.ENEMY_GETHIT_TAG);

        if (getHitVfx != null)
        {
            Instantiate(getHitVfx, ctx.hitPoint, Quaternion.identity);
        }
    }

    void OnDeath()
    {
        animator.SetTrigger("Die oyun bitti");
        Time.timeScale = 0;
    }
}
