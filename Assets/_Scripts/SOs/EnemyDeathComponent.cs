using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Death Types/Enemy Death", fileName = "Enemy Death")]
public class EnemyDeathComponent : DeathComponent
{
    public static event Action OnEnemyDeath;

    public override void Die(MonoBehaviour context)
    {
        OnEnemyDeath?.Invoke();
        Destroy(context.gameObject);
    }
}
