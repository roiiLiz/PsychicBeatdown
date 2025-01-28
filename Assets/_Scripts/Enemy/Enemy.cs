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

public abstract class Enemy : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IThrowable
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

    public static event Action<IThrowable> OnSelected;

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

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer click");
        Selected();
    }

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

    public virtual void Selected()
    {
        Debug.Log($"{name} selected");
        OnSelected?.Invoke(this);
    }

    public GameObject GetThrowableObject()
    {
        return this.gameObject;
    }

    public void ChangeState(EnemyState state)
    {
        currentState = state;
    }
}
