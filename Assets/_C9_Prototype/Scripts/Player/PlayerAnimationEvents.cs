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
        PlayerMoveEnd();
    }
    public void OnSkillEnd()
    {
        skillController.OnSkillEnd();
        PlayerMoveStart();
    }

    // Whirlwind atarken hareket edebilmesi için
    public void WhirlwindSkillStart()
    {
        skillController.OnSkillStart();
    }

    #endregion

    #region Basic Attack
    public void OnAnimationHit()
    {
        skillController?.OnAnimationHit();
        PlayerMoveEnd();
    }

    public void OnAttackAnimationEnd()
    {
        stateMachine?.OnAttackAnimationEnd();
        PlayerMoveStart();
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
