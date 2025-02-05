using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] RangedAttackerComponent rangedAttackerComponent;
    [SerializeField] float movementPenaltyMultiplier = 2f;

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

        void HandleWalkState()
        {
            WalkTowardsPlayer();

            if (rangedAttackerComponent.isInRange)
            {
                movementComponent.movementSpeed /= movementPenaltyMultiplier;
                ChangeState(EnemyState.ATTACKING);
            }
        }

        void HandleAttackState()
        {
            WalkTowardsPlayer();

            if (!rangedAttackerComponent.isInRange)
            {
                movementComponent.movementSpeed *= movementPenaltyMultiplier;
                ChangeState(EnemyState.WALKING);
            }
        }

        void HandleHeldState()
        {
            sprite.transform.Rotate(0f, 0f, thrownSpinRate * heldSpinMultiplier * Time.deltaTime);
            animator.enabled = false;
        }
    }

    void WalkTowardsPlayer()
    {
        movementComponent.MoveTowards(new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), gameObject);
        animator.Play("PlayerWalk");
    }
}
