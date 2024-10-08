using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateCamera : MonoBehaviour
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

    [Header("Look Sensitivity Settings")]
    [SerializeField]
    private float mouseSen = 2.0f;

    private float cinemacineTargetPitch;
    private float rotVelocity;
    private float moveCam = 0.01f;

    public void HandleRotation(PlayerInputHandler input)
    {
        // If there is an input
        if (input.lookInput.sqrMagnitude >= moveCam)
        {
            float deltaTimeMultiplier = 1f;
            float mouseYInput = invertYAxis ? -input.lookInput.y : input.lookInput.y;

            cinemacineTargetPitch -= mouseYInput * mouseSen * deltaTimeMultiplier;
            rotVelocity = input.lookInput.x * mouseSen * deltaTimeMultiplier;

            cinemacineTargetPitch = Mathf.Clamp(cinemacineTargetPitch, -buttonClamp, topClamp);

            cinemacineTarget.transform.localRotation = Quaternion.Euler(cinemacineTargetPitch, 0.0f, 0.0f);

            // Rotate the player left and right
            transform.Rotate(Vector3.up * rotVelocity);
        }
    }
}
