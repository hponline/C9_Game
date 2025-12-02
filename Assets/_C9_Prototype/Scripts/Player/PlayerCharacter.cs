using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(RunTimeStats))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerSkillController))]
public class PlayerCharacter : MonoBehaviour, IAttackSource
{
    public Transform AttackOrigin => attackOrigin;

    [SerializeField] Transform attackOrigin;
    [SerializeField] PlayerSkillController skillController;

    InputHandler playerInput;
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerInput = GetComponent<InputHandler>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        playerMovement.PlayerMove();
        //playerMovement.PlayerMove(playerInput.moveInput); ??

        if (playerInput.primaryAttackPressed)
        {
            skillController.UseBasicAttack(this);
        }

        if (playerInput.skill1Pressed)
        {
            skillController.UseSkillSlot(0, this);
        }
        playerInput.ConsumeInputs();
    }
}
