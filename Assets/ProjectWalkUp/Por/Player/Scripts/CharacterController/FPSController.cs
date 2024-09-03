using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerRotateCamera), typeof(PlayerInteract))]
public class FPSController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerRotateCamera playerRotCamera;
    private PlayerInteract playerInteract;

    private PlayerInputHandler inputHandler;
    private PlayerUI playerUI;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerRotCamera = GetComponent<PlayerRotateCamera>();
        playerInteract = GetComponent<PlayerInteract>();
        playerUI = GetComponent<PlayerUI>();
    }

    private void Start()
    {
        inputHandler = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        playerMovement.HandleMovement(inputHandler);
        playerMovement.HandleJumping(inputHandler);
        playerInteract.HandleInteract(inputHandler, playerUI);
    }

    private void LateUpdate()
    {
        playerRotCamera.HandleRotation(inputHandler);
    }
}
