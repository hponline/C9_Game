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
        if (owner.animator != null)
        {
            owner.animator.SetBool("IsRun", true);
        }
    }
    public void UpdateState()
    {
        owner.playerMovement.PlayerMove();
    }

    public void ExitState()
    {
        if (owner.animator != null)
        {
            owner.animator.SetBool("IsRun", false);
        }
    }

}
