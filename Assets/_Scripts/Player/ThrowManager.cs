using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "Managers/Throw Manager", menuName = "Throw Manager")]
public class ThrowManager : ScriptableObject
{
    private readonly List<IThrowable> subscribers = new ();
    private IThrowable heldThrowable = null;

    public void Subscribe (IThrowable throwable) => subscribers.Add(throwable);
    public void Unsubscribe (IThrowable throwable) => subscribers.Remove(throwable);

    public IThrowable GetHeldThrowable() => heldThrowable;

    public event Action<IThrowable> TryThrow;

    public void SetThrowable(IThrowable throwable)
    {
        if (heldThrowable == null)
        {
            heldThrowable = subscribers.Find(t => t.Equals(throwable));
        } else
        {
            return;
        }
    }

    public void Notify(MonoBehaviour throwable)
    {
        if (heldThrowable != null)
        {
            // held isn't null -> throw 
            TryThrow?.Invoke(heldThrowable);
            SetThrowable(null);
        } else
        {
            // no held, get object
            heldThrowable = throwable.GetComponent<IThrowable>();
        }
    }
}