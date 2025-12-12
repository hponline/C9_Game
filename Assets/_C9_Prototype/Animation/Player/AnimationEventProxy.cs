using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
    public PlayerSkillController skillController;

    public void OnAnimationHit()
    {
        skillController?.OnAnimationHit();
    }
}
