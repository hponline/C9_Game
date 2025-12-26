using UnityEngine;

public class EnemyOnAnimations : MonoBehaviour
{
    [SerializeField] EnemyMelee enemyMelee;

    public void OnAnimationHit()
    {
        enemyMelee?.DoAttack();
    }

    public void OnAttackFinished()
    {
        enemyMelee?.AttackFinished();
    }
}
