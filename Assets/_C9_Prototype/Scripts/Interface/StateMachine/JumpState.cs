using UnityEngine;

public class JumpState : IState
{
    PlayerStateMachine owner;

    public JumpState(PlayerStateMachine owner)
    {
        this.owner = owner;
    }

    public void EnterState()
    {
        if (owner.InputHandler.ConsumeJump())
        {
            owner.playerMovement.RequestJump();
        }
        owner.playerMovement.SetCanMove(false);
    }

    public void UpdateState()
    {
        Debug.Log("JumpState");
    }

    public void ExitState()
    {
        owner.playerMovement.SetCanMove(true);
    }
}
