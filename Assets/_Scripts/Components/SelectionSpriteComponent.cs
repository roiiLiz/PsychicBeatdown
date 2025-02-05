using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionSpriteComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] SpriteRenderer selectionSprite;
    [SerializeField] SpriteMask mask;
    [SerializeField] ThrowableComponent throwableComponent;

    void Start()
    {
        selectionSprite.enabled = false;
        mask.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!throwableComponent.isHeld)
        {
            selectionSprite.enabled = true;
            mask.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectionSprite.enabled = false;
        mask.enabled = false;
    }
}