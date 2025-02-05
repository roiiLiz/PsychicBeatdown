using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Input/Input Reader", fileName = "New Input Reader")]
public class InputReader : ScriptableObject
{
    [Header("Input")]
    [SerializeField] InputActionAsset inputAsset;

    // [Header("Action Map")]
    // [SerializeField] string actionMapName = "Player";

    [Header("Action Names")]
    [SerializeField] string move = "Move";
    [SerializeField] string look = "Look";
    [SerializeField] string fire = "Fire";
    [SerializeField] string pause = "Pause";

    InputAction moveAction;
    InputAction lookAction;
    InputAction fireAction;
    InputAction pauseAction;

    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction<Vector2> LookEvent;
    public event UnityAction FireEvent;
    public event UnityAction PauseEvent;

    void OnEnable()
    {
        moveAction = inputAsset.FindAction(move);
        lookAction = inputAsset.FindAction(look);
        fireAction = inputAsset.FindAction(fire);
        pauseAction = inputAsset.FindAction(pause);

        moveAction.started += OnMove;
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        lookAction.started += OnLook;
        lookAction.performed += OnLook;
        lookAction.canceled += OnLook;

        fireAction.started += OnFire;
        fireAction.performed += OnFire;
        fireAction.canceled += OnFire;

        pauseAction.started += OnPause;
        pauseAction.performed += OnPause;
        pauseAction.canceled += OnPause;

        moveAction.Enable();
        lookAction.Enable();
        fireAction.Enable();
        pauseAction.Enable();
    }

    void OnDisable()
    {
        moveAction.started -= OnMove;
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;

        lookAction.started -= OnLook;
        lookAction.performed -= OnLook;
        lookAction.canceled -= OnLook;

        fireAction.started -= OnFire;
        fireAction.performed -= OnFire;
        fireAction.canceled -= OnFire;

        pauseAction.started -= OnPause;
        pauseAction.performed -= OnPause;
        pauseAction.canceled -= OnPause;

        moveAction.Disable();
        lookAction.Disable();
        fireAction.Disable();
        pauseAction.Disable();
    }

    void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        if (FireEvent != null && context.started)
        {
            FireEvent?.Invoke();
        }
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (PauseEvent != null && context.started)
        {
            PauseEvent?.Invoke();
        }
    }

    // public void ChangeInputType(bool navigationOnly)
    // {
    //     if (navigationOnly)
    //     {
    //         inputAsset.FindActionMap("Player").Disable();
    //         inputAsset.FindActionMap("UI").Enable();
    //     } else
    //     {
    //         inputAsset.FindActionMap("Player").Enable();
    //         inputAsset.FindActionMap("UI").Disable();
    //     }
    // }
}