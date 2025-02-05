using System;
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
                HandleWalkState();
                break;
            case EnemyState.ATTACKING:
                HandleAttackState();
                break;
            case EnemyState.HELD:
                HandleHeldState();
                break;
            default:
                break;
        }
        
    }

    void HandleWalkState()
    {
        movementComponent.MoveTowards(new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), gameObject);
        animator.Play("PlayerWalk");

        //
    }

    void HandleAttackState()
    {
        //
    }

    void HandleHeldState()
    {
        sprite.transform.Rotate(0f, 0f, thrownSpinRate * heldSpinMultiplier * Time.deltaTime);
        animator.enabled = false;
    }
}
