using UnityEngine;

public enum EnemyState
{
    Idle,
    Chase,
    Attack
}
public class EnemyMelee : Enemy
{
    public EnemyState curretState;
    public EnemyStatSO enemyStatSO;

    [SerializeField] float chaseRange = 15f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] SkillBehaviour meleeSkill;
    [SerializeField] Transform target;

    [Header("Attack")]
    [SerializeField] float attackRange = 3f;
    float finalAttackSpeed;
    float attackSpeedMultiplier;
    float baseAttackSpeed = 1f;
    float attackCooldown;
    float attackTimer;      


    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        attackCooldown = 1f / baseAttackSpeed;
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

    void EnemyInitialize()
    {
        baseAttackSpeed = enemyStatSO.finalAttackSpeed;
    }

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
            DoAttack();
            attackTimer = attackCooldown;
        }
    }

    void DoAttack()
    {
        Debug.Log("Attack");
        transform.position += new Vector3(0,.5f,0); // örnek gösterim
        // Damage at 
        // Anim Tetikle
    }

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

    void EnemyMoveToPlayer()
    {
        Vector3 moveDir = (target.position - transform.position).normalized;
        transform.position += moveDir * enemyStatSO.moveSpeed * Time.deltaTime;
        moveDir.y = 0;
        if (moveDir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveDir);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    protected override void HandleDeath(Health health)
    {
        // Animasyon, loot, event, pool vs.
        Debug.Log($"{name}: öldü");
        throw new System.NotImplementedException();
    }
}
