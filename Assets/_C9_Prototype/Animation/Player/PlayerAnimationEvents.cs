using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public PlayerSkillController skillController;
    public PlayerStateMachine stateMachine;
    public PlayerVFX playerVFX;

    #region Skill

    public void OnSkillStart()
    {
        skillController.OnSkillStart();
        playerVFX.StartTrail();
    }
    public void OnSkillEnd()
    {
        playerVFX.EndTrail();
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

    public void SlashEffect()
    {
        playerVFX.AttackSlashEffect();
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
