using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool jumpTriggered { get; private set; }
    public float sprintValue { get; private set; }

    public static PlayerInputHandler instance { get; private set; }

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

    [Header ("Deadzone Values")]
    [SerializeField]
    private float leftStickDZValue;

    private InputAction moveAct;
    private InputAction lookAct;
    private InputAction jumpAct;
    private InputAction sprintAct;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
    }

    private void OnEnable()
    {
        moveAct.Enable();
        lookAct.Enable();
        jumpAct.Enable();
        sprintAct.Enable();
    }

    private void OnDisable()
    {
        moveAct.Disable();
        lookAct.Disable();
        jumpAct.Disable();
        sprintAct.Disable();
    }
}
