using UnityEngine;

public class SkillState : IState
{
    PlayerStateMachine owner;
    bool isStarted = false;

    public SkillState(PlayerStateMachine owner)
    {
        this.owner = owner;
    }

    public void EnterState()
    {
        if (!isStarted)
        {
            isStarted = true;
            Debug.Log($"SkillState baþladý: ");
        }
    }
    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public void ExitState()
    {
        Debug.Log($"SkillState bitti: ");
        isStarted = false; 
    }
}
