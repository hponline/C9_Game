public class BasicAttackState : IState
{
    PlayerStateMachine owner;
    public BasicAttackState(PlayerStateMachine owner)
    {
        this.owner = owner;
    }

    public void EnterState()
    {
        owner.attackFinished = true;
        PlayerMovement.Instance.SetCanMove(false);
        owner.playerSkillController.UseBasicAttack();
    }
    public void UpdateState()
    {
        if (!owner.attackFinished) return;

        if (owner.InputHandler.moveInput.sqrMagnitude > 0.01f)
            owner.ChangeState(owner.runState);
        else
            owner.ChangeState(owner.idleState);
    }

    public void ExitState() { }
}
