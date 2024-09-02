using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speed Settings")]
    [SerializeField]
    private float walkSpeed = 5.0f;

    [SerializeField]
    private float sprintMulti = 2.0f;

    [Header("Jump Settings")]
    [SerializeField]
    private float jumpForce = 5.0f;

    [SerializeField]
    private float gravity = 9.81f;

    private CharacterController charController;
    private Vector3 currentMovement;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    public void HandleMovement(PlayerInputHandler input)
    {
        //Walk and Sprint
        float speed = walkSpeed * (input.sprintValue > 0 ? sprintMulti : 1f);

        Vector3 inputDir = new Vector3(input.moveInput.x, 0f, input.moveInput.y);
        Vector3 worldDir = transform.TransformDirection(inputDir);
        worldDir.Normalize();

        currentMovement.x = worldDir.x * speed;
        currentMovement.z = worldDir.z * speed;

        charController.Move(currentMovement * Time.deltaTime);
    }

    public void HandleJumping(PlayerInputHandler input)
    {
        if (charController.isGrounded)
        {
            currentMovement.y = -0.5f;
            if (input.jumpAct.WasPressedThisFrame())
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }
}
