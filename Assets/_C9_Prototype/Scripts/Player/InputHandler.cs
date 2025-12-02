using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    PlayerInput playerInput;

    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public bool jumpPressed;
    [HideInInspector] public bool primaryAttackPressed;
    [HideInInspector] public bool skill1Pressed;


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
        Debug.Log("Skill basýldý");
    }
    void OnPrimaryAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            primaryAttackPressed = true;
        Debug.Log("basic attack yapýldý");
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

    public bool ConsumeJump() // ground check
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
