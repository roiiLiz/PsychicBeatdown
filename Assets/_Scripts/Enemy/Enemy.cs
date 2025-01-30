using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EnemyState
{
    IDLE,
    WALKING,
    ATTACKING,
    HELD,
    THROWN
}

public abstract class Enemy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Stats")]
    [SerializeField] protected Stats stats;
    [SerializeField] protected EnemyState startingState;
    [Space]
    [Header("Components")]
    [SerializeField] protected HealthComponent healthComponent;
    [SerializeField] protected MovementComponent movementComponent;
    [Space]
    [Header("Sprite Variables")]
    [SerializeField] protected SpriteRenderer selectedSprite;
    [SerializeField] protected SpriteMask spriteMask;

    protected GameObject player => GameObject.FindGameObjectWithTag("Player");
    protected bool isSelected = false;
    protected EnemyState currentState = EnemyState.IDLE;

    protected int _attackDamage;
    protected float _thrownSpeed;

#region Initialization

    protected virtual void Start()
    {
        InitStats(stats);
        InitSprite();
        currentState = startingState;
    }

    protected virtual void InitSprite()
    {
        selectedSprite.enabled = false;
        spriteMask.enabled = false;
    }

    protected virtual void InitStats(Stats stats)
    {
        healthComponent.SetHealth(stats.maxHealth);
        movementComponent.movementSpeed = stats.movementSpeed;
        _attackDamage = stats.damageAmount;
        _thrownSpeed = stats.thrownSpeed;

        // Debug.Log("Stats initalized");
    }

#endregion

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        selectedSprite.enabled = true;
        spriteMask.enabled = true;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        selectedSprite.enabled = false;
        spriteMask.enabled = false;
    }

    public void ChangeState(EnemyState state)
    {
        currentState = state;
    }

    // protected virtual void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("ThrownObjects"))
    //     {
    //         HealthComponent thrownObjectHealth = collision.GetComponent<HealthComponent>();
    //         if (thrownObjectHealth != null)
    //         {
    //             thrownObjectHealth.Damage(thrownObjectHealth.MaxHealth);
    //             healthComponent.Damage(thrownObjectHealth.MaxHealth);
    //         }
    //     }
    // }
}
