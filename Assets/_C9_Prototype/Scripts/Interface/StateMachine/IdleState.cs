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
        Debug.Log($"Enter Idle");
        if (owner.animator != null)
        {
            owner.animator.SetBool("IsRun", false);
        }

    }
    public void UpdateState()
    {
        Debug.Log($"IdleState");        
    }

    public void ExitState()
    {
        Debug.Log($"Exit Idle");
    }
}
