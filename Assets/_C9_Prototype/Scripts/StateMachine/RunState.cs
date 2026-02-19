public class RunState : IState
{
    PlayerStateMachine owner;

    public RunState(PlayerStateMachine owner)
    {
        this.owner = owner;
    }

    public void EnterState() 
    { 
        if (owner.animator != null)
            owner.animator.SetBool(GameTags.PlayerAnimationTags.PLAYER_RUN_TAG, true);
    }   
    
    public void UpdateState()
    {
        PlayerMovement.Instance.SetMoveInput(owner.InputHandler.moveInput);

        if (owner.InputHandler.primaryAttackPressed)
        {
            owner.ChangeState(owner.basicAttackState);
            return;
        }

        if (owner.InputHandler.moveInput.sqrMagnitude < 0.01f)
            owner.ChangeState(owner.idleState);
    }

    public void ExitState()
    {
        if (owner.animator != null)        
            owner.animator.SetBool(GameTags.PlayerAnimationTags.PLAYER_RUN_TAG, false);        
    }

}
