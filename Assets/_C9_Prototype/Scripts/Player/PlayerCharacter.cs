using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerRunTimeStats))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerSkillController))]
public class PlayerCharacter : MonoBehaviour
{
    public Transform AttackOrigin => attackOrigin;

    [SerializeField] Transform attackOrigin;
    [SerializeField] PlayerSkillController skillController;

    InputHandler playerInput;

    private void Awake()
    {
        playerInput = GetComponent<InputHandler>();
    }

    //private void Update()
    //{
    //    if (playerInput.primaryAttackPressed)
    //    {
    //        skillController.UseBasicAttack(this);
    //    }

    //    if (playerInput.skill1Pressed)
    //    {            
    //        skillController.UseSkillSlot(0, this);
    //    }
    //    playerInput.ConsumeInputs();
    //}
}
