using UnityEngine;

public class EnemyOnAnimations : MonoBehaviour
{
    [SerializeField] EnemyMelee enemyMelee;

    public void StartAttack()
    {
        enemyMelee.isAttacking = true;
        enemyMelee.canDealDamage = false;
    }

    public void OnAnimationHit()
    {
        if (!enemyMelee.isAttacking) return;
        enemyMelee.canDealDamage = true;

        enemyMelee?.DoAttack();
    }

    public void OnAttackFinished()
    {
        enemyMelee?.AttackFinished();
    }
}
