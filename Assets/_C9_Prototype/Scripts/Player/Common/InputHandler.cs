using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    PlayerInput playerInput;

    public Vector2 moveInput;
    public bool jumpPressed;
    public bool primaryAttackPressed;
    public bool skill1Pressed;


    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();

        playerInput.Player.Move.performed += OnMove;
        playerInput.Player.Move.canceled += OnMove;

        playerInput.Player.Jump.performed += OnJump;
        playerInput.Player.PrimaryAttack.performed += OnPrimaryAttack;
        playerInput.Player.Skill1Pressed.performed += OnSkill1;
    }

    private void OnDisable()
    {
        playerInput.Player.Move.performed -= OnMove;
        playerInput.Player.Move.canceled -= OnMove;

        playerInput.Player.Jump.performed -= OnJump;
        playerInput.Player.PrimaryAttack.performed -= OnPrimaryAttack;
        playerInput.Player.Skill1Pressed.performed -= OnSkill1;

        playerInput.Player.Disable();
    }

    void OnSkill1(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            skill1Pressed = true;
    }
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
}
