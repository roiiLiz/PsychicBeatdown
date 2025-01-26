using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Input Asset")]
    [SerializeField] InputReader input;
    [Header("Components")]
    [SerializeField] MovementComponent movement;
    [SerializeField] ManaManager mana;

    Vector2 movementDirection;

    
    void OnEnable()
    {
        input.MoveEvent += OnMove;
        input.FireEvent += OnFire;
    }

    void OnDisable()
    {
        input.MoveEvent -= OnMove;
        input.FireEvent -= OnFire;
    }

    void OnMove(Vector2 direction)
    {
        movementDirection = direction;
    }

    void OnFire()
    {
        mana.SpendMana(25);
    }

     void Update()
    {
        HandleMovement();
        // HandleFire();
    }

    void HandleMovement()
    {
        // movementDirection = input.moveInput;
        movement.MoveTowards(movementDirection, gameObject);
    }

    // void HandleFire()
    // {
    //     if (input.fireTriggered)
    //     {
    //         mana.SpendMana(25);
    //     }
    // }
}
