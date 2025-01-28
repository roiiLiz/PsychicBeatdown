using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Input Asset")]
    [SerializeField] InputReader input;
    [Header("Components")]
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] ManaManager manaComponent;
    [SerializeField] ThrowScript throwComponent;
    [SerializeField] Transform pivot;

    Vector2 movementDirection;
    Vector2 mousePos;
    
    void OnEnable()
    {
        input.MoveEvent += OnMove;
        input.LookEvent += OnLook;
        input.FireEvent += OnFire;
    }

    void OnDisable()
    {
        input.MoveEvent -= OnMove;
        input.LookEvent -= OnLook;
        input.FireEvent -= OnFire;
    }

    void OnMove(Vector2 direction) => movementDirection = direction;
    void OnLook(Vector2 mousePosition) => mousePos = mousePosition;

    void OnFire()
    {
        if (!PauseManager.isPaused && manaComponent.CanAffordSpell(25))
        {
            manaComponent.SpendMana(25);
            // throwComponent.Handle
        }
    }

    void Update()
    {
        HandleMovement();
        RotatePivot();
    }

    void HandleMovement()
    {
        // movementDirection = input.moveInput;
        movementComponent.MoveTowards(movementDirection, gameObject);
    }

    void RotatePivot()
    {
        Vector3 lookDirection = Camera.main.ScreenToWorldPoint(mousePos);

        lookDirection = (lookDirection - transform.position).normalized;
        
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        pivot.transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);
    }

    // void HandleFire()
    // {
    //     if (input.fireTriggered)
    //     {
    //         mana.SpendMana(25);
    //     }
    // }
}
