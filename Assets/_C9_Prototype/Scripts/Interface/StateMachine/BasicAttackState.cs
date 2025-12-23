using UnityEngine;

public class BasicAttackState : IState
{
    PlayerStateMachine owner;

    public BasicAttackState(PlayerStateMachine owner)
    {
        this.owner = owner;
    }

    public void EnterState()
    {
        owner.playerMovement.SetCanMove(false);
        var attackSource = owner.GetComponent<IAttackSource>();
        owner.playerSkillController.UseBasicAttack(attackSource);
    }
    public void UpdateState()
    {
        // State Çýkýþý animasyon yapacak
    }

    public void ExitState()
    {
        owner.playerMovement.SetCanMove(true);
    }
}
