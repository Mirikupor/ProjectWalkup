using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header ("Movement Speed Settings")]
    [SerializeField]
    private float walkSpeed = 5.0f;

    [SerializeField]
    private float sprintMulti = 2.0f;

    [Header("Camera Settings")]
    [SerializeField]
    private GameObject cinemacineTarget;

    [SerializeField]
    private bool invertYAxis = false;

    [SerializeField]
    private float topClamp = 89f;

    [SerializeField]
    private float buttonClamp = -89f;

    [Header ("Jump Settings")]
    [SerializeField]
    private float jumpForce = 5.0f;

    [SerializeField]
    private float gravity = 9.81f;

    [Header ("Look Sensitivity Settings")]
    [SerializeField]
    private float mouseSen = 2.0f;

    [SerializeField]
    private float upDownRange = 80.0f;

    [Header("Interact Settings")]
    [SerializeField]
    private float interactDist = 3.0f;

    [SerializeField]
    private LayerMask layMask;

    private CharacterController charController;
    private Camera mainCamera;
    private PlayerInputHandler inputHandler;
    private PlayerUI playerUI;

    private Vector3 currentMovement;

    private float cinemacineTargetPitch;
    private float rotVelocity;
    private float moveCam = 0.01f;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerUI = GetComponent<PlayerUI>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        inputHandler = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleInteract();
    }

    private void LateUpdate()
    {
        HandleRotation();
    }

    private void HandleMovement()
    {
        //Walk and Sprint
        float speed = walkSpeed * (inputHandler.sprintValue > 0 ? sprintMulti : 1f);

        Vector3 inputDir = new Vector3 (inputHandler.moveInput.x, 0f, inputHandler.moveInput.y);
        Vector3 worldDir = transform.TransformDirection(inputDir);
        worldDir.Normalize();

        currentMovement.x = worldDir.x * speed;
        currentMovement.z = worldDir.z * speed;

        charController.Move(currentMovement * Time.deltaTime);
    }

    private void HandleJumping()
    {
        if (charController.isGrounded) 
            { currentMovement.y = -0.5f; if (inputHandler.jumpTriggered) { currentMovement.y = jumpForce; } }
        else
            currentMovement.y -=  gravity * Time.deltaTime;
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

            cinemacineTargetPitch = Mathf.Clamp(cinemacineTargetPitch, buttonClamp, topClamp);

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

                if (inputHandler.interactTriggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
