using UnityEngine;

public class BasicAttackState : IState
{
    PlayerStateMachine owner;
    bool started = false;

    public BasicAttackState(PlayerStateMachine owner)
    {
        this.owner = owner;
    }

    public void EnterState()
    {
        started = true;
        owner.playerMovement.SetCanMove(false);

        var attackSource = owner.GetComponent<IAttackSource>();
        owner.playerSkillController.UseBasicAttack(attackSource);
        //owner.animator.SetTrigger(owner.playerSkillController.skillDataSO.animationTriggerName);
    }
    public void UpdateState()
    {
        if (!started) return;
        owner.ChangeState(owner.idleState);
    }

    public void ExitState()
    {
        started = false;
        owner.playerMovement.SetCanMove(true);
    }

}
