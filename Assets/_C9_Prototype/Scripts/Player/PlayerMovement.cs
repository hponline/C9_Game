using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    [Header("References")]
    [SerializeField] InputHandler inputHandler;
    [SerializeField] Transform cameraTransform;

    [Header("Player Deðerleri")]
    [SerializeField] float playerBaseSpeed = 3f;
    [SerializeField] float playerJumpForce = 3f;
    [SerializeField] float playerRotationSpeed = 10f;

    Rigidbody rb;

    private void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
        PlayerJump();
    }

    void PlayerMove()
    {
        Vector3 moveDirection = GetMoveDirection();

        if (moveDirection.sqrMagnitude > 0.001f)
        {
            rb.MovePosition(rb.position + moveDirection * playerBaseSpeed * Time.fixedDeltaTime);

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, playerRotationSpeed * Time.deltaTime);
        }
    }

    public Vector3 GetMoveDirection()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * inputHandler.moveInput.y + right * inputHandler.moveInput.x;
        return moveDir;
    }

    void PlayerJump()
    {
        if (inputHandler.ConsumeJump())
        {
            rb.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
        }
    }
}
