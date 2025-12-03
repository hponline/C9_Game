using UnityEngine;

[RequireComponent (typeof(InputHandler))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    [Header("References")]
    [SerializeField] InputHandler inputHandler;
    [SerializeField] Transform cameraTransform;
    [SerializeField] CharacterStatSO characterStatSO;

    [Header("Player Variables")]
    [SerializeField] float playerRotationSpeed = 10f;

    Rigidbody rb;

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

    public void PlayerMove()
    {
        Vector3 moveDirection = GetMoveDirection();

        if (moveDirection.sqrMagnitude > 0.001f)
        {
            rb.MovePosition(rb.position + moveDirection * characterStatSO.moveSpeed * Time.fixedDeltaTime);

            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation (Quaternion.Slerp(rb.rotation, toRotation, playerRotationSpeed * Time.fixedDeltaTime));
            Debug.Log("Karakter gidiyo");
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

        Vector3 moveDir = forward * inputHandler.moveInput.y + right * inputHandler.moveInput.x;
        return moveDir;
    }

    void PlayerJump()
    {
        if (inputHandler.ConsumeJump())
        {
            rb.AddForce(Vector3.up * characterStatSO.jumpForce, ForceMode.Impulse);
        }
    }
}
