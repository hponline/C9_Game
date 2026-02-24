using UnityEngine;

public class EnemyOnAnimations : MonoBehaviour
{
    [SerializeField] EnemyMelee enemyMelee;

    public void StartAttack()
    {
        enemyMelee.AttackStart();
    }

    public void OnAnimationHit()
    {
        enemyMelee.DoAttack();
    }

    public void StopAttack()
    {
        enemyMelee.AttackFinish();
    }
}
