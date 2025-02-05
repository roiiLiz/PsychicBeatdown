using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EnemyState
{
    IDLE,
    WALKING,
    ATTACKING,
    HELD,
    // THROWN
}

public abstract class Enemy : MonoBehaviour
{
    [field: SerializeField] public Stats stats { get; private set; }
    [Header("Stats")]
    [SerializeField] protected EnemyState startingState;
    [Space]
    [Header("Components")]
    [SerializeField] protected HealthComponent healthComponent;
    [SerializeField] protected MovementComponent movementComponent;
    [SerializeField] protected CinemachineImpulseSource impulseSource;
    [Space]
    [Header("Sprite Variables")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform sprite;
    // [SerializeField] protected SpriteRenderer selectedSprite;
    // [SerializeField] protected SpriteMask spriteMask;
    [SerializeField] protected float thrownSpinRate = 360f;
    [SerializeField] protected float heldSpinMultiplier = .25f;

    protected GameObject player => GameObject.FindGameObjectWithTag("Player");
    protected bool isSelected = false;
    protected EnemyState currentState = EnemyState.IDLE;

    protected int _attackDamage;

#region Initialization

    void OnDisable()
    {
        ScreenShakeManager.instance.CameraShake(impulseSource);
    }

    protected virtual void Start()
    {
        InitStats(stats);
        // InitSprite();
        currentState = startingState;
    }

    // protected virtual void InitSprite()
    // {
    //     selectedSprite.enabled = false;
    //     spriteMask.enabled = false;
    // }

    protected virtual void InitStats(Stats stats)
    {
        healthComponent.SetHealth(stats.maxHealth);
        movementComponent.movementSpeed = stats.movementSpeed;
        _attackDamage = stats.damageAmount;
        // _thrownSpeed = stats.thrownSpeed;

        // Debug.Log("Stats initalized");
    }

#endregion

    // public virtual void OnPointerEnter(PointerEventData eventData)
    // {
    //     selectedSprite.enabled = true;
    //     spriteMask.enabled = true;
    // }

    // public virtual void OnPointerExit(PointerEventData eventData)
    // {
    //     selectedSprite.enabled = false;
    //     spriteMask.enabled = false;
    // }

    public void ChangeState(EnemyState state)
    {
        currentState = state;
    }
}
