using UnityEngine;

public class SkillState : IState
{
    PlayerStateMachine owner;
    float timer;
    int index;

    public SkillState(PlayerStateMachine owner)
    {
        this.owner = owner;
    }

    public void EnterState()
    {
        Debug.Log($"SkillState baþladý: ");

        index = owner.pendingSkillIndex;
        var data = owner.playerSkillController.GetSkillDataSO(index);

        timer = data.animDuration;
        owner.playerMovement.SetCanMove(false);

        //owner.playerSkillController.UseSkillSlot(index, owner.source)
       
    }
    public void UpdateState()
    {
        Debug.Log("SkillState");
    }

    public void ExitState()
    {
        Debug.Log($"SkillState bitti: ");
    }
}
