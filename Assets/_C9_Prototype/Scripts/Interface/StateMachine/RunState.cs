using UnityEngine;

public class RunState : IState
{
    PlayerStateMachine owner;

    public RunState(PlayerStateMachine owner)
    {
        this.owner = owner;
    }

    public void EnterState()
    {
       // Debug.Log("Enter RunState");
    }

    public void UpdateState()
    {
        if (owner.animator != null)
        {
            owner.animator.SetBool("IsRun", true);
            owner.playerMovement.SetMoveInput(owner.InputHandler.moveInput);
        }
    }

    public void ExitState()
    {
        if (owner.animator != null)
        {
            owner.animator.SetBool("IsRun", false);
        }
        //Debug.Log("Exit RunState");
    }

}
