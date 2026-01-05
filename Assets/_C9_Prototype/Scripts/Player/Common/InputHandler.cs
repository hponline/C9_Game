using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerSkillController skillController;
    WhirlwindSkill whirlwindSkill;

    public Vector2 moveInput;
    public bool jumpPressed;
    public bool primaryAttackPressed;
    public bool skill1Pressed;

    [Header("Skill Action")]
    public Action<int> OnSkillInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        skillController = GetComponent<PlayerSkillController>();
        whirlwindSkill = GetComponent<WhirlwindSkill>();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();

        playerInput.Player.Move.performed += OnMove;
        playerInput.Player.Move.canceled += OnMove;

        playerInput.Player.Jump.performed += OnJump;
        playerInput.Player.PrimaryAttack.performed += OnPrimaryAttack;

        playerInput.Player.SkillWhirlwind.performed += SkillWhirlwind;
        playerInput.Player.SkillLine.performed += SkillLine;
    }

    private void OnDisable()
    {
        playerInput.Player.Move.performed -= OnMove;
        playerInput.Player.Move.canceled -= OnMove;

        playerInput.Player.Jump.performed -= OnJump;
        playerInput.Player.PrimaryAttack.performed -= OnPrimaryAttack;

        playerInput.Player.SkillWhirlwind.performed -= SkillWhirlwind;
        playerInput.Player.SkillLine.performed -= SkillLine;

        playerInput.Player.Disable();
    }

    #region StandartControl
    void OnPrimaryAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            primaryAttackPressed = true;
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            jumpPressed = true;
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public bool ConsumeJump()
    {
        if (jumpPressed)
        {
            jumpPressed = false;
            return true;
        }
        return false;
    }

    public void ConsumeInputs()
    {
        jumpPressed = false;
        primaryAttackPressed = false;
        skill1Pressed = false;
    }

    #endregion

    #region Skills

    void SkillWhirlwind(InputAction.CallbackContext ctx)
    {
        OnSkillInput?.Invoke(0);
    }
    void SkillLine(InputAction.CallbackContext ctx)
    {
        OnSkillInput?.Invoke(1);        
    }


    #endregion
}
