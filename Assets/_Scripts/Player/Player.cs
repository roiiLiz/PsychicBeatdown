using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] MovementComponent movement;
    [SerializeField] ManaManager mana;

    PlayerInput input;
    Vector2 movementDirection;

    void Awake()
    {
        input = PlayerInput.instance;
    }

    private void Update()
    {
        HandleMovement();
        HandleFire();
    }

    private void HandleMovement()
    {
        movementDirection = input.moveInput;
        movement.MoveTowards(movementDirection, gameObject);
    }

    private void HandleFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mana.SpendMana(25);
        }
    }
}
