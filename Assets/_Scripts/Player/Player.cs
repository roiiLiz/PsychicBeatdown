using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] MovementComponent movement;
    [SerializeField] ManaManager mana;

    Vector2 movementDirection;

    private void Update()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        movement.MoveTowards(movementDirection, gameObject);

        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }
}
