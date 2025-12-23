using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
    public PlayerSkillController skillController;
    public PlayerStateMachine stateMachine;

    public void OnAnimationHit()
    {
        skillController?.OnAnimationHit();        
    }

    public void OnAttackAnimationEnd()
    {
        stateMachine?.OnAttackAnimationEnd();        
    }

    public void PlayerMoveStart()
    {
        PlayerMovement.Instance.SetCanMove(true);
    }

    public void PlayerMoveEnd()
    {
        PlayerMovement.Instance.SetCanMove(false);
    }

}
