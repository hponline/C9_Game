using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Chase,
    Attack
}
public class EnemyMelee : Enemy
{
    [Header("References")]
    [SerializeField] EnemyState curretState;
    [SerializeField] SkillBehaviour meleeSkill;
    [SerializeField] Transform target;
    [SerializeField] PlayerHealth playerHealth;

    [Header("Patrol")]
    [SerializeField] float chaseRange = 15f;

    [Header("Attack")]
    [SerializeField] float attackRange = 3f;
    [SerializeField] LayerMask layerMask;
    public bool isAttacking;
    public bool canDealDamage;

    float finalAttackSpeed;
    float attackSpeedMultiplier;
    float baseAttackSpeed = 1f;
    float attackCooldown;
    float attackTimer;


    [Header("Death")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSound;
    [SerializeField] float destroyDelay = 1.5f;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        playerHealth = target.GetComponent<PlayerHealth>();

        attackCooldown = 1f / baseAttackSpeed;

        //Debug.Log("EnemyRuntimeStats scriptini baðla");
    }

    private void Update()
    {
        if (target == null) return;
        if (!health.IsAlive) return;

        if (!playerHealth.IsAlive)
        {
            curretState = EnemyState.Idle;
        }

        float distance = Vector3.Distance(transform.position, target.position);
        switch (curretState)
        {
            case EnemyState.Idle:
                IdleState(distance);
                break;

            case EnemyState.Chase:
                ChaseState(distance);
                break;

            case EnemyState.Attack:
                AttackState(distance);
                break;
        }
    }
    void EnemyMoveToPlayer()
    {
        Vector3 moveDir = (target.position - transform.position).normalized;
        transform.position += moveDir * enemyConfigSO.baseMoveSpeed * Time.deltaTime;
        moveDir.y = 0;
        if (moveDir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveDir);
    }

    #region Attack
    public void DoAttack()
    {
        if (target == null) return;

        var hits = Physics.OverlapSphere(attackOrigin.transform.position, attackRange, layerMask);
        foreach (var hit in hits)
        {
            var damageable = hit.GetComponent<IDamageable>();
            if (damageable == null) continue;

            Vector3 hitPoint = hit.transform.position;
            Vector3 hitNormal = transform.forward;
            var ctx = DamageCalculator.EnemyCalculate(enemyConfigSO, runTimeStats, hitPoint, hitNormal);
            damageable.TakeDamage(ctx);
        }
    }

    void HandleDamaged(DamageContext ctx)
    {
        if (isAttacking && !canDealDamage)
        {
            CancelAttack();
        }
    }

    void CancelAttack()
    {
        isAttacking = false;
        canDealDamage = false;

        animator.ResetTrigger(GameTags.EnemyAnimationTags.ENEMY_ATTACK_TAG);
        animator.SetTrigger(GameTags.EnemyAnimationTags.ENEMY_GETHIT_TAG);
    }

    public void AttackFinished()
    {
        isAttacking = false;
        canDealDamage = false;
    }

    #endregion

    #region Death
    protected override void HandleDeath(EnemyHealth health)
    {
        if (deathVFX != null)
        {
            Debug.Log("ölüm vfx tetikle");
            //Instantiate(deathVFX, transform.position, Quaternion.identity);
        }

        if (deathSound != null)
        {
            Debug.Log("ölüm sound tetikle");
            //AudioSource.PlayClipAtPoint(deathSound, transform.position); // Geçici olarak o yerde 3d ses ekler ve siler, -- AudioManager a geç
        }

        OnDeath();
    }

    void OnDeath()
    {
        animator.SetTrigger(GameTags.EnemyAnimationTags.ENEMY_DEATH_TAG);
        Despawn();
        DisableEnemy();
    }

    void Despawn()
    {
        StartCoroutine(DespawnCoroutine());
    }

    IEnumerator DespawnCoroutine()
    {
        yield return new WaitForSeconds(destroyDelay);
        gameObject.SetActive(false);
    }

    void DisableEnemy()
    {
        var collider = GetComponent<Collider>();
        if (collider) collider.enabled = false;

        var rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;
    }

    #endregion

    #region StateMachine
    void IdleState(float distance)
    {
        if (distance <= chaseRange)
        {
            curretState = EnemyState.Chase;
        }
        animator.SetBool(GameTags.EnemyAnimationTags.ENEMY_RUN_TAG, false);
        animator.SetBool(GameTags.EnemyAnimationTags.ENEMY_IDLE_TAG, true);
    }

    void ChaseState(float distance)
    {
        if (distance > chaseRange)
        {
            curretState = EnemyState.Idle;
            return;
        }

        if (distance <= attackRange)
        {
            curretState = EnemyState.Attack;
            return;
        }

        EnemyMoveToPlayer();
        animator.SetBool(GameTags.EnemyAnimationTags.ENEMY_RUN_TAG, true);
    }

    void AttackState(float distance)
    {
        if (target == null) curretState = EnemyState.Idle;
        if (distance > attackRange)
        {
            curretState = EnemyState.Chase;
            return;
        }
        animator.SetBool(GameTags.EnemyAnimationTags.ENEMY_RUN_TAG, false);
        animator.SetTrigger(GameTags.EnemyAnimationTags.ENEMY_ATTACK_TAG);
    }

    #endregion


    #region AttackSpeed
    public void AttackSpeedBuff(float multiplier)
    {
        attackSpeedMultiplier *= multiplier;
        RecalculateAttackSpeed();
    }

    void RecalculateAttackSpeed()
    {
        finalAttackSpeed = baseAttackSpeed * attackSpeedMultiplier;
        attackCooldown = 1f / finalAttackSpeed;
        attackTimer = Mathf.Min(attackTimer, attackCooldown);
    }
    #endregion


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnEnable()
    {
        health.OnDamaged += HandleDamaged;
    }

    private void OnDisable()
    {
        health.OnDamaged -= HandleDamaged;
    }

}
