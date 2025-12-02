using UnityEngine;

public class EnemyMelee : Enemy
{
    [SerializeField] float chaseRange = 15f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] SkillBehaviour meleeSkill;
    [SerializeField] Transform target;
    // Navmesh kullanılabilir

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (target == null) return;
        if (!health.IsAlive) return;

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > chaseRange) return;
        
        if (distance < attackRange)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            Vector2 moveInput = new Vector2(dir.x, dir.y);
            transform.position = moveInput; // navmesh ile yürütebilir
        }
        else
        {
            meleeSkill.Execute(this);
        }
        
    }

    protected override void HandleDeath(Health health)
    {
        // Animasyon, loot, event, pool vs.
        Debug.Log($"{name}: öldü");
        throw new System.NotImplementedException();
    }
}
