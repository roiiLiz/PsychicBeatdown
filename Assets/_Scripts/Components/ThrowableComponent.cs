using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowableComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] public Stats stats { get; private set; }
    [field: SerializeField] public bool shouldRotate { get; private set; }
    [field: SerializeField] public Transform sprite { get; private set; }

    public static event Action<MonoBehaviour> OnThrowableSelected;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnThrowableSelected?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnThrowableSelected?.Invoke(null);
    }
}
