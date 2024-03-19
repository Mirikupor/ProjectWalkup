using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool jumpTriggered { get; private set; }
    public bool interactTriggered { get; private set; }
    public float sprintValue { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    [Header ("Input Action Asset")]
    [SerializeField]
    private InputActionAsset playerControls;

    [Header ("Map Name Reference")]
    [SerializeField]
    private string actionMapName = "Player";

    [Header ("Action Name Reference")]
    [SerializeField]
    private string move = "Move";

    [SerializeField]
    private string look = "Look";

    [SerializeField]
    private string jump = "Jump";

    [SerializeField]
    private string sprint = "Sprint";

    [SerializeField]
    private string interact = "Interact";

    [Header ("Deadzone Values")]
    [SerializeField]
    private float leftStickDZValue;

    private InputAction moveAct;
    private InputAction lookAct;
    private InputAction jumpAct;
    private InputAction sprintAct;
    private InputAction interactAct;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        moveAct = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAct = playerControls.FindActionMap(actionMapName).FindAction(look);
        jumpAct = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAct = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        interactAct = playerControls.FindActionMap(actionMapName).FindAction(interact);

        RegisterInputActions();

        InputSystem.settings.defaultDeadzoneMin = leftStickDZValue;
    }

    private void RegisterInputActions()
    {
        moveAct.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAct.canceled += context => moveInput = Vector2.zero;

        lookAct.performed += context => lookInput = context.ReadValue<Vector2>();
        lookAct.canceled += context => lookInput = Vector2.zero;

        jumpAct.performed += context => jumpTriggered = true;
        jumpAct.canceled += context => jumpTriggered = false;

        sprintAct.performed += context => sprintValue = context.ReadValue<float>();
        sprintAct.canceled += context => sprintValue = 0f;

        interactAct.performed += context => interactTriggered = true;
        interactAct.canceled += context => interactTriggered = false;
    }

    private void OnEnable()
    {
        moveAct.Enable();
        lookAct.Enable();
        jumpAct.Enable();
        sprintAct.Enable();
        interactAct.Enable();

        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        moveAct.Disable();
        lookAct.Disable();
        jumpAct.Disable();
        sprintAct.Disable();
        interactAct.Disable();

        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected: Debug.Log("Device Disconneted" + device.name);
                                                 break;

            case InputDeviceChange.Reconnected: Debug.Log("Device Connected" + device.name);
                                                break;
        }
    }
}
