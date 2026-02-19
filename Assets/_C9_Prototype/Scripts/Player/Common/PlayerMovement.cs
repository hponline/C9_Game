using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InputHandler))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    [Header("References")]
    [SerializeField] PlayerRunTimeStats playerRunTimeStats;
    [SerializeField] InputHandler inputHandler;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform playerRoot;

    [Header("Player Variables")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float playerRotationSpeed = 10f;
    [SerializeField] float groundCheckRaycast = .5f;
    [SerializeField] bool canMove = true;
    [SerializeField] bool requestJump = false;

    Rigidbody rb;
    Vector2 currentMoveInput = Vector2.zero;

    private void Awake()
    {
        Instance = this;

        rb = GetComponentInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
        PlayerJump();
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        currentMoveInput = moveInput;
    }


    public void SetCanMove(bool value)
    {
        canMove = value;
        if (!canMove)
        {
            currentMoveInput = Vector2.zero;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void PlayerMove()
    {
        if (!canMove) return;
        
        Vector3 moveDirection = GetMoveDirection();

        if (moveDirection.sqrMagnitude > 0.001f)
        {
            rb.MovePosition(rb.position + moveDirection * playerRunTimeStats.MoveSpeed * Time.fixedDeltaTime);
            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, toRotation, playerRotationSpeed * Time.fixedDeltaTime));
        }
    }

    Vector3 GetMoveDirection()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * currentMoveInput.y + right * currentMoveInput.x;
        return moveDir;
    }

    #region Jump
    public void RequestJump()
    {
        requestJump = true;
    }
    void PlayerJump()
    {
        if (!requestJump) return;
        if (IsGrounded())
        {
            Debug.Log("Karkter zýpladý");
            rb.AddForce(Vector3.up * playerRunTimeStats.JumpForce, ForceMode.Impulse);
        }
        requestJump = false;
    }

    public bool IsGrounded()
    {
        Vector3 origin = playerRoot.position + Vector3.up * 0.1f;
        float maxDistance = groundCheckRaycast + 0.1f;
        return Physics.Raycast(origin, Vector3.down, maxDistance, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(playerRoot.position, Vector3.down * groundCheckRaycast);
    }
    #endregion
}
