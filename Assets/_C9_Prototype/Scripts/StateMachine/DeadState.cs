public class DeadState : IState
{
    PlayerStateMachine stateMachine;

    public DeadState (PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void EnterState()
    {
        stateMachine.animator.SetTrigger("IsDead");
        stateMachine.playerMovement.SetCanMove(false);
    }

    public void ExitState()
    {
        // Respawn?
    }

    public void UpdateState()
    {

    }
}
