using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    PlayerInput playerInput;

    [Header("Player Deðerleri")]
    [SerializeField] float zoomSpeed = 2f;
    [SerializeField] float zoomLerpSpeed = 10f;
    [SerializeField] float minDistance = 3f;
    [SerializeField] float maxDistance = 15f;

    CinemachineCamera cam;
    CinemachineOrbitalFollow orbitalCam;
    Vector2 scrollDelta;

    float targetZoom;
    float currentZoom;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        cam = GetComponent<CinemachineCamera>();
        orbitalCam = GetComponent<CinemachineOrbitalFollow>();
        targetZoom = currentZoom = orbitalCam.Radius;
    }

    private void Update()
    {
        if (scrollDelta.y != 0)
        {
            targetZoom = Mathf.Clamp(orbitalCam.Radius - scrollDelta.y * zoomSpeed, minDistance, maxDistance);
            scrollDelta = Vector2.zero;
        }

        currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomLerpSpeed);
        orbitalCam.Radius = currentZoom;
    }

    private void HandleMouseScroll(InputAction.CallbackContext ctx)
    {
        scrollDelta = ctx.ReadValue<Vector2>();
    }
    private void OnEnable()
    {
        playerInput.CameraControls.Enable();

        playerInput.CameraControls.MouseZoom.performed += HandleMouseScroll;
    }
}
