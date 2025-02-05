using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ThrowableType
{
    FIREBALL,
    ARROW,
    ENEMY
}

public class ThrowableComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] public Stats stats { get; private set; }
    [field: SerializeField] public bool shouldRotate { get; private set; }
    [field: SerializeField] public Transform sprite { get; private set; }
    [field: SerializeField] public ThrowableType throwType { get; private set; }

    public bool isHeld { get; private set; } = false;

    public static event Action<MonoBehaviour> OnThrowableSelected;

    void OnEnable() => ThrowScript.HeldObject += IsCurrentlyHeld;
    void OnDisable() => ThrowScript.HeldObject -= IsCurrentlyHeld;

    void IsCurrentlyHeld(GameObject heldObject)
    {
        if (heldObject != null)
        {
            if (heldObject == gameObject)
            {
                isHeld = true;
            } else
            {
                isHeld = false;
            }
        } else
        {
            isHeld = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnThrowableSelected?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnThrowableSelected?.Invoke(null);
    }
}
