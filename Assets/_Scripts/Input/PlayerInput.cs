using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] InputActionAsset playerControls;

    [Header("Action Map")]
    [SerializeField] string actionMapName = "Player";

    [Header("Action Names")]
    [SerializeField] string move = "Move";
    [SerializeField] string look = "Look";
    [SerializeField] string fire = "Fire";
    [SerializeField] string pause = "Pause";
    // [SerializeField] string move = "Move";

    InputAction moveAction;
    InputAction lookAction;
    InputAction fireAction;
    InputAction pauseAction;

    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool fireTriggered { get; private set; }
    public bool pauseTriggered { get; private set; }

    public static PlayerInput instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        fireAction = playerControls.FindActionMap(actionMapName).FindAction(fire);
        pauseAction = playerControls.FindActionMap(actionMapName).FindAction(pause);   

        InitializeActions();
    }

    private void InitializeActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        lookAction.performed += context => lookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => lookInput = Vector2.zero;

        fireAction.performed += context => fireTriggered = true;
        fireAction.canceled += context => fireTriggered = false;

        pauseAction.performed += context => pauseTriggered = true;
        pauseAction.canceled += context => pauseTriggered = false;
    }

    void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        fireAction.Enable();
        pauseAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        fireAction.Disable();
        pauseAction.Disable();
    }
}


