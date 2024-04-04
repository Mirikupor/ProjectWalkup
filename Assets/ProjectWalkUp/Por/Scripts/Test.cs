using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class Test : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera playerCam;

    [SerializeField]
    private UnityEvent eventPlayerCam;

    [SerializeField]
    public CinemachineVirtualCamera[] cameras;

    private CinemachineVirtualCamera currentCam;
    private PlayerInputHandler inputHandler;

    private void Start()
    {
        inputHandler = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        HandleSwitchMainCharacter();
    }

    public void SwitchCamera(CinemachineVirtualCamera cam)
    {
        currentCam = cam;
        ChangeCameraPriority();
    }

    private void HandleSwitchMainCharacter()
    {
        if (currentCam == playerCam) return;

        if (inputHandler.interactAct.WasPressedThisFrame())
        {
            currentCam = playerCam;
            ChangeCameraPriority();
            eventPlayerCam.Invoke();
        }
    }

    private void ChangeCameraPriority()
    {
        currentCam.Priority = 20;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != currentCam)
            {
                cameras[i].Priority = 10;
            }
        }
    }
}
