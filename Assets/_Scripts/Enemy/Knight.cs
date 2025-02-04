using UnityEngine;
using UnityEngine.EventSystems;

public class Knight : Enemy
{
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.IDLE:
                break;
            case EnemyState.WALKING:
                movementComponent.MoveTowards(new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), gameObject);
                animator.Play("PlayerWalk");
                break;
            case EnemyState.ATTACKING:
                break;
            case EnemyState.HELD:
                sprite.transform.Rotate(0f, 0f, thrownSpinRate * heldSpinMultiplier * Time.deltaTime);
                animator.enabled = false;
                break;
            case EnemyState.THROWN:
                // transform.Translate(Vector2.right * _thrownSpeed * Time.deltaTime);
                // sprite.transform.Rotate(0f, 0f, thrownSpinRate * Time.deltaTime);
                break;
            default:
                break;
        }
        
    }
}
