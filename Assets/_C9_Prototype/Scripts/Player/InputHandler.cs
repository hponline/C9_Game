using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    PlayerInput playerInput;

    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public bool jumpPressed;


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
    }


    private void OnDisable()
    {
        playerInput.Player.Move.performed -= OnMove;
        playerInput.Player.Move.canceled -= OnMove;
        playerInput.Player.Jump.performed -= OnJump;

        playerInput.Player.Disable();
    }
    

    void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            jumpPressed = true;
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
}
