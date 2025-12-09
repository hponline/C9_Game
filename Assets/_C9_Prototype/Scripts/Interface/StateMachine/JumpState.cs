
using UnityEngine;

public class JumpState : IState
{
    PlayerStateMachine owner;
    bool jumped = false;

    public JumpState(PlayerStateMachine owner)
    {
        this.owner = owner;
    }

    public void EnterState()
    {
        if (!jumped)
        {
            if (owner.InputHandler != null && owner.InputHandler.ConsumeJump())
            {
                owner.InputHandler.ConsumeJump();
                owner.playerMovement.PlayerJump();
                jumped = true;
                if (owner.animator != null)
                {
                    owner.animator.SetTrigger("IsJump");
                }
            }
        }
    }
    public void UpdateState()
    {
        Debug.Log("JumpState");
    }

    public void ExitState()
    {
        jumped = false;
    }

}
