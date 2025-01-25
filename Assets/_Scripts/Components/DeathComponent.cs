using UnityEngine;

public abstract class DeathComponent : ScriptableObject
{
    public abstract void Die(MonoBehaviour context);
}
