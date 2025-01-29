using UnityEngine;
using UnityEngine.EventSystems;

public interface IThrowable
{
    void RequestSelection();
    GameObject GetGameObject();
    // void NotifyThrowMediator(GameObject gameObject);
    // void DebugNotify(string message);
}

public class ThrowableComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // void OnEnable() => ThrowManager.Subscribe(this);
    // void OnDisable() => ThrowManager.Unsubscribe(this);

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
