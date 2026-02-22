using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public PlayerSkillController skillController;
    public PlayerStateMachine stateMachine;

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

    #region BasicAttack
    public void SlashEffect()
    {
        skillController.SlashEffect();
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
