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
    public EnemyState curretState;
    [SerializeField] SkillBehaviour meleeSkill;
    [SerializeField] Transform target;


    [Header("Patrol")]
    [SerializeField] float chaseRange = 15f;
    [SerializeField] float moveSpeed = 5f;

    [Header("Attack")]
    [SerializeField] float attackRange = 3f;
    float finalAttackSpeed;
    float attackSpeedMultiplier;
    float baseAttackSpeed = 1f;
    float attackCooldown;
    float attackTimer;
    // çarpanlara bak EnemyRuntimeStats ile çakýþýyor mu

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
        attackCooldown = 1f / baseAttackSpeed;

        //Debug.Log("EnemyRuntimeStats scriptini baðla");
    }

    private void Update()
    {
        if (target == null) return;
        if (!health.IsAlive) return;

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

    public void DoAttack(IDamageable target)
    {
        if (target == null) return;
        Debug.Log("Attack");

        var ctx = new DamageContext
        {
            amount = runTimeStats.Damage,
            hitPoint = transform.position,
            hitNormal = transform.forward,
            //sourceOwner = this
        };
        target.TakeDamage(ctx);

        transform.position += new Vector3(0,.5f,0); // örnek gösterim
        // Damage at 
        // Anim Tetikle
    }

    void EnemyMoveToPlayer()
    {
        Vector3 moveDir = (target.position - transform.position).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        moveDir.y = 0;
        if (moveDir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveDir);
    }

    #region Death
    protected override void HandleDeath(EnemyHealth health)
    {

        // Animasyon, loot, event, pool vs.
        //lootDropper?.DropLoot(transform.position); //drobu event ile yap baþka scriptte
        Debug.Log($"{name}: öldü");

        if (animator != null)
        {
            Debug.Log($"ölüm animasyonu tetikle");
            //animator.SetTrigger("Die");
        }

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

        DisableEnemy();
        //Destroy(gameObject, destroyDelay);
    }

    void DisableEnemy()
    {
        var collider = GetComponent<Collider>();
        if (collider) collider.enabled = false;

        var rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;
    }

    public void OnDeathAnimationFinished()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #region State
    void IdleState(float distance)
    {
        if (distance <= chaseRange)
        {
            curretState = EnemyState.Chase;
        }
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
    }

    void AttackState(float distance)
    {
        if (distance > attackRange)
        {
            curretState = EnemyState.Chase;
            return;
        }

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {            
            //DoAttack();
            Debug.Log("EnemyAttackState attack");
            attackTimer = attackCooldown;
        }
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

}
