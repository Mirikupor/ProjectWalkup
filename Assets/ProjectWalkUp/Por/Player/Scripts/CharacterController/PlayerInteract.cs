using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interact Settings")]
    [SerializeField]
    private float interactDist = 3.0f;

    [SerializeField]
    private LayerMask layMask;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void HandleInteract(PlayerInputHandler input, PlayerUI playerUI)
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

                if (input.interactAct.WasPressedThisFrame())
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
