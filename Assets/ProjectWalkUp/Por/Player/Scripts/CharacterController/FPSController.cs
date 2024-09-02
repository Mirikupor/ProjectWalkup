using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class FPSController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField]
    private GameObject cinemacineTarget;

    [SerializeField]
    private bool invertYAxis = false;

    [SerializeField]
    private float topClamp = 89f;

    [SerializeField]
    private float buttonClamp = 89f;

    [Header ("Look Sensitivity Settings")]
    [SerializeField]
    private float mouseSen = 2.0f;

    [Header("Interact Settings")]
    [SerializeField]
    private float interactDist = 3.0f;

    [SerializeField]
    private LayerMask layMask;

    //Test SOLID
    private PlayerMovement playerMovement;

    private Camera mainCamera;
    private PlayerInputHandler inputHandler;
    private PlayerUI playerUI;

    private float cinemacineTargetPitch;
    private float rotVelocity;
    private float moveCam = 0.01f;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerUI = GetComponent<PlayerUI>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        inputHandler = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        playerMovement.HandleMovement(inputHandler);
        playerMovement.HandleJumping(inputHandler);

        HandleInteract();
    }

    private void LateUpdate()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        // If there is an input
        if (inputHandler.lookInput.sqrMagnitude >= moveCam)
        {
            float deltaTimeMultiplier = 1f;
            float mouseYInput = invertYAxis ? -inputHandler.lookInput.y : inputHandler.lookInput.y;

            cinemacineTargetPitch -= mouseYInput * mouseSen * deltaTimeMultiplier;
            rotVelocity = inputHandler.lookInput.x * mouseSen * deltaTimeMultiplier;

            cinemacineTargetPitch = Mathf.Clamp(cinemacineTargetPitch, -buttonClamp, topClamp);

            cinemacineTarget.transform.localRotation = Quaternion.Euler(cinemacineTargetPitch, 0.0f, 0.0f);

            // Rotate the player left and right
            transform.Rotate(Vector3.up * rotVelocity);
        }
    }

    private void HandleInteract()
    {
        playerUI.UpdateText(string.Empty);

        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * interactDist);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, interactDist, layMask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.gameObject.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.propmtMessage);

                if (inputHandler.interactAct.WasPressedThisFrame())
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
