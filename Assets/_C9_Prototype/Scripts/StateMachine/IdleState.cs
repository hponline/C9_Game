using UnityEngine;

public class IdleState : IState
{
    PlayerStateMachine owner;

    public IdleState(PlayerStateMachine owner)
    {
        this.owner = owner;
    }

    public void EnterState()
    {
        if (owner.animator != null)        
            owner.animator.SetBool("IsRun", false);
        
    }
    public void UpdateState()
    {
        PlayerMovement.Instance.SetMoveInput(Vector2.zero);

        if (owner.InputHandler.primaryAttackPressed)
        {
            owner.ChangeState(owner.basicAttackState);
            return;
        }

        if (owner.InputHandler.moveInput.sqrMagnitude > 0.01f)
            owner.ChangeState(owner.runState);
    }

    public void ExitState() { }
}
