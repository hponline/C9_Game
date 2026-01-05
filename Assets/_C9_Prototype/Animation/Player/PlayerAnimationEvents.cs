using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public PlayerSkillController skillController;
    public PlayerStateMachine stateMachine;

    #region Skill

    public void OnSkillStart()
    {
        skillController.OnSkillStart();
    }
    public void OnSkillEnd()
    {
        skillController.OnSkillEnd();
    }

    #endregion

    #region Basic Attack
    public void OnAnimationHit()
    {
        skillController?.OnAnimationHit();        
    }

    public void OnAttackAnimationEnd()
    {
        stateMachine?.OnAttackAnimationEnd();        
    }

    #endregion

    public void PlayerMoveStart()
    {
        PlayerMovement.Instance.SetCanMove(true);
    }

    public void PlayerMoveEnd()
    {
        PlayerMovement.Instance.SetCanMove(false);
    }

}
