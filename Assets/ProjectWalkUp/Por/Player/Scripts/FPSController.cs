using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class FPSController : MonoBehaviour
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

    [Header("Look Sensitivity Settings")]
    [SerializeField]
    private float mouseSenti = 2.0f;

    [SerializeField]
    private float upDownRange = 80.0f;

    private CharacterController charController;
    private Camera mainCamera;
    private PlayerInputHandler inputHandler;

    private Vector3 currentMovement;

    private float verticalRot;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        inputHandler = PlayerInputHandler.instance;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        //Walk and Sprint
        float speed = walkSpeed * (inputHandler.sprintValue > 0 ? sprintMulti : 1f);

        Vector3 inputDir = new Vector3(inputHandler.moveInput.x, 0f, inputHandler.moveInput.y);
        Vector3 worldDir = transform.TransformDirection(inputDir);
        worldDir.Normalize();

        currentMovement.x = worldDir.x * speed;
        currentMovement.z = worldDir.z * speed;
        
        //Jump
        HandleJumping();

        charController.Move(currentMovement * Time.deltaTime);
    }

    void HandleJumping()
    {
        if (charController.isGrounded) 
            { currentMovement.y = -0.5f; if (inputHandler.jumpTriggered) { currentMovement.y = jumpForce; } }
        else
            currentMovement.y -=  gravity * Time.deltaTime;
    }

    void HandleRotation()
    {
        float mouseXRot = inputHandler.lookInput.x * mouseSenti;
        transform.Rotate(0, mouseXRot, 0);

        verticalRot -= inputHandler.lookInput.y * mouseSenti;
        verticalRot = Mathf.Clamp(verticalRot, -upDownRange, upDownRange);

        mainCamera.transform.localRotation = Quaternion.Euler(verticalRot, 0, 0);
    }
}
