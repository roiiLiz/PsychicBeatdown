using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Knight : Enemy
{
    [SerializeField] MeleeAttackerComponent meleeAttackerComponent;

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

        if (meleeAttackerComponent.isInRange)
        {
            ChangeState(EnemyState.ATTACKING);
        }
    }

    void HandleAttackState()
    {
        if (!meleeAttackerComponent.isInRange)
        {
            ChangeState(EnemyState.WALKING);
        }
    }

    void HandleHeldState()
    {
        sprite.transform.Rotate(0f, 0f, thrownSpinRate * heldSpinMultiplier * Time.deltaTime);
        meleeAttackerComponent.enabled = false;
        animator.enabled = false;
    }
}
