using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowableComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
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
